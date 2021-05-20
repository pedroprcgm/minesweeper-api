const axios = require("axios");

// TODO: Should use env
const baseUrl = "http://localhost:5000";
const httpClient = {};

httpClient.post = (url, data) => {
	return axios.post(baseUrl + "/" + url, data);
};

httpClient.put = (url, data) => {
	return axios.put(baseUrl + "/" + url, data);
};

module.exports = httpClient;
