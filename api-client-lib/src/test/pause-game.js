let mineSweeperAPIClient = require("../../index");

let args = process.argv.slice(2);

let gameId = args[0];
let token = args[1];

mineSweeperAPIClient
	.pause(gameId, token)
	.then((game) => console.log(game))
	.catch((error) => console.log(error));
