dotnet build
set version=%1%
nuget push ./bin/Debug/JackHttpServer.%version%.nupkg -Source https://www.nuget.org