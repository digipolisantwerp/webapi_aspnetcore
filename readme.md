# WebApi Toolbox  

The WebApi Toolbox offers some functionality that can be used in ASP.NET Core 1.0 Web API projects:
- versioning.
- base classes that encapsulate common functionality.
- root object formatter for JSON formatting.
- controller action overloading.  
- swagger extensions

## Table of Contents

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->


- [Installation](#installation)
- [ActionOverloading](#actionoverloading)
- [RootObject Formatters](#rootobject-formatters)
- [Versioning](#versioning)
- [Swagger extensions](#swagger-extensions)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

## Installation

To add the toolbox to a project, you add the package to the project.json :

``` json 
"dependencies": {
    "Toolbox.WebApi":  "1.1.0"
 }
``` 

In Visual Studio you can use the NuGet Package Manager to do this.
  
  
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

In ASP.NET Core 1.0, the MVC and Web API frameworks are merged into one unified MVC framework and the above functionality is not included anymore.     

With the ActionOverloading part of this toolbox you can add this functionality to an MVC 6 API project by calling the following method in Startup.**ConfigureServices** :

```
services.AddMvc().AddActionOverloading();
```
     
## RootObject Formatters

...ToDo...  
  
  
## Versioning  

The versioning framework adds an additional endpoint to the Web Api where the version number of the application can be requested via a GET request.  
By default, this endpoint is provided at the url **_admin/version_**, but this can be changed to another value during startup.

The versioning framework is added to the project in **ConfigureServices** method of the Startup  class :

``` csharp
  services.AddMvc().AddVersioning();           // default route = /admin/version
  
  service.AddMvc().AddVersioning(options => options.Route = "myRoute");      // use custom route 
```

## Swagger extensions

When you use SwashBuckle to generate a Swagger UI for your API project, you might like to have the root url of your API point to that site. This can be done in the **Configure** of the Startup class :  

``` csharp

// ui on default url (swagger/ui)
app.UseSwaggerUi();                 // from SwashBuckle.SwaggerUi package
app.UseSwaggerUiRedirect();         

// custom url
app.UseSwaggerUi("myUrl");          // from SwashBuckle.SwaggerUi package
app.UseSwaggerUiRedirect("myUrl") 
``` 
