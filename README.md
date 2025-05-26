# K6 Load Test
This repository addresses a proof of concept about load test in a docker environment.

## Local Setup
Use the following `make` commands:
```
=============================
Available commands:
=============================
down                      Stop containers
load                      Run load test
up                        Setup containers
```

## Results
- Execution of load test in k6;
- Automatic real time report of load test in grafana via influxdb;
- Metrics collected such as p95 response time.

<img src="./docs/screenshot-grafana-dashboard.png"/>
