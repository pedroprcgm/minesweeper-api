const httpClient = require("../infra/http-client");
const game = {};

/**
 * Create a game with name, number of rows, cols and mines
 * @param {string} name
 * @param {number} rows
 * @param {number} cols
 * @param {number} mines
 * @returns {promise} Game id
 */
game.create = (name, rows, cols, mines) => {
	return httpClient
		.post("Games", {
			name: name,
			rows: rows * 1,
			cols: cols * 1,
			mines: mines * 1,
		})
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
 * @returns {promise} Game details
 */
game.getById = (gameId) => {
	return httpClient
		.get(`Games/${gameId}`)
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
 * @returns {promise} Bool indicating success
 */
game.pause = (gameId) => {
	return httpClient
		.put(`Games/${gameId}/pause`)
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
 * @returns {promise} Bool indicating success
 */
game.resume = (gameId) => {
	return httpClient
		.put(`Games/${gameId}/resume`)
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
 * @returns {Promise} Result of visiting a cell
 */
game.visitCell = (gameId, row, col) => {
	return httpClient
		.put(`Games/${gameId}/rows/${row}/cols/${col}/visit-cell`, {})
		.then((response) => {
			s;
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
 * @returns {Promise} Result of flag a cell
 */
game.flagCell = (gameId, row, col, flag) => {
	return httpClient
		.put(`Games/${gameId}/rows/${row}/cols/${col}/flag`, {
			flag: flag * 1,
		})
		.then((response) => {
			return response.data;
		})
		.catch((err) => {
			console.log(err);
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

module.exports = game;
