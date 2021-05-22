const httpClient = require("../infra/http-client");
const game = {};

/**
 * Create a game with name, number of rows, cols and mines
 * @param {string} name
 * @param {number} rows
 * @param {number} cols
 * @param {number} mines
 * @param {string} token
 * @returns {promise} Game id
 */
game.create = (name, rows, cols, mines, token) => {
	return httpClient
		.post(
			"Games",
			{
				name: name,
				rows: rows * 1,
				cols: cols * 1,
				mines: mines * 1,
			},
			token
		)
		.then((response) => {
			return response.data;
		})
		.catch((err) => {
			handleError(err);
		});
};

/**
 * Get game by id
 * @param {string} gameId
 * @param {string} token
 * @returns {promise} Game details
 */
game.getById = (gameId, token) => {
	return httpClient
		.get(`Games/${gameId}`, token)
		.then((response) => {
			return response.data;
		})
		.catch((err) => {
			handleError(err);
		});
};

/**
 * Pause a game
 * @param {string} gameId
 * @param {string} token
 * @returns {promise} Bool indicating success
 */
game.pause = (gameId, token) => {
	return httpClient
		.put(`Games/${gameId}/pause`, {}, token)
		.then((response) => {
			return response.data;
		})
		.catch((err) => {
			handleError(err);
		});
};

/**
 * Resume a game
 * @param {string} gameId
 * @param {string} token
 * @returns {promise} Bool indicating success
 */
game.resume = (gameId, token) => {
	return httpClient
		.put(`Games/${gameId}/resume`, {}, token)
		.then((response) => {
			return response.data;
		})
		.catch((err) => {
			handleError(err);
		});
};

/**
 * Visit a cell
 * @param {string} gameId
 * @param {number} row
 * @param {number} col
 * @param {string} token
 * @returns {Promise} Result of visiting a cell
 */
game.visitCell = (gameId, row, col, token) => {
	return httpClient
		.put(`Games/${gameId}/rows/${row}/cols/${col}/visit-cell`, {}, token)
		.then((response) => {
			return response.data;
		})
		.catch((err) => {
			handleError(err);
		});
};

/**
 * Flag a cell
 * @param {string} gameId
 * @param {number} row
 * @param {number} col
 * @param {string} token
 * @returns {Promise} Result of flag a cell
 */
game.flagCell = (gameId, row, col, flag, token) => {
	return httpClient
		.put(
			`Games/${gameId}/rows/${row}/cols/${col}/flag`,
			{
				flag: flag * 1,
			},
			token
		)
		.then((response) => {
			return response.data;
		})
		.catch((err) => {
			console.log(err);
			handleError(err);
		});
};

let handleError = (err) => {
	console.log(err);
	let error;
	if (err && err.response) {
		if (err.response.status == 401) error = "Invalid token!";
		else error = err.response.data.title;
	} else error = "Unexpected error";

	throw new Error(error);
};

module.exports = game;
