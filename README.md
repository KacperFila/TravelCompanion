# About Travel Companion! ⛰️

Travel Companion is an .NET/Angular web application focused on creating, managing and documenting trips with your friends. It allows you to create travel plans which could be edited by your friends which makes managing travels easier!

## Features:

1. Accounts
2. Plans (could modify, accept or reject suggestions)
3. Travels (could browse, set already visited points, rate)
4. Invitations (users could be invited to participate in a plan)
5. Sharing travel costs with specific users
6. Toast notifications using SignalR

## Technology used:

- ASP .NET Core
- PostgreSQL
- Docker
- Entity Framework
- SignalR

## How to run?

1. Create an empty folder and type: git clone https://github.com/KacperFila/TravelCompanion.BE.git
2. Make sure you're running Docker Desktop
3. Move into directory by typing: `cd TravelCompanion`
4. To run local db move into local-db directory by typing: `cd local-db`
5. Type: `docker-compose up -d`
6. Move up typing: `cd..`
7. Type `docker build -t travelcompanion-api . && docker run -d -p 5000:5000 --name travelcompanion-api travelcompanion-api` (or similar for your terminal)
8. Swagger documentation is available under `localhost:5000/docs`.
9. PgAdmin is available under `localhost:5050`
10. Postgres is available under `localhost:5432`
