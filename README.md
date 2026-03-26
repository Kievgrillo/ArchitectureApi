ProjectReactAndNet — Backend API
> REST API construída com **ASP.NET Core 8** seguindo os princípios da **Clean Architecture**.  
> Gerenciamento de produtos com CRUD completo, Entity Framework Core e SQL Server.
---
Tecnologias
Tecnologia	Versão	Uso
ASP.NET Core	8.0	Framework web e HTTP pipeline
Entity Framework Core	8.x	ORM e migrations
SQL Server	2019+	Banco de dados relacional
Swagger / OpenAPI	—	Documentação interativa da API
C#	12	Linguagem principal
---
Arquitetura
O projeto segue a Clean Architecture, onde as dependências sempre apontam para dentro — camadas externas conhecem as internas, mas nunca o contrário.
```
Solution 'ProjectReactAndNet'
├── ProjectReactAndNet.API             # Controllers, Program.cs, configurações
├── ProjectReactAndNet.Application     # Serviços e casos de uso
├── ProjectReactAndNet.Domain          # Entidades e interfaces (núcleo)
└── ProjectReactAndNet.Infrastructure  # Repositórios, DbContext, acesso ao banco
```
Fluxo de dependências
```
API → Application → Domain
API → Infrastructure → Domain
```
O `Domain` não depende de nada. O `Infrastructure` implementa as interfaces definidas no `Domain`.
---
Estrutura de pastas
```
ProjectReactAndNet.API/
├── Controllers/
│   └── ProductsController.cs
├── appsettings.json
└── Program.cs

ProjectReactAndNet.Application/
└── Services/
    └── ProductService.cs

ProjectReactAndNet.Domain/
├── Entities/
│   └── Product.cs
└── Interfaces/
    └── IProductRepository.cs

ProjectReactAndNet.Infrastructure/
├── Data/
│   └── AppDbContext.cs
└── Repositories/
    └── ProductRepository.cs
```
---
Pré-requisitos
.NET 8 SDK
SQL Server (ou SQL Server Express)
Visual Studio 2022 ou VS Code
---
Como rodar o projeto
1. Clone o repositório
```bash

```
2. Configure a connection string
Abra `ProjectReactAndNet.API/appsettings.json` e ajuste conforme seu ambiente:
```json

```
> Veja a seção [Connection Strings](#connection-strings) para outros cenários.
3. Aplique as migrations
No Package Manager Console (Visual Studio) ou no terminal:
```powershell

```

