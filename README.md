# K6 Load Test
This repository addresses a proof of concept about load test in a docker environment.

## How to get service up and running
```
./run.sh up
```

## How to start load test
```
./run.sh loadtest
```

## How to clean up
```
./run.sh down
```

# Results
- Execution of load test in k6;
- Automatic real time report of load test in grafana via influxdb;
- Metrics collected such as p95 response time.

<img src="./docs/screenshot-grafana-dashboard.png"/>
