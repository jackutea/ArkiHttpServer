set file=where *.nupkg
del %file%
set file=where *.nuspec
del %file%

nuget pack
nuget spec
set version=%1%
nuget push JackHttpServer.%version%.nupkg -Source https://www.nuget.org