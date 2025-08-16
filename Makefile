COMPOSE_FILE=./docker-compose.yml

.PHONY: up loadtest down

up:
	@echo "Starting Docker Compose..."
	docker compose -f $(COMPOSE_FILE) up -d

loadtest:
	@echo "Verifying API's health check..."
	@while true; do \
		content=$$(curl -sSf http://localhost:3333/health || true); \
		if [ "$$content" = "Healthy" ]; then \
			echo "API is healthy."; \
			break; \
		else \
			echo "API is not healthy yet. Retrying in 5 seconds..."; \
			sleep 5; \
		fi; \
	done
	@echo "Starting Load Test..."
	@echo "Reports on: http://localhost:3000/d/k6/k6-load-testing-results"
	docker compose -f $(COMPOSE_FILE) run ecommerce-k6-load-test run //scripts//script.js

down:
	@echo "Stopping Docker Compose..."
	docker compose -f $(COMPOSE_FILE) down --volumes --rmi all
