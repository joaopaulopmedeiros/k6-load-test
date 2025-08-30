# K6 Load Test

This repository provides a simple proof of concept for load testing an ASP.NET Core Restful API in a fully containerized environment.  

It demonstrates how to define and execute load tests using k6, with metrics stored in InfluxDB and visualized in Grafana.  
The project helps developers, especially those new to performance testing, to:
- Simulate realistic traffic and concurrent users against an API.  
- Measure key performance indicators such as latency, throughput, and error rate.  
- Monitor system behavior in real-time through Grafana dashboards.  
- Run reproducible tests in an isolated Docker environment with minimal setup.

## Local Setup
Use the following `make` commands:
```
=============================
Available commands:
=============================
down                      Stop containers
up                        Setup containers
load                      Run load test
```

## Contributing
Contributions are welcome!
Open issues for bugs, questions, or suggestions.
Submit pull requests with new test scripts, scenarios, or performance improvements.

## Results
While running the load test, you can open grafana at `localhost:3000` to visualize the result such as:
<img src="./docs/screenshot-grafana-dashboard.png" alt="Grafana Dashboard Screenshot"/>
