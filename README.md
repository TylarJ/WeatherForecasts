# Weather Forecast API

## Overview
This is a **RESTful API** backed by a database, designed to store and retrieve **weather forecasts** based on latitude and longitude. The API integrates with [Open-Meteo](https://open-meteo.com/) to fetch real-time weather data and supports operations to **add, update, delete, and list locations**.

Focus was given to building a well-structured and maintainable solution. The system was designed with clean architecture principles, ensuring separation of concerns, modular components, and efficient data flow.

## Features
- **Store weather forecasts** based on latitude & longitude.
- **Retrieve stored locations** and get the latest forecast.
- **Prevent duplicate latitude/longitude entries** in the database.
- **Update forecasts for an existing location**.
- **Delete locations when they’re no longer needed**.

## Tech Stack
- **C# / .NET**
- **Entity Framework Core**
- **SQL (in memory for the purpose of this example)**
- **xUnit and Moq for unit testing**
- **Open-Meteo API**

## Setup & Installation

### 1. Clone the Repository
```sh
git clone https://github.com/TylarJ/WeatherForecasts
cd WeatherForecasts
```

### 2. Install Dependencies
```sh
dotnet restore
```

### 3. Run the API
```sh
cd WeatherForecasts.Web
dotnet run
```

### 4. Access the API
Open your browser or API client and navigate to:
```
http://localhost:5267/swagger/index.html
```

## Unit Testing
Run the test suite to validate functionality:
```sh
dotnet test
```

## Improvements & Future Enhancements
- Pagination support for listing locations.
- Caching for reducing API calls to Open-Meteo.
- Extended weather data such as precipitation & wind conditions.
- Fully implement DTOs to prevent returning EF entities directly.
- Improve database mocking for unit tests to enhance isolation and reliability.
- Improve handling of results from services, including errors.

---