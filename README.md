# JobTarget Backend

This is a .NET Core web application that provides APIs for managing job data.

## Features
- API to fetch all jobs from `jobs.json`.
- API to fetch a job by its ID.

## Endpoints
- **GET** `/api/jobs` - Retrieves all jobs.
- **GET** `/api/jobs/{id}` - Retrieves a specific job by ID.

## Technologies Used
- .NET Core
- C#
- JSON file as a data source

## Setup Instructions
1. Clone the repository:
   ```bash
   git clone https://github.com/Rocky074111/JobTarget_Backend.git
2. Open the project in Visual Studio.
3. Update the appsettings.json file with the path to jobs.json.
4. Run the application

## Future Improvements
Add a database for scalability.
Implement authentication and authorization.
Enhance error handling and logging.

## Folder Structure
- `Controllers/` - API controllers.
- `Services/` - Business logic services.
- `Models/` - Data models.
- `DTOs/` - Data Transfer Objects.
- `Properties/` - Application settings.

