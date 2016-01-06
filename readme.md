# WebApi Toolbox

<br>
De WebApi Toolbox biedt een aantal algemene functionaliteiten aan die kunnen gebruikt worden in ASP.NET 5 Web Api projecten

- versioning.
- base classes die veel gebruikte functionaliteiten encapsuleren.
- root ojbect formatter voor koppelingen met de ESB.
- query string mapping.

<br>
## Installatie
De toolbox toevoegen aan een project kan gedaan worden via de NuGet Package Manager in Visual Studio of door het package toe te voegen aan de project.json :

``` json
 "dependencies": {
    ...,
    "Digipolis.WebApi":  "1.0.4", 
    ...
 }
```
<br>
## Versioning

Het versioning framework zorgt voor een extra endpoint op de Web Api waar het versienummer van de toepassing kan opgevraagd worden. Standaard is dit endpoint voorzien op url **_admin/version_**, maar deze kan tijdens het opstarten een andere waarde gegeven worden.

<br>
Het framework wordt toegevoegd aan een ASP.NET 5 project in de **_Startup_** class. Eerst worden de nodige services geconfigureerd in de ConfigureServices method van de Startup class :

``` csharp
  services.AddWebApiVersioning(WebApiVersioningOptions.Default);
```
Als je tevreden bent met de standaard opties, geef je **_WebApiVersioningOptions.Default_** mee. Eigen opties instellen, kan door een nieuw geïnstantieerd WebApiVersioningOptions object mee te geven :

``` csharp
   app.AddWebApiVersioning(new WebApiVersioningOptions() { Route = "api/mijnversie" });
```

Volgende opties kunnen ingesteld worden :

Optie              | Omschrijving                                                | Default
------------------ | ----------------------------------------------------------- | --------------------------------------
Route              | de url waar de lijst van codetabellen kan opgevraagd worden | admin/version

Daarna wordt in de Configure method van de Startup class, het framework opgestart.

``` csharp
   app.UseWebApiVersioning();
```


<br>
## Base classes

Info volgt nog.


<br>
## RootObjectFormatters

Info volgt nog.


<br>
## QueryStringFormatting

Info volgt nog.



<br>
## Versies

| Versie | Auteur                                  | Omschrijving
| ------ | ----------------------------------------| ----------------------------------------------------
| 1.0.0  | Steven Vanden Broeck                    | Initiële versie.
|        |                                         |  
