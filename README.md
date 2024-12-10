## Requisitos para Rodar o Projeto

- **.NET 8**
- **Angular 18**
- **Docker**

---

## Passos para Rodar as Migrations no Projeto `LibraryManager.API`

1. **Acesse a pasta do projeto `LibraryManager.API`:**

2. **Execute os seguintes comandos:**

     ```bash
     dotnet ef migrations add InitialDataBase --output-dir Migrations --project ../LibraryManager.Infrastructure/LibraryManager.Infrastructure.csproj --context LibraryContext --startup-project LibraryManager.API.csproj -v
     ```

     ```bash
     dotnet ef database update
     ```

---
