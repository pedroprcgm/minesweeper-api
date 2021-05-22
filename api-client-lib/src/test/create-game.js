let mineSweeperAPIClient = require("../../index");

let args = process.argv.slice(2);

let name = args[0];
let rows = args[1];
let cols = args[2];
let mines = args[3];
let token = args[4];

mineSweeperAPIClient
	.createGame(name, rows, cols, mines, token)
	.then((gameId) => console.log(gameId))
	.catch((error) => console.log(error));
