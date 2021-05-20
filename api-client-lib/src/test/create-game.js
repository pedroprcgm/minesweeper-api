let mineSweeperAPIClient = require("../../index");

let args = process.argv.slice(2);

let name = args[0];
let rows = args[1];
let cols = args[2];
let mines = args[3];

mineSweeperAPIClient
	.createGame(name, rows, cols, mines)
	.then((gameId) => console.log(gameId))
	.catch((error) => console.log(error));
