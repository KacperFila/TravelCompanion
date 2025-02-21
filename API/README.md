# About Travel Companion! ⛰️

Travel Companion is an .NET/Angular web application focused on creating, managing and documenting trips with your friends. It allows you to create travel plans which could be edited by your friends which makes managing travels easier! 

## Features:
1. Accounts
2. Plans (could modify, accept or reject suggestions)
3. Travels (could browse, set already visited points, rate)
4. Invitations (users could be invited to participate in a plan)
5. Postcards from travel points
6. Sharing travel costs with specific users
7. Simple real-time notifications using SignalR
8. Sending emails using MailKit
9. Simple background jobs using Hangfire

## Technology used:
- ASP .NET Core
- PostgreSQL
- Docker
- Entity Framework
- Hangfire
- SignalR

## How to run?
1. Create an empty folder and type: git clone https://github.com/KacperFila/TravelCompanion.git
2. Move into directory by typing: cd TravelCompanion
3. Run docker container by typing: docker-compose up -d
4. Move into src/Bootstrapper/TravelCompanion.Bootstrapper
5. Start project by typing: dotnet run
  
## Documentation
Swagger documentation is available under localhost:5000/docs.
Hangfire dashboard is available under localhost:5000/hangfire.
