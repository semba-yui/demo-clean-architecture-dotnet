# demo-clean-architecture-dotnet

## Migration

### Create migration

```shell
dotnet tool run dotnet-ef migrations add InitialIdentity \
  --project ./src/DemoCleanArchitecture.Infrastructure \
  --startup-project ./src/DemoCleanArchitecture.Api \
  --context DemoCleanArchitectureDbContext
```

```shell
dotnet tool run dotnet-ef migrations add InitialData \
  --project ./src/DemoCleanArchitecture.Infrastructure \
  --startup-project ./src/DemoCleanArchitecture.Api \
  --context DemoCleanArchitectureDbContext
```

### Update database

```shell
dotnet tool run dotnet-ef database update InitialIdentity \
  --project ./src/DemoCleanArchitecture.Infrastructure \
  --startup-project ./src/DemoCleanArchitecture.Api \
  --context DemoCleanArchitectureDbContext
```

```shell
dotnet tool run dotnet-ef database update InitialData \
  --project ./src/DemoCleanArchitecture.Infrastructure \
  --startup-project ./src/DemoCleanArchitecture.Api \
  --context DemoCleanArchitectureDbContext
```

### Remove migration

```shell
dotnet tool run dotnet-ef migrations remove \
  --project ./src/DemoCleanArchitecture.Infrastructure \
  --startup-project ./src/DemoCleanArchitecture.Api \
  --context DemoCleanArchitectureDbContext
```

## Code Format

```shell
dotnet tool run jb cleanupcode DemoCleanArchitecture.sln
```

## 備考

乱数生成（JWTのシークレットキー）

```shell
$ openssl rand -base64 32
zT13v36WiWNVzW/XrhaKGW/e78a/1hsNy0h2DdqRmYo=
```