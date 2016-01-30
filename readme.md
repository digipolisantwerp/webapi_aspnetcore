# WebApi Toolbox  

The WebApi Toolbox offers some functionality that can be used in ASP.NET Core 1.0 Web API projects:
- versioning.
- base classes that encapsulate common functionality.
- root object formatter for JSON formatting.
- controller action overloading.  

## Installation

Adding the toolbox to a project can be done via the NuGet Package Manager in Visual Studio or by adding the package to the project.json :

``` json
 "dependencies": {
    "Toolbox.WebApi":  "1.0.8"
 }
```

## ActionOverloading

If you have used older versions ASP.NET Web API, you are used to writing overloaded controllers that get called depending on the number of query string parameters.

```
// GET /myapi/books
[HttpGet]
public IActionResult GetAll()

// GET /myapi/books?page=2&pagesize=10
[HttpGet]
public IActionResult GetAllPaging([FromQuery]int page, [FromQuery]int pagesize)

// GET /myapi/books?authorid=7&page=1&pagesize=10
[HttpGet]
public IActionResult GetByAuthorIdPaging([FromQuery]int authorid, [FromQuery]int page, [FromQuery]int pagesize)
```

In ASP.NET Core 1.0, the MVC and Web API frameworks are merged into one unified MVC framework and the above functionality is not possible anymore.     

With the ActionOverloading part of this toolbox you can add this functionality to an MVC 6 API project by calling the following method in Startup.**ConfigureServices** :

```
services.AddMvc().AddActionOverloading();
```

## Versioning  

The versioning framework adds an additional endpoint on the Web Api where the version number of the application can be requested. By default, this endpoint is provided at the url **_admin/version_**, but this can be changed to another value during startup.


The versioning framework is added to an ASP.NET project in the **_Startup _**  class. First the necessary services are configured in the **ConfigureServices** method of the Startup class :

``` csharp
  services.AddWebApiVersioning(WebApiVersioningOptions.Default);
```

If you are satisfied with the default options, pass in ** _WebApiVersioningOptions.Default_** as parameter. To set custom options, instantiate a new WebApiVersioningOptions object and set its properties :

``` csharp
   app.AddWebApiVersioning(new WebApiVersioningOptions() { Route = "api/myendpoint" });
```

The following options can be set :

Option             | Description                                                 | Default
------------------ | ----------------------------------------------------------- | --------------------------------------
Route              | the URL where the list of code tables can be requested      | admin/version

Then the framework is started in the **Configure** method of the Startup class.

``` csharp
   app.UseWebApiVersioning();
```


<br>
## Base classes

Details coming soon.


<br>
## RootObjectFormatters

Details coming soon.


