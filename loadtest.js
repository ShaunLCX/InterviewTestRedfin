import http from 'k6/http';
import { sleep } from 'k6';

export default function() {
  let response = http.get('http://localhost:8000/randomhash/request-hash');
  if (response.status === 200) {
    console.log(`Hash generated: ${response.body}`);
  } else {
    console.error(`Error generating hash: ${response.status}`);
  }
  sleep(1000);
}