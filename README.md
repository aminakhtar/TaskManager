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
