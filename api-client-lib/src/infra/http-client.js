const axios = require("axios");

// TODO: Should use env
const baseUrl = "http://localhost:5000";
const httpClient = {};

httpClient.post = (url, data, token) => {
	let header = null;
	if (token) header = { headers: { Authorization: `Bearer ${token}` } };

	return axios.post(baseUrl + "/" + url, data, header);
};

httpClient.put = (url, data, token) => {
	let header = null;
	if (token) header = { headers: { Authorization: `Bearer ${token}` } };

	return axios.put(baseUrl + "/" + url, data, header);
};

httpClient.get = (url, token) => {
	let header = null;
	if (token) header = { headers: { Authorization: `Bearer ${token}` } };

	return axios.get(baseUrl + "/" + url, header);
};

module.exports = httpClient;
