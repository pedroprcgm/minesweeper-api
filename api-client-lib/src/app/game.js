const httpClient = require('../infra/http-client');
const game = {};

game.create = (name, rows, cols, mines) => {
    console.log(name, rows, cols, mines);
    
    httpClient.post('Games', { 
        name: name,
        rows: rows,
        cols: cols,
        mines: mines
    })
    .then(response => {
        console.log(response);
    })
    .catch(err => console.log(err));
};

game.visitCell = (gameId, row, col) => {
	console.log(gameId, row, col);
};

module.exports = game;