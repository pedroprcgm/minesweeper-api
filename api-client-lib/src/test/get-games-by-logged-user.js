let mineSweeperAPIClient = require("../../index");

let args = process.argv.slice(2);

let token = args[0];

mineSweeperAPIClient
	.getGamesByLoggedUser(token)
	.then((games) => console.log(games))
	.catch((error) => console.log(error));