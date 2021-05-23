# MineSweeper API
An API for minesweeper game including a client lib to API in NodeJS.

# Steps to run / access

You can access the published API on Heroku by the link: https://mnswpr-api.herokuapp.com/swagger/index.html.

Also you can access the open the .sln file on the root in Visual Studio. 
You can run using native VS function or using docker (already there's a Docker file).

# What to build

The following is a list of items (prioritized from most important to least important) we wish to see:

- [x] Design and implement a documented RESTful API for the game (think of a mobile app for your API)
- [x] Implement an API client library for the API designed above. Ideally, in a different language, of your preference, to the one used for the API
- [x] When a cell with no adjacent mines is revealed, all adjacent squares will be revealed (and repeat)
- [x] Ability to 'flag' a cell with a question mark or red flag
- [x] Detect when game is over
- [x] Persistence
- [x] Time tracking
- [x] Ability to start a new game and preserve/resume the old ones
- [x] Ability to select the game parameters: number of rows, columns, and mines
- [x] Ability to support multiple users/accounts

# Developer decisions

- Project structure
    - The project is created using a DDD structure because I think that the game has enough complexity to build a domain that we already knew

- MongoBD instead of SQL Server (or any relational database)
    - While thinking in the game structure, with game information and cells information, I guess that will be more eficient use a non-relational database to store the cells since the cells are created the same moment that game is created

- Cells actions above GameController
    - I could create a specific controller to Cells but I thought that wasn't necessary because the strong Entity on this relation is the Game and the Cell hasn't meaning out of a game.

- Time tracking
    - I builded time tracking on the simplest way I found. I thought about create time tracking using a new class to store the pause/resume events but it would add more complexity to the project and it wasn't a specific requirement to store the pause/resume history. So I decided to build the requirements simplest possible.

- There's on `docs` folder a simple image explaining the solution structure.

- Also in `docs` folder there's a Excel file with my time tracking to build this test.

# Technical debits (could be done)

- Testing on API
    - It's missing Unit and Integration tests, at least to visit cell action that is a important action and with a lot of code.

- Testing on NodeJS project

- Logging and monitoring
