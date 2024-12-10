Requisitos para rodar o projeto:

#.Net 8
#Angular 18
#Docker

1. Acesse a pasta do projeto `LibraryManager.API`.

2. Execute os comandos abaixo para adicionar a migração inicial:

   ```bash
   dotnet ef migrations add InitialDataBase --output-dir Migrations --project ../LibraryManager.Infrastructure/LibraryManager.Infrastructure.csproj --context LibraryContext --startup-project LibraryManager.API.csproj -v

   dotnet ef database update
