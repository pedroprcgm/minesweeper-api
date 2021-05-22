const httpClient = require("../infra/http-client");
const user = {};

/**
 * Create a user with email and password
 * @param {string} email
 * @param {string} password
 * @returns {promise} Bool indicating success
 */
user.create = (email, password) => {
	return httpClient
		.post("Users", {
            email: email,
            password: password
		})
		.then((response) => {
			return response.data;
		})
		.catch((err) => {
            console.log(err)
			handleError(err);
		});
};

/**
 * Login a user
 * @param {string} email
 * @param {string} password
 * @returns {promise} Token to authorization
 */
user.login = (email, password) => {
	return httpClient
		.post("Users/login", {
            email: email,
            password: password
		})
		.then((response) => {
			return response.data;
		})
		.catch((err) => {
			handleError(err);
		});
};

/**
 * Get games created by logged user
 * @param {string} token Authorization token for user
 * @returns {promise} List of games
 */
user.getGamesByLoggedUser = (token) => {
	return httpClient
		.get("Users/logged/games", token)
		.then((response) => {
			return response.data;
		})
		.catch((err) => {
			handleError(err);
		});
};


let handleError = (err) => {
	let error;
	if (err && err.response && err.response.data)
		error = err.response.data.title;
	else error = "Unexpected error";

	throw new Error(error);
};

module.exports = user;