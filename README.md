# Bixi Staton Data (Equisoft Full Stack Developer Test)

This project is a full-stack application that fetches and displays Bixi bike station information.

- *Backend*: ASP.NET Core Web API (.NET 8)
- *Frontend*: React (Bootstrap for UI)
- *Data Source*: Two public JSON APIs providing station metadata and status

## Features 
-Filter data by: 
  - Station name
  - Minimum Available bikes range
  - Minimum Available docks range
  - eBike availability

-Sort By Station Name, Available Bikes, Available Docks, Availability and Last Updated Time.
-Pagination, each pages contains 10 items.

## API structure (Backend)
Since the data structure is relatively simple and involves fewer than 1000 records from the API, I chose to keep everything within a single web project. This includes service classes and DTOs, which I organized into separate folders for clarity.
In more complex projects—especially those that interact directly with a database, it is best practice to follow a layered architecture. This typically involves:
A Data Models layer for representing database entities
A DTO (Data Transfer Object) layer for shaping the data sent to and from the API
A Repository layer that handles all data access logic
A Service layer that acts as an intermediary between repositories and controllers
In this specific case, the service class simply consumes two external APIs, joins their data, and prepares the result required by the UI for displaying station information. To keep the service layer focused on data handling, I performed filtering and sorting in the controller.
However, in larger applications, filtering and sorting are better handled in the service layer to promote reusability and separation of concerns.
As for pagination, since the dataset is under 1000 records, I implemented client-side paging in React. For larger datasets, it would be more efficient to implement server-side pagination within the API service to reduce data transfer and improve performance.



## Frontend (React)
- `components/StationList.jsx`
- `services/stationService.js`
- Uses `fetch` for API calls
- Bootstrap UI with filter inputs, table view, and pagination

## Getting Started

-Run Backend 

```bash
cd Equisoft_Bixi.WebApi
dotnet build
dotnet run
```

Ensure CORS is enabled for React port (e.g., 3000).

-Run Fronend

```bash
cd bixi-ui
npm install
npm start
```


## Ambiguous Feature Interpretation

#### Problem: The public API may return outdated or incomplete station data.
#### Proposed Solution (Not Yet Implemented Due to Time Constraints):
To ensure data accuracy, the service layer can include validation logic to check the integrity of the received data. Additionally, by comparing the lastUpdated timestamp against a predefined threshold, we can detect stale data. If the data is outdated or invalid, the UI can notify the user with a warning message and provide a refresh icon to manually trigger data reload from the API.

