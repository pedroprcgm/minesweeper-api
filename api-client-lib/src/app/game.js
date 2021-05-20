const httpClient = require("../infra/http-client");
const game = {};

/**
 * Create a game with name, number of rows, cols and mines
 * @param {string} name
 * @param {int} rows
 * @param {int} cols
 * @param {int} mines
 * @returns A promise with the data returned from MineSweeperAPI
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
			console.log(err);
		});
};

/**
 * Visit a cell
 * @param {string} gameId
 * @param {int} row
 * @param {int} col
 * @returns A promise with the result of visiting a cell
 */
game.visitCell = (gameId, row, col) => {
	return httpClient
		.put(`Games/${gameId}/rows/${row}/cols/${col}/visit-cell`, {})
		.then((response) => {s
			return response.data;
		})
		.catch((err) => {
			let error;
			if (err && err.response && err.response.data)
				error = err.response.data.title;
			else error = "Unexpected error";

			throw new Error(error);
		});
};

module.exports = game;
