let mineSweeperAPIClient = require("../../index");

let args = process.argv.slice(2);

let gameId = args[0];
let row = args[1];
let col = args[2];
let token = args[3];

mineSweeperAPIClient
	.visitCell(gameId, row, col, token)
	.then((visitResult) => console.log(visitResult))
	.catch((error) => console.log(error));
