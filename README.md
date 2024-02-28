# Task 1

Develop a .NET Core 6 application that integrates with Azure Functions and Azure Storage (both cloud and local emulator options for storage) to periodically fetch data from a specified API and manage logging and data retrieval. The application should:
Utilize Azure Functions to trigger every minute, requesting data from the "https://api.publicapis.org/random?auth=null" endpoint.
Record each attempt (both successful and unsuccessful) in Azure Table Storage, including details for tracking and review.
Save the complete payload received from the API call into Azure Blob Storage for persistence.
Implement a GET API endpoint to enumerate logs within a specified time range (start to end), allowing users to review the history of data fetch attempts.
Provide a GET API endpoint to retrieve the full payload data from a specific log entry stored in the blob, based on log identifiers or criteria.
Ensure the entire codebase is published on GitHub, making it publicly accessible for review, contributions, or deployment
