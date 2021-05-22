let mineSweeperAPIClient = require("../../index");

let args = process.argv.slice(2);

let email = args[0];
let password = args[1];

mineSweeperAPIClient
	.createUser(email, password)
	.then((result) => console.log(result))
	.catch((error) => console.log(error));