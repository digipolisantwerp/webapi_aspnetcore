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
- [ActionFilters](#actionfilters)
  - [ValidateModelState](#validatemodelstate)
- [RootObject Formatters](#rootobject-formatters)
- [Versioning](#versioning)
- [Swagger extensions](#swagger-extensions)
- [Exception handling](#exception-handling)
  - [Http status code mappings](#http-status-code-mappings)
  - [Usage](#usage)
  - [Logging](#logging)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

## Installation

To add the toolbox to a project, you add the package to the project.json :

``` json 
"dependencies": {
    "Toolbox.WebApi":  "1.3.0"
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
     
## ActionFilters  

### ValidateModelState  
When you write a lot of CUD operations one of the most recurring pieces of code is the validation of the ModelState in your controllers :  

``` csharp
if ( !ModelState.IsValid )
{
    // maybe do some logging
    return new BadRequestObjectResult(ModelState);
}
```  

By adding the ValidateModelState action filter attribute to your action, the validation is done automatically :

``` csharp 
[HttpPost]
[ValidateModelState]
public IActionResult Create(MyModel model)
{
    // no need to validate the ModelState here, it's already done before this code is executed
}
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

## Exception handling

The toolbox provides a uniform way of exception handling.
The best way to use this feature is to have you code throw exceptions that derive from the **BaseException** type defined in the [error toolbox](https://github.com/digipolisantwerp/errors_aspnetcore).

If an exception is thrown in the application, the exception handler will create a response with the correct http status code and a meaningful error object that the 
api consumers can use to handle the error.

For exceptions that derive from **BaseException** the **Error** property is used as the response error object.
``` javascript
{
  "Id": "e4506b3e-1066-4f8e-bae2-336a0215e1a3",
  "Messages": [
    {
      "Key": "125",
      "Message": "VAT number invalid"
    },
    {
      "Key": "356",
      "Message": "Address invalid"
    },
    {
      "Key": "698",
      "Message": "Email invalid"
    }
  ]
}
```

For other exceptions a simple error object is created, only exposing the exception type.
``` javascript
{
  "Id": "e4506b3e-1066-4f8e-bae2-336a0215e1a3",
  "Messages": [
    {
      "Key": "",
      "Message": "Exception of type System.ArgumentNullException occurred. Check logs for more info."
    }
  ]
}
```

### Http status code mappings

It is possible to map exception types to specific http status codes. 
The default code is 500.  

Some exception types that are defined in the [error toolbox](https://github.com/digipolisantwerp/errors_aspnetcore) have default mappings predefined. For these exceptions it is not necessary to define the mappings in the configuration.

Exception type              | Http status code
------------------ | ----------------------------------------------------------- | --------------------------------------
NotFoundException              | 404 
ValidationException | 400
UnauthorizedException | 403  

Important note!
The default mappings will be overridden if you specify them in the mappings setup.

### Usage

To enable exception handling, call the **UseExceptionHandling** method on the **IApplicationBuilder** object in the **Configure** method of the **Startup** class.  
This must be the first call on the **IApplicationBuilder** object in the **Configure** method!

``` csharp
    app.UseExceptionHandling(mappings =>
    {
    });
    
    // !! other calls on the app object must be placed after the UseExceptionHandling call !!
``` 

To specify the mappings of exception types to http status codes you can use the **HttpStatusCodeMappings** object that is passed to the setupAction of the **UseExceptionHandling** method.

To add a new mapping, call the Add method and pass the exception type the http status code.

``` csharp
    app.UseExceptionHandling(mappings =>
    {
        mappings.Add(typeof(NotFoundException), 404);
    });
``` 

You can also use a generic method to specify the exception type.

``` csharp
    mappings.Add<NotFoundException>(404);
``` 

If you have a lot of mappings to configure and you don't want to place the code in the **Startup** code file you can use the **AddRange** method that accepts a mappings collection that will be added.


Define a collection of mappings somewhere:
``` csharp
    var customMappings = new Dictionary<Type, int>()
    {
        { typeof(NotFoundException), 404 },
        { typeof(ValidationException), 500 },
        { typeof(UnauthorizedException), 401 }
    };
``` 

and add it in the **setupAction**
``` csharp
    app.UseExceptionHandling(mappings =>
    {
        mappings.AddRange(customMappings);
    });
``` 

### Logging

The exception handler will also log the exception.
If the http status code is in range of 4xx the exception will be logged with a **Debug** level.
Exceptions with a status code in range 5xx will be logged as **Error** level.

The logged message is a json with following structure:

``` javascript
{
	"HttpStatusCode" : 404,
	"Error" : {
		//The Error object serialized as Json
	},
	"Exception" : {
		//The exception object serialized as Json
	}
}
```

For exceptions that do not derive from **BaseException** the **Error** property will be empty. 

