# TaskManager
## End points - There are three endpoints in the Task Manager project
### GetAllTasks
- This is a GET requeet
- The endpoint url is (host)/api/task-management/GetAllTasks
- The live endpoint on Azure is https://tasksmanager20220815134021.azurewebsites.net/api/task-management/GetAllTasks
### AddTask
- AddTask: This is a POST requeet
- The endpoint url is (host)/api/task-management/AddTask
- The live endpoint on Azure is https://tasksmanager20220815134021.azurewebsites.net/api/task-management/AddTask
- The Task should be a JSON file in the body of the request
- The ID is auto generated in the database and it has been ignored from the payload
### UpdateTask
- UpdateTask: This is a POST requeet
- The endpoint url is (host)/api/task-management/UpdateTask
- The live endpoint on Azure is https://tasksmanager20220815134021.azurewebsites.net/api/task-management/UpdateTask
- The Task should be a JSON file in the body of the request
- The ID of the overload matches with the right record in the database and all parameters get updated
- In the future we can update the API to make sure it updates the provided fields 
## The Payload example for both AddTask and UpdateTask end points
{
    "Id": 107,
    "Name": "Clean house mate 2",
    "Description": "We should take the car to carwash",
    "DueDate": "2022-08-20T00:00:00",
    "StartDate": "2022-08-18T00:00:00",
    "EndDate": "2022-08-19T00:00:00",
    "Priority": 1,
    "Status": 1
}
## Database Settings
This is the connection string in the application: 
"ConnectionStrings": {"TaskDatabase": "Server=localhost\\SQLEXPRESS;Database=tasks;Trusted_Connection=True;User Id=DevUser;Password=Pa$$word!;"},
The username and password of the user are as above
The script creates the database and the table for the task manager
Feel free to create your username and password and update the connection string to access your database
  