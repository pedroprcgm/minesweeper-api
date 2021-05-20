const game = require("./src/app/game");

exports.createGame = game.create;

exports.getById = game.getById;

exports.pause = game.pause;

exports.resume = game.resume;

exports.visitCell = game.visitCell;
