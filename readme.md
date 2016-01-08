# WebApi Toolbox

<br>
The WebApi Toolbox offers some functionality that can be used in ASP.NET Web API 5 projects:
- versioning.
- base classes that encapsulate common functionality.
- root object formatter for connecting with ESB.
- query string mapping.

<br>
## Installation
Adding the toolbox to a project can be done via the NuGet Package Manager in Visual Studio or by adding the package to the project.json :

``` json
 "dependencies": {
    ...,
    "Toolbox.WebApi":  "1.0.5",
    ...
 }
```
<br>
## Versioning
The versioning framework adds an additional endpoint on the Web Api where the version number of the application can be requested. By default, this endpoint is provided at the url **_admin/version_**, but this can be changed to another value during startup.

<br>
The framework is added to an ASP.NET project in the 5 **_Startup _**  class. First the necessary services are configured in the ConfigureServices method of the Startup class :

``` csharp
  services.AddWebApiVersioning(WebApiVersioningOptions.Default);
```
If you are satisfied with the default options, you send ** _WebApiVersioningOptions.Default_** as the parameter. To set custom options, can send a newly instantiated object WebApiVersioningOptions :

``` csharp
   app.AddWebApiVersioning(new WebApiVersioningOptions() { Route = "api/mijnversie" });
```

The following options can be set :

Option              | Description                                                | Default
------------------ | ----------------------------------------------------------- | --------------------------------------
Route              | the URL where the list of code tables can be requested | admin/version

Then the framework gets started in the Configure method of the startup class.

``` csharp
   app.UseWebApiVersioning();
```


<br>
## Base classes

Details coming soon.


<br>
## RootObjectFormatters

Details coming soon.


<br>
## QueryStringFormatting

Details coming soon.



<br>
## Versions

| Version | Author                                  | Description
| ------ | ----------------------------------------| ----------------------------------------------------
| 1.0.0  | Steven Vanden Broeck                    | Initial version.
| 1.0.1  | Koen Stroobants                         | English translation
