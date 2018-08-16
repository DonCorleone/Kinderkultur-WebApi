# Readme Server-Part

## Introduction

Application-Server built on .NET Core 2.1 for Kinderkultur.ch Pet - Project

## Authorization

[Idee](https://docs.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-2.1)

## Swagger

[NSwag](https://github.com/RSuter/NSwag)
[Microsoft](https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-nswag?view=aspnetcore-2.1&tabs=visual-studio%2Cvisual-studio-xml)

## AutoMapper

[NuGet](https://www.nuget.org/packages/AutoMapper.Extensions.Microsoft.DependencyInjection/)
[Docs](https://dotnetcoretutorials.com/2017/09/23/using-automapper-asp-net-core/)
[Official](http://automapper.readthedocs.io)

## DBs

### MongoDB

    cd /usr/local/bin;
    mongod --config mongod.cfg

### Maria DB

#### Create Docker Container

docker run --name mariadb -e MYSQL_ROOT_PASSWORD=rootpwd -p 3306:3306 -d mariadb

#### Init

dotnet ef database update 20180323185657_initial --context MariaDbContext