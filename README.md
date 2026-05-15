# GameStore

This is a full-stack web application for a game store. It includes a .NET Web API backend and a React frontend.

## Technologies Used

### Backend
- C# with .NET 10
- ASP.NET Core for the Web API
- Entity Framework Core for data access
- SQLite as the database

### Frontend
- React 19
- TypeScript
- Vite for the build tooling
- Bootstrap for styling
- React Router for navigation

## Project Structure

The repository is organized into two main projects:

- `GameStore.Api/`: The backend .NET solution.
- `GameStore.React/`: The frontend React application.

## How to Run the Project

### Prerequisites
- .NET 10 SDK
- Node.js (v18 or later recommended)

### Backend Setup
1. Navigate to the `GameStore.Api` directory:
   ```sh
   cd GameStore.Api
   ```
2. Restore the .NET dependencies:
   ```sh
   dotnet restore
   ```
3. Apply the database migrations:
   ```sh
   dotnet ef database update
   ```
4. Run the backend server:
   ```sh
   dotnet run
   ```
The API will be running on `https://localhost:5001` (or a similar port).

### Frontend Setup
1. Navigate to the `GameStore.React` directory:
   ```sh
   cd GameStore.React
   ```
2. Install the Node.js dependencies:
   ```sh
   npm install
   ```
3. Run the frontend development server:
   ```sh
   npm run dev
   ```
The React application will be available at `http://localhost:3000` (or a similar port) and will connect to the backend API.

