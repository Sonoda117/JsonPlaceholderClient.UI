# Json Placeholder Client

ASP.NET Core Web API that consumes the [JsonPlaceholder](https://jsonplaceholder.typicode.com/) site resources. This is a really basic project to use as base for future practices and test implementations.

## API

Run the app using the `http` profile so you can use the [API.http](./src/API/API.http) file to test the endpoints. This file uses the route `http://localhost:7012` as base, which is the same when [running the app in a container](#docker-compose).

```powershell
API> dotnet run --launch-profile "http"
```

## Docker

You can run the app in a docker container too, see the [docker-compose.yml](./docker-compose.yml) file.

```yml
# docker compose up -d --build
# docker ps --format "table {{.Names}}\t{{.State}}\t{{.RunningFor}}\t{{.Status}}"
# docker stats --format "table {{.Name}}\t{{.MemUsage}}\t{{.CPUPerc}}\t{{.PIDs}}"
# docker compose rm -s
jsonoplaceholder-api:
    container_name: jsonoplaceholder-api
    image: jsonoplaceholder-api:dev
    build:
      context: .
      dockerfile: src/API/Dockerfile
    ports:
      - "7012:80"
```