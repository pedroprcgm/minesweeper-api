let mineSweeperAPIClient = require("../../index");

let args = process.argv.slice(2);

let gameId = args[0];

mineSweeperAPIClient
	.pause(gameId)
	.then((game) => console.log(game))
	.catch((error) => console.log(error));
