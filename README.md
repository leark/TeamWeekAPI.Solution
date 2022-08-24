# Seung Lee's Animal Gauntlet API

#### By _Seung Lee, Garrett Hays, Alex Shevlin, Matt Herbert_

#### _An API._

## Technologies Used

* _C#/.NET_
* _Entity Framework_
* _Identity_
* _JwtBearer_

## Description

A simple API.

## Setup/Installation Requirements
_Requires console application such as Git Bash, Terminal, or PowerShell_

1. Open Git Bash or PowerShell if on Windows and Terminal if on Mac
2. Run the command

    ``git clone https://github.com/leark/TeamWeekAPI.Solution.git``

3. Run the command

    ``cd TeamWeekAPI.Solution``

* You should now have a folder `TeamWeekAPI.Solution` with the following structure.
    <pre>TeamWeekAPI.Solution
    ├── .gitignore 
    ├── ... 
    └── TeamWeekAPI
        ├── Controllers
        ├── Models
        ├── ...
        ├── README.md
        └── Startup.cs</pre>

4. Add a file named appsettings.json in the following location, inside TeamWeekAPI folder 

    <pre>TeamWeekAPI.Solution
    ├── .gitignore 
    ├── ... 
    └── TeamWeekAPI
        ├── Controllers
        ├── Models
        ├── ...
        ├── Startup.cs
        └── <strong>appsettings.json</strong></pre>
      
5. Copy and paste below JSON text in appsettings.json.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "System": "Information",
      "Microsoft": "Information"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;database=teamweekapi;uid=root;pwd=[YOUR-PASSWORD-HERE];"
  },
  "JwtConfig": {
    "Secret" : "[YOUR-SECRET-HERE]"
  }
}

```

7. Replace [YOUR-PASSWORD-HERE] with your MySQL password.

8. Replace [YOUR-SECRET-HERE] with your random length 32 string.

9. Run the command

    ```dotnet ef database update```


<strong>To Run</strong>

Navigate to the following directory in the console
    <pre>TeamWeekAPI.Solution
    └── <strong>TeamWeekAPI</strong></pre>

Run the following command in the console

  ``dotnet build``

Then run the following command in the console

  ``dotnet run``

This program was built using _`Microsoft .NET SDK 6.0.4`_, and may not be compatible with other versions. Your milage may vary.

## API Documentation
#### Available Requests

#### Animals
| HTTP   | EndPoints         | Auth  | Description                             |
| ------ | ----------------- | ----- | --------------------------------------- |
| GET    | /api/Animals      | Anon  | Gets all animals in the database.       |
| POST   | /api/Animals      | Admin | Adds an animal to the database          |
| GET    | /api/Animals/{id} | Anon  | Gets an animal with animalId = {id}     |
| PUT    | /api/Animals/{id} | Admin | Updates an animal with animalId = {id}  |
| DELETE | /api/Animals/{id} | Admin | Deletes an animal with animalId = {id}  |

#### Teams
| HTTP   | EndPoints                | Auth   | Description                                                          |
| ------ | ------------------------ | ------ | -------------------------------------------------------------------- |
| GET    | /api/Teams               | Admin  | Gets all teams in the database.                                      |
| POST   | /api/Teams               | Player | Adds a team to the database                                          |
| GET    | /api/Teams/{id}          | Player | Gets a team with teamId = {id}                                       |
| PUT    | /api/Teams/{id}          | Player | Updates a team with teamId = {id}                                    |
| DELETE | /api/Teams/{id}          | Player | Deletes a team with teamId = {id}                                    |
| POST   | /api/Teams/{tId}/{aId}   | Player | Adds an animal with animalId to team with teamId                     |
| PUT    | /api/Teams/{tId}/{atId}  | Player | Updates an animalTeam with teamId = {tId} and animalTeamId = {atId}  |
| GET    | /api/Teams/battle/{tId}  | Player | Gets result of random battle team with teamId = {tId} performed      |
| GET    | /api/TeamAnimals/{tId}   | Player | Gets all the animals in team with teamId = {tId}                     |

#### AnimalTeams
| HTTP   | EndPoints                  | Auth   | Description                                     |
| ------ | ---------------------------| ------ | ----------------------------------------------- |
| GET    | /api/AnimalTeams/{teamId}  | Player | Gets all AnimalTeam with teamId = {teamId}      |
| DELETE | /api/AnimalTeams/{id}      | Player | Deletes an animalTeam with animalTeamId = {id}  |

#### AuthManagement
| HTTP   | EndPoints         | Auth   | Description                        |
| ------ | ----------------- | ------ | ---------------------------------- |
| POST   | /api/Register     | Player | Registers a player.                |
| POST   | /api/Login        | Player | Login as a player.                 |
| POST   | /api/RefreshToken | Player | Get new Token and RefreshToken     |

*Auth is minimum role required to use the endpoint
#### Example Query
Example uses hosted API so if building on own machine, replace ```https://slagapi.azurewebsites.net/``` with ```https://localhost:5000/```
```
https://slagapi.azurewebsites.net/api/animals/1
```
Expected JSON Response
```
{
  "animalId": 1,
  "image": "https://cdn.discordapp.com/attachments/1008839085172981781/1008883732104626246/musclepikachu.png",
  "name": "Pikachad",
  "hp": 3,
  "attack": 5
}
```
#### Example POST Request
```
https://slagapi.azurewebsites.net/api/animals/
```
Sample Request
```
{
  "image": "https://cdn.discordapp.com/attachments/1008839085172981781/1008930285691351131/MonionNoBgZoom.png",
  "name": "Monion",
  "hp": 6,
  "attack": 3
}
```

## Known Bugs

* _No known issues_

## License

[GNU](/LICENSE)

Copyright (c) 2022 Seung Lee, Garrett Hays, Alex Shevlin, Matt Herbert