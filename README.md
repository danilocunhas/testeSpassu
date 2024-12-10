Requisitos para rodar o projeto:

.Net 8
Angular 18
Docker

1. Acesse a pasta do projeto `LibraryManager.API`.

2. Execute o comando abaixo para adicionar a migração inicial:

   ```bash
   dotnet ef migrations add InitialDataBase --output-dir Migrations --project ../LibraryManager.Infrastructure/LibraryManager.Infrastructure.csproj --context LibraryContext --startup-project LibraryManager.API.csproj -v

3.Em seguida, execute o comando abaixo para aplicar as migrações no banco de dados:

   ```bash
   dotnet ef database update
