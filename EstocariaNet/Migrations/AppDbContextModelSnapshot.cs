﻿// <auto-generated />
using System;
using EstocariaNet.Shared.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EstocariaNet.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("EstocariaNet.Models.Admin", b =>
                {
                    b.Property<string>("AdminId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AplicationUserAdminId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Setor")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.HasKey("AdminId");

                    b.HasIndex("AplicationUserAdminId");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("EstocariaNet.Models.AplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("AdminId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("EstoquistaId")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<int?>("TipoUsuario")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("EstoquistaId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("EstocariaNet.Models.Categoria", b =>
                {
                    b.Property<int>("CategoriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("CategoriaId"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.HasKey("CategoriaId");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("EstocariaNet.Models.Estoque", b =>
                {
                    b.Property<int>("EstoqueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("EstoqueId"));

                    b.Property<float>("Capacidade")
                        .HasColumnType("float");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("EstoqueAdminId")
                        .HasColumnType("longtext");

                    b.Property<string>("LinkAdminAdminId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Local")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("varchar(120)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.HasKey("EstoqueId");

                    b.HasIndex("LinkAdminAdminId");

                    b.ToTable("Estoques");
                });

            modelBuilder.Entity("EstocariaNet.Models.Estoquista", b =>
                {
                    b.Property<string>("EstoquistaId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AplicationUserEstoquistaId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Celular")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("EstoqueId")
                        .HasColumnType("int");

                    b.Property<int?>("EstoquistaEstoqueId")
                        .HasColumnType("int");

                    b.HasKey("EstoquistaId");

                    b.HasIndex("AplicationUserEstoquistaId");

                    b.HasIndex("EstoqueId");

                    b.HasIndex("EstoquistaEstoqueId");

                    b.ToTable("Estoquistas");
                });

            modelBuilder.Entity("EstocariaNet.Models.Lancamento", b =>
                {
                    b.Property<int>("LancamentoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("LancamentoId"));

                    b.Property<DateTime?>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("EstoqueaId")
                        .HasColumnType("int");

                    b.Property<string>("EstoquistaId")
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("LinkEstoqueEstoqueId")
                        .HasColumnType("int");

                    b.Property<int?>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<float>("QuantEntrada")
                        .HasColumnType("float");

                    b.Property<float>("QuantSaida")
                        .HasColumnType("float");

                    b.HasKey("LancamentoId");

                    b.HasIndex("EstoquistaId");

                    b.HasIndex("LinkEstoqueEstoqueId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("Lancamentos");
                });

            modelBuilder.Entity("EstocariaNet.Models.Produto", b =>
                {
                    b.Property<int>("ProdutoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ProdutoId"));

                    b.Property<int?>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("EstoqueId")
                        .HasColumnType("int");

                    b.Property<string>("ImagemUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<decimal?>("Preco")
                        .IsRequired()
                        .HasColumnType("decimal(65,30)");

                    b.Property<float?>("QuantEstoqueMax")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<float?>("QuantEstoqueMin")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<float?>("Saldo")
                        .HasColumnType("float");

                    b.HasKey("ProdutoId");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("EstoqueId");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("EstocariaNet.Models.Relatorio", b =>
                {
                    b.Property<int>("RelatorioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("RelatorioId"));

                    b.Property<string>("AdminId")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataFim")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("MesAnoPred")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("PredProdutoEntrada")
                        .HasColumnType("int");

                    b.Property<int?>("PredProdutoSaida")
                        .HasColumnType("int");

                    b.Property<double?>("PredTotalArrecadar")
                        .HasColumnType("double");

                    b.Property<bool?>("PredicaoProxMeses")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("ProdutoMaisEntrou")
                        .HasColumnType("int");

                    b.Property<int?>("ProdutoMaisSaiu")
                        .HasColumnType("int");

                    b.Property<string>("RelatorioName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<double?>("TotalArrecadado")
                        .HasColumnType("double");

                    b.HasKey("RelatorioId");

                    b.HasIndex("AdminId");

                    b.ToTable("Relatorios");
                });

            modelBuilder.Entity("LancamentoRelatorio", b =>
                {
                    b.Property<int>("LancamentosLancamentoId")
                        .HasColumnType("int");

                    b.Property<int>("RelatoriosRelatorioId")
                        .HasColumnType("int");

                    b.HasKey("LancamentosLancamentoId", "RelatoriosRelatorioId");

                    b.HasIndex("RelatoriosRelatorioId");

                    b.ToTable("LancamentoRelatorio");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("EstocariaNet.Models.Admin", b =>
                {
                    b.HasOne("EstocariaNet.Models.AplicationUser", "AplicationUser")
                        .WithMany()
                        .HasForeignKey("AplicationUserAdminId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("AplicationUser");
                });

            modelBuilder.Entity("EstocariaNet.Models.AplicationUser", b =>
                {
                    b.HasOne("EstocariaNet.Models.Admin", "Admin")
                        .WithMany()
                        .HasForeignKey("AdminId");

                    b.HasOne("EstocariaNet.Models.Estoquista", "Estoquista")
                        .WithMany()
                        .HasForeignKey("EstoquistaId");

                    b.Navigation("Admin");

                    b.Navigation("Estoquista");
                });

            modelBuilder.Entity("EstocariaNet.Models.Estoque", b =>
                {
                    b.HasOne("EstocariaNet.Models.Admin", "LinkAdmin")
                        .WithMany()
                        .HasForeignKey("LinkAdminAdminId");

                    b.Navigation("LinkAdmin");
                });

            modelBuilder.Entity("EstocariaNet.Models.Estoquista", b =>
                {
                    b.HasOne("EstocariaNet.Models.AplicationUser", "AplicationUser")
                        .WithMany()
                        .HasForeignKey("AplicationUserEstoquistaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EstocariaNet.Models.Estoque", null)
                        .WithMany("Estoquistas")
                        .HasForeignKey("EstoqueId");

                    b.HasOne("EstocariaNet.Models.Estoque", "Estoque")
                        .WithMany()
                        .HasForeignKey("EstoquistaEstoqueId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("AplicationUser");

                    b.Navigation("Estoque");
                });

            modelBuilder.Entity("EstocariaNet.Models.Lancamento", b =>
                {
                    b.HasOne("EstocariaNet.Models.Estoquista", "LinkEstoquista")
                        .WithMany("Lancamentos")
                        .HasForeignKey("EstoquistaId");

                    b.HasOne("EstocariaNet.Models.Estoque", "LinkEstoque")
                        .WithMany("Lancamentos")
                        .HasForeignKey("LinkEstoqueEstoqueId");

                    b.HasOne("EstocariaNet.Models.Produto", "LinkProduto")
                        .WithMany("Lancamentos")
                        .HasForeignKey("ProdutoId");

                    b.Navigation("LinkEstoque");

                    b.Navigation("LinkEstoquista");

                    b.Navigation("LinkProduto");
                });

            modelBuilder.Entity("EstocariaNet.Models.Produto", b =>
                {
                    b.HasOne("EstocariaNet.Models.Categoria", "LinkCategoria")
                        .WithMany("ProdutoLink")
                        .HasForeignKey("CategoriaId");

                    b.HasOne("EstocariaNet.Models.Estoque", "LinkEstoque")
                        .WithMany("Produtos")
                        .HasForeignKey("EstoqueId");

                    b.Navigation("LinkCategoria");

                    b.Navigation("LinkEstoque");
                });

            modelBuilder.Entity("EstocariaNet.Models.Relatorio", b =>
                {
                    b.HasOne("EstocariaNet.Models.Admin", "LinkAdmin")
                        .WithMany("Relatorios")
                        .HasForeignKey("AdminId");

                    b.Navigation("LinkAdmin");
                });

            modelBuilder.Entity("LancamentoRelatorio", b =>
                {
                    b.HasOne("EstocariaNet.Models.Lancamento", null)
                        .WithMany()
                        .HasForeignKey("LancamentosLancamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EstocariaNet.Models.Relatorio", null)
                        .WithMany()
                        .HasForeignKey("RelatoriosRelatorioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("EstocariaNet.Models.AplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("EstocariaNet.Models.AplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EstocariaNet.Models.AplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("EstocariaNet.Models.AplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EstocariaNet.Models.Admin", b =>
                {
                    b.Navigation("Relatorios");
                });

            modelBuilder.Entity("EstocariaNet.Models.Categoria", b =>
                {
                    b.Navigation("ProdutoLink");
                });

            modelBuilder.Entity("EstocariaNet.Models.Estoque", b =>
                {
                    b.Navigation("Estoquistas");

                    b.Navigation("Lancamentos");

                    b.Navigation("Produtos");
                });

            modelBuilder.Entity("EstocariaNet.Models.Estoquista", b =>
                {
                    b.Navigation("Lancamentos");
                });

            modelBuilder.Entity("EstocariaNet.Models.Produto", b =>
                {
                    b.Navigation("Lancamentos");
                });
#pragma warning restore 612, 618
        }
    }
}