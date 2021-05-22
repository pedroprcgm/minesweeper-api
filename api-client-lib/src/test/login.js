let mineSweeperAPIClient = require("../../index");

let args = process.argv.slice(2);

let email = args[0];
let password = args[1];

mineSweeperAPIClient
	.login(email, password)
	.then((token) => console.log(token))
	.catch((error) => console.log(error));