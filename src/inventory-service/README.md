# Inventory Service

This project needs an instance of SQL Server, SQL Database, or PostgreSQL. Create a user secret in InventoryService.Api project and run the app:

```
dotnet user-secrets set 'ConnectionStrings:InventoryContext' '<sqldb-connection-string>'
dotnet run
```

* REST API docs can be accessed using Swagger UI: `/swagger`
* Get real-time inventory updates, see SignalR test page: `/www`

Optional: Use Azure SignalR Service by adding another secret:

```
dotnet user-secrets set 'SignalRServiceConnectionString' '<azure-signalr-connection-string>'
```
