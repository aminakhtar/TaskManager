# TaskManager
## End points - There are three endpoints in the Task Manager project
### GetAllTasks
- This is a GET requeet
- The endpoint url is (host)/api/task-management/GetAllTasks
### AddTask
- AddTask: This is a POST requeet
- The endpoint url is (host)/api/task-management/AddTask
- The Task should be a JSON file in the body of the request
- The ID is auto generated in the database and it has been ignored
### UpdateTask
- UpdateTask: This is a POST requeet
- The endpoint url is (host)/api/task-management/UpdateTask
- The Task should be a JSON file in the body of the request
- The ID of the overload matches with the right record in the database and all parameters get updated
## Payload Example
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