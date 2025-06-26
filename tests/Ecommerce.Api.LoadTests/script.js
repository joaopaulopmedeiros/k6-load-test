import http from 'k6/http';
import { check, sleep } from 'k6';

const target_vus = 100;

export const options = {
  thresholds: {
    http_req_duration: ['avg<500', 'med<500', 'min<100', 'max<2000'],
  },
  stages: [
    { duration: "5s", target: target_vus },
    { duration: "50s", target: target_vus },
    { duration: "5s", target: 0 }
  ]
};

export default function () {
  const res = http.get('http://ecommerce-nginx:3333/products?page=1&size=1000&title=product');
  check(res, { 'status was 200': (r) => r.status == 200 });
  sleep(1);
}