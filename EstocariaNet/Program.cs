using EstocariaNet.Services;
using EstocariaNet.Services.Interfaces;
using EstocariaNet.Shared.Context;
using EstocariaNet.Shared.Repositories;
using EstocariaNet.Shared.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EstocariaNet.Models;
using Microsoft.OpenApi.Models;
using EstocariaNet.Shared.Swagger;
using EstocariaNet.Shared.DTOs.Creates;
using EstocariaNet.Shared.DTOs;
using EstocariaNet.Shared.DTOs.Updates;

var builder = WebApplication.CreateBuilder(args);
var secretKey = builder.Configuration["JWT:SecretKey"] ?? throw new ArgumentException("Invalid secretkey");

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });
builder.Services.AddIdentity<AplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IRepositoryProdutosPaginate, RepositoryProdutosPaginate>();
builder.Services.AddScoped<IRepositoryRelatorios, RepositoryRelatorios>();
builder.Services.AddScoped<IRepositoryLancamentos, RepositoryLancamentos>();

builder.Services.AddScoped<IProdutosServices, ProdutosServices>();
builder.Services.AddScoped<IEstoquistaServices, EstoquistaServices>();
builder.Services.AddScoped<IEstoquesServices, EstoquesServices>();
builder.Services.AddScoped<IAdminServices, AdminsServices>();
builder.Services.AddScoped<IAuthServices, AuthServices>();
builder.Services.AddScoped<ICategoriasServices, CategoriasServices>();
builder.Services.AddScoped<ILancamentosServices, LancamentosServices>();
builder.Services.AddScoped<IRelatoriosServices, RelatoriosServices>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
    };
});
builder.Services.AddScoped<ITokenServices, TokenServices>();

// add policies of users
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("QuemPuderAdministrar", policy => policy.RequireRole("Administrar"));
    options.AddPolicy("QuemPuderGerenciar", policy => policy.RequireRole("Gerenciar"));
    options.AddPolicy("QuemPuderEstocar", policy => policy.RequireRole("Estocar"));
    options.AddPolicy("QuemPuderFazerAmbasAsFuncoes", policy => policy.RequireAssertion(context => context.User.IsInRole("Administrar") || context.User.IsInRole("Estocar")));
});

// add policy cors
var OriginAcess = "_originAcessAllowed";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: OriginAcess,
    policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "estocaria.net", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Bearer JWT ",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
    c.DocumentFilter<FilterSwagger>(new List<Type> {
        typeof(CreateAdminDTO),typeof(LoginDTO),typeof(UpdateAdminDTO),typeof(TokenDTO),typeof(RegistroDTO),
        typeof(DataComaparableDTO),typeof(CreateRelatorioDTO),typeof(CreateProdutoDTO),typeof(CreateCategoriaDTO),
        typeof(UpdateEstoquistaDTO),typeof(CreateLancamentoDTO),typeof(TipoUsuarioEnum),typeof(UpdateEstoqueDTO),
        typeof(UpdateProdutoDTO),typeof(UpdateProdutoDTO),
        });
});

//register context
string? mySQLConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(mySQLConnection, ServerVersion.AutoDetect(mySQLConnection)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors(OriginAcess);

app.UseAuthorization();
app.MapControllers();

// init Estoque padrao
using (var scope = app.Services.CreateScope())
{
    var estoquesServices = scope.ServiceProvider.GetRequiredService<IEstoquesServices>();
    await estoquesServices.InitEstoque();
}


app.Run();
