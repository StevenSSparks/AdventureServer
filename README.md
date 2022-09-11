# AdventureServer
Text Adventure Framework inside a .NET 6 WebAPI. Sample Adventure is included. 

* Origionally coded in .Net Core 2.1 then compleatly refactored into .Net 6.0 with some C# version 9 touches
* Designed to run a generic text adventure that is stored in JSON inside the code
* Adventure House Game is based on a Microsoft PowerShell demo project but has been enhancesd and expanded

# Source Projects includes
* Welcome controller that returns html to explain the project 
* PlayAdventure Controller 
  * Welcome Page to setup the adventure 
  * Client page that calls the AdventureContrller to play the game
* AdventureController 
  * Swagger API that can be consumed by any HTTP Client
  * Depends on allowing a session variable to store the unique instance ID for each adventure 
  * Works with other clients like PlayAdv
