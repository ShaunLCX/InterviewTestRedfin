const axios = require('axios');
const { RateLimiter } = require('limiter');
const https = require('https');

const limiter = new RateLimiter({ tokensPerInterval: 1, interval: 1000 });
const numRequests = 10;

async function sendRequest() {
  try {
    const response = await axios.get('https://localhost:7193/request-hash', {
      headers: { 'Content-Type': 'application/json' },
      httpsAgent: new https.Agent({ rejectUnauthorized: false })
    });
    console.log('Request successful!');
    console.log('Hash:', response.data);
  } catch (error) {
    console.error('Error sending request:', error);
  }
}

async function runLoadTest() {
  for (let i = 0; i < numRequests; i++) {
    await limiter.removeTokens(1);
    await sendRequest();
  }
}

runLoadTest();
