# Linq Assessment - Flight Route API

This is a .NET Web API solution that provides functionality for finding optimal flight routes between locations.

## Features

- User authentication (registration, login, logout)
- Find routes with minimum exchanges
- Find routes with minimum total price
- Find routes with minimum total duration
- Paginated list of all journeys
- Secure storage of user credentials
- Persistent storage of flight and route data

## Prerequisites

- .NET 9.0 SDK 
- SQL Server (or SQL Server Express)
- Visual Studio 2022 or VS Code

## Setup Instructions

1. Clone the repository
2. Navigate to the solution directory
3. Update the connection string in `appsettings.json` if needed
4. Run the following commands:

```bash
dotnet restore
dotnet build
dotnet run --project LinqAssessment.API
```

The API will be available at `https://localhost:7027` and `http://localhost:5200`

## API Endpoints

### Authentication
- POST `/api/auth/register` - Register a new user
- POST `/api/auth/login` - Login
- POST `/api/auth/logout` - Logout

### Routes
- GET `/api/routes/minimize-exchanges` - Find routes with minimum exchanges
- GET `/api/routes/minimize-price` - Find routes with minimum total price
- GET `/api/routes/minimize-duration` - Find routes with minimum total duration
- GET `/api/routes/all` - Get all journeys with pagination

## Testing

You can test the API using the provided Postman collection (`LinqAssessment.postman_collection.json`). The collection includes examples for all endpoints.

### Testing Steps

1. Import the Postman collection
2. Set up environment variables:
   - `baseUrl`: `https://localhost:5001`
   - `token`: (will be set after login)

3. Test the endpoints in this order:
   a. Register a new user
   b. Login to get the JWT token
   c. Use the token to test the route endpoints

### Example Requests

#### Register
```http
POST /api/auth/register
Content-Type: application/json

{
    "username": "testuser",
    "password": "Test123!",
    "email": "test@example.com"
}
```

#### Login
```http
POST /api/auth/login
Content-Type: application/json

{
    "username": "testuser",
    "password": "Test123!"
}
```

#### Get Routes with Minimum Exchanges
```http
GET /api/routes/minimize-exchanges?origin=A&destination=C&ascending=true
Authorization: Bearer <your-token>
```

#### Get Routes with Minimum Price
```http
GET /api/routes/minimize-price?origin=A&destination=C&departure=+1 day 5 hour&ascending=true
Authorization: Bearer <your-token>
```

#### Get Routes with Minimum Duration
```http
GET /api/routes/minimize-duration?origin=A&destination=C&departure=+1 day 5 hour&ascending=true
Authorization: Bearer <your-token>
```

#### Get All Journeys
```http
GET /api/routes/all?page=1&size=10&ascending=true
Authorization: Bearer <your-token>
```

## Data Structure

The API uses two main data types:

1. Routes - Direct connections between locations
2. Flights - Travel options on routes with provider, price, and departure time

Sample data is provided in the `Data/SampleDataSeeder.cs` file.

## Security

- Passwords are hashed using BCrypt
- JWT tokens are used for authentication
- All sensitive endpoints require authentication
- CORS is configured to allow all origins (can be restricted in production)

## Error Handling

The API returns appropriate HTTP status codes and error messages:
- 200: Success
- 400: Bad Request (invalid input)
- 401: Unauthorized (missing or invalid token)
- 404: Not Found
- 500: Internal Server Error 
