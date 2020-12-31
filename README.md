# BattleShip
This is a simple one sided version of the BattleShip Game (https://www.youtube.com/watch?v=q0qpQ8doUp8). When the game starts, it creats a 10 x 10 OceanGrid and randomly positions 2 Destroyers and 1 BattleShip on the OceanGrid. You are now prompted to enter an attack coordinate which ranges from `A1 --> (1, 1)` to `J10 --> (10, 10)`. For example `C2` translates to `(2, 3)` on the OceanGrid. When you dont hit any ship, the game returns a `Misses` status, but when you hit a ship, the game returns a `Hits` status, also when you sink one ship, the game returns a `Sinks` status, then when the last hit sinks the final floating ship, the game ends and returns a `SinkedAllShips` status, it also prompts you to hit any button to close the console terminal. 

# Structure
The Solution Consits of 3 projects:  
BattleShip.Test : This contains all the unitTests for the game.  
BattleShip.Client: This is a console application and the entry point of the application.  
Battle.Core : This Contains all the game logic and models.
# How to run
To run the game you need to have dotnet core(https://dotnet.microsoft.com/download) installed, Then navigate to the `BattleShip.Client` folder and execute `dotnet run`. If you have visual studio, you can set the `BattleShip.Client` project as the startup project and then start the app.

# Running the UnitTests
To run the unitTest, navigate to `BattleShip.Tests` folder and execute `dotnet test`.
