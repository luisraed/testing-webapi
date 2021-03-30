# testing-webapi
 Small example about how to test an endpoint using SpecFlow
 
 # Solution projects
 
 # InventoryManagementApi
 
 The Web API Project configured to run at http://localhost:8888
 
 # InventoryManagementApiUnitTests
 
 The Unit Tests Project for the Web API classes.
 
 # InventoryManagementDto

Project to share the API Request/Response objects

# InventoryManagementAcceptanceTests

Acceptance Tests to verify the API behavior using Specflow.

To run the acceptance tests you can open the solution in 2 Visual Studio simultaneous instances:

- The first one should be used to execute the API (InventoryManagementApi)
- The second one can be used to run the Specflow scenarios against the API.