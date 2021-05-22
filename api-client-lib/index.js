const game = require("./src/app/game");
const user = require("./src/app/user");

exports.createGame = game.create;

exports.getById = game.getById;

exports.pause = game.pause;

exports.resume = game.resume;

exports.visitCell = game.visitCell;

exports.flagCell = game.flagCell;

exports.createUser = user.create;

exports.login = user.login;

exports.getGamesByLoggedUser = user.getGamesByLoggedUser;
