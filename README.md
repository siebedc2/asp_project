# ASP-project

- [Opdracht](#opdracht)
  - [MongoDB Initializen](#mongodb-initializen)
  - [Swagger](#swagger)
  - [Domainmodel](#domainmodel)


## MongoDB Initializen ##

Om dit project te runnen moet je MongoDB geïnstalleerd hebben op je lokale machine.
- Zie hier: https://docs.mongodb.com/manual/installation/

De lokale Mongo instantie moet opgezet worden op 

> mongodb://localhost:27017

Dit is de default port voor MongoDB. Eens je het project opstart met "dotnet run" zal er een database "ShelterDB" aangemaakt worden. Je kan deze database bekijken in MongoDB atlas of Robo3T.


## Swagger ##
Swagger documentatie kan je vinden op de /swagger url na het runnen van het project via `dotnet run`.

## Domainmodel ##

> Hou rekening met de namespace! Je kan dit model voorlopig testen door de `Program.cs` wat objecten te laten aanmaken; later gaan we deze integreren via ASP.NET.

- Een dierenasiel (`Shelter`) heeft een lijst van dieren (`Animals`).
- Een dierenasiel heeft medewerkers (`Employees`).
- Het systeem kent de volgende rollen/medewerkers: `Manager`, `Caretaker`, `Administrator`.
- Elke medewerker met de rol `Caretaker` staat in voor één of meedere dieren.
- Een dier kan ofwel een `Dog`, `Cat` of `Other` zijn.
  - Alle dieren hebben een `Name`, `DateOfBirth`, `IsChecked`, `KidFriendly` en `Since` (datetime)
  - Een kat heeft `Race`, `Declawed`.
  - Een hond heeft `Race`, `Barker`
  - Andere heeft `Description`, `Kind`
