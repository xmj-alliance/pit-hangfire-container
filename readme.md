# Hangfire Container

An example of containerizing Hangfire dashboard and server for background job processings.

## How to run

1. Install MSSQL Server.

2. Provide an `appsettings.Production.json` including your connection strings. For example:

``` json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=superSQLServer,1433;Database=HangfireContainer-dev;User Id=[dbuser];Password=[mypasswd];"
  }
}
```

3. Follow the [docker-compose example](./docker-compose.run.example.yaml) and make yours, then run with:

```
docker-compose up -d
```

## How to build

```
docker-compose build
```

## Structure

`HangfireContainer.Dashboard`:

A WebAPI type app for hosting Hangfire dashboard. Visit `/hangfire` to access.

`HangfireContainer.Job`:

A collection of defined tasks for Hangfire to schedule and run. Exists as a class library.

`HangfireContainer.Server`:

A Worker type app for hosting Hangfire background server. Main task scheduler and runner.

## How this works

In `HangfireContainer.Job/Welcome.cs` we defined a job, `SendWelcome()`, Which logs the given name to the console each time it is called.

In `HangfireContainer.Dashboard/Controllers/WelcomeController.cs`, a `WelcomeUser()` Controller is created.

When we send a POST request to `/welcome/welcome` with data looking like:

``` json
{
  "username": "Jacky"
}
```

The Hangfire client, which is connected to the background server, will enqueue a task of `SendWelcome()` and pass in the given username, and send out a reponse of the acquired Hangfire Job ID.

``` json
{
  "ok": true,
  "message": "Job Id 11 Completed. Welcome Sent!"
}
```

When the job is executed, we can see some output at the console of background server, which looks like:

```
Welcome to our application, Jacky
```

And finally docker files and docker-compose build config have been made, which allows us to implement containerization technologies and make the scaling easier according to our needs.

## Ref

https://alimozdemir.com/posts/hangfire-docker-with-multiple-servers/
https://codewithmukesh.com/blog/hangfire-in-aspnet-core-3-1/
