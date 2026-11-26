.PHONY: up down test help

# Default local stack: Postgres + web (COMPOSE_PROFILES=dev via .env)
up:
	docker compose up --build

down:
	docker compose down

# Unit + integration tests against Postgres (replaces .env profile; does not start web)
test:
	COMPOSE_PROFILES=test docker compose up --build --abort-on-container-exit --exit-code-from tests

help:
	@echo "make up    - run Postgres + web app"
	@echo "make down  - stop containers"
	@echo "make test  - run unit + integration tests in Docker"
