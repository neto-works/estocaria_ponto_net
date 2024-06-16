## Project estocaria

<div align="center">
  <img src="#" width="70" height="70" />
</div>

<h4 align="center">This project is just to train the creation of apis with aspnet used during Hanami on 05/2024 :cherry_blossom: </h4>

<p align="center">
  [Project's badges]
</p>

<p align="center">
    <a href="#Technologies_Used">Technologies Used</a> •
    <a href="#Conceptual_Model">Conceptual model</a> •
    <a href="#Api_resources">Api resources</a> •
    <a href="#Folder_Architecture">Folder Architecture</a> •
    <a href="#Running_Application">Running application</a> •
    <a href="#About_the_Author">About the Author</a> •
    <a href="https://github.com/neto-works/estocaria_ponto_net/blob/main/LICENSE">Licensing</a>
</p>

## Technologies_Used

- The following technologies were used in this project:
    - C# as a programming language.
    - The dotNet development platform.
    - The ASP.NET Core microframework for building web applications.
    - The famous ORM on the .Net platform, the Entity Framework Core with some plugins and Mysql database.
    - And the docker containerization tool.

## Folder_Architecture
```
Solução + P.EstocariaNet.
              ├── obj
              ├── Properties
              ├── Shared.
              ├         └──────────  Context/ AppDbContext.cs
              ├         └──────────  DTOs /  allDTO.cs
              ├         └──────────  Repositories / allRepository,cs
              ├── Controllers   / *allController.cs
              ├── Services      / *allServices.cs
              ├── Models        / *allModels.cs
              ├── Migrations    / *_mig_feature.cs
              ├
              ├── EstocariaNet.csproj
              ├── appsettings.Development.json
              ├── appsettings.json
              └── Program.cs

        +  P. Testes.
              ├── obj
              ├── Properties
              ├── Testes.csproj
              ├── UnitTest_ ... alltest.cs
              └── Pilha_comMaisTestes.cs
```
## Conceptual_Model

<img src="https://github.com/neto-works/estocaria_ponto_net/blob/main/EstocariaNet/docs/modelo.PNG" />

## Bank_Model

<img src="https://github.com/neto-works/estocaria_ponto_net/blob/main/EstocariaNet/docs/dbeaver.PNG" />

## Api_resources v1

### Auth 

- open to everyone
  ```http
  POST /api/Auth/login
  ```
  ```http
  POST /api/Auth/register
  ```
  ```http
  POST /api/Auth/refresh-token
  ```
- Authorize Policy _"QuemPuderAdinistrar"(Admin)_
  ```http
  POST /api/Auth/revoke/{email}
  ```
   ```http
  POST api/Auth/CreateRole
  ```
   ```http
  POST /api/Auth/AddUserToRole
  ```

### Estoquista

- Authorize Policy _"QuemPuderEstocar"(Estoquista)_
  ```http
  PUT /api/Estoquista/{id}
  ```
- Authorize Policy _"QuemPuderAdinistrar"(Admin)_
  ```http
  GET /api/Estoquistas/{id}
  ```
  ```http
  DELETE /api/Estoquistas/{id}
  ```
  ```http
  GET /api/Estoquistas
  ```

### Admin

- Authorize Policy _"QuemPuderAdinistrar"(Admin)_
  ```http
  POST /api/Admin/{id}/Estoquista/{id}/Estoque/{id}
  ```
  ```http
  DELETE /api/Admin/{id}/Estoquista/{id}/Estoque/{id}
  ```

### Estoques

- Authorize Policy _"QuemPuderEstocar"(Estoquista) OR "QuemPuderAdinistrar"(Admin)_
 ```http
  GET /api/Estoques
  ```
  ```http
  GET /api/Produtos/{id}
  ```
- Authorize Policy _"QuemPuderEstocar"(Estoquista) OR "QuemPuderAdinistrar"(Admin)_
  ```http
  PUT /api/Estoques
  ```
  ```http
  DELETE /api/Produtos/{id}
  ```
  ```http
  POST /api/Estoques
  ```

### Categorias

- Authorize Policy _"QuemPuderAdinistrar"(Admin)_
  ```http
  POST /api/Categorias
  ```

### Produtos

- Authorize Policy _"QuemPuderEstocar"(Estoquista) OR "QuemPuderAdinistrar"(Admin)_
  ```http
  POST /api/Produtos
  ```
  ```http
  GET /api/Produtos
  ```
  ```http
  GET /api/Produtos/{id}
  ```
  ```http
  PUT /api/Produtos/{id}
  ```
  ```http
  DELETE /api/Produtos/{id}
  ```
  ```http
  POST /api/Produtos/{id}/Categorias/{id}
  ```

### Lancamentos

- Authorize Policy _"QuemPuderEstocar"(Estoquista)_
  ```http
  POST /api/Lancamentos 
  ```
- Authorize Policy _"QuemPuderAdinistrar"(Admin)_
  ```http
  GET /api/Lancamentos/Periodo
  ```

### Relatorios

- Authorize Policy _"QuemPuderAdinistrar"(Admin)_
  ```http
  POST /api/Relatorios 
  ```

## Prerequisites
- .Net version 8
- Docker and Docke-compose plugin
- EntityFrameworkCore Design
  ```
    dotnet tool install --global dotnet-ef
  ```
## Running_Application

- create the bank:
  ```
    cd EstocariaNet && docker compose up
  ```

- download dependencies:
  ```
    dotnet restore
  ```

- to execute:
  ```
    dotnet ef database update &&
    dotnet run
  ```
 - http://localhost:7048/swagger/index.html

## Test the application using postman

- open your postman desktop
- click import
- import from z_Postman_Endpoints folder

> [!IMPORTANT]
> When opening the endpoints, check the "base_url" addressing variable if it is correct.

> [!WARNING]
> When making requests, pay attention to the tokens and the app's authorization policies. Errors may also be found as it is still under development


## About_the_Author
- Clodoaldo Neto :call_me_hand:
