const axios = require('axios').create({
    httpsAgent: new https.Agent({  
      rejectUnauthorized: false
    })
  });

// TODO: Should use env
const baseUrl = 'https://localhost';
const port = 44332;
const httpClient = {};

httpClient.post = (url, data) => {
    return axios.post('/' + url, data, { proxy: { host: baseUrl, port: port } });
};

httpClient.put = (url, data) => {
    return axios.put(baseUrl + url, data);
};

module.exports = httpClient;