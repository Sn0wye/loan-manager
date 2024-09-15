# Loan Manager

This project consists of two microservices, `Core` and `Risk`, built with ASP.NET Core, along with an instance of SQL Server. The services are orchestrated using Docker Compose for easy setup and deployment.

# Running the project

Make sure you have Docker and Docker Compose installed on your machine.

1. Clone the repository.

2. Run docker compose to start the services

```bash
docker-compose up -d
```

3. Access the Core service at `http://localhost:8080`.

# Stopping the project

```bash
docker-compose down
```
