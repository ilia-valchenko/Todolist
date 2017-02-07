(function () {
    var todoListModule = angular.module('TodoList', ['Repository']);

    todoListModule.controller('TodoListController', ['$scope', '$http', 'Tasks', function ($scope, $http, Tasks) {

        $scope.tasks = [];
        var isExist = false;
        
        function Init() {
            Tasks.getAll().then(function (response) {
                var objectsFromJson = response.data;

                for (var i = 0; i < objectsFromJson.length; i++)
                    $scope.tasks.push(objectsFromJson[i]);
            }), function (response) {
                // error
                initializeAndOpenErrorPopup(response);
            };
        };

        // Initialize application
        Init();

        $scope.submitNewTask = function () {

            console.log("Into submit new task function. isExist: " + isExist);

            if (isExist) {
                // Update task
                // Update date on UI
                $scope.tasks[$scope.id].Title = $scope.title;
                $scope.tasks[$scope.id].Description = $scope.description;

                var data = { "id": $scope.tasks[$scope.id].Id, "title": $scope.title, "description": $scope.description, "publishDate": $scope.tasks[$scope.id].PublishDate, "IsCompleted": $scope.tasks[$scope.id].IsCompleted };

                $http.put(
                    '/api/task',
                    JSON.stringify(data),
                    {
                        headers: { 'Content-Type': 'application/json' }
                    }
                ).then(function (response) {
                    $scope.tasks.push(response.data);
                    console.log("The task with id: " + $scope.tasks[$scope.id].Id + " was successfully updated.");
                },
                    function (response) {
                        // error
                        initializeAndOpenErrorPopup(response);
                    });
            }
            else {
                // Create task
                var data = { "title": $scope.title, "description": $scope.description };

                $http.post(
                    '/api/task',
                    JSON.stringify(data),
                    {
                        headers: { 'Content-Type': 'application/json' }
                    }
                ).then(function (response) {
                    // stub
                    // change returning types of service

                    var newtask = $scope.tasks[1];
                    newtask.Title = $scope.title;
                    newtask.Description = $scope.description;

                    $scope.tasks.push(newtask);

                    //$scope.tasks.push(response.data);
                },
                    function (response) {
                        // error
                        initializeAndOpenErrorPopup(response);
                    });
            }

            $scope.isVisiblePopup = false;

        };

        $scope.getQueryResults = function () {

            $http.get(
                '/api/task/search',
                {
                    params: { query: $scope.query }
                }
            ).then(function (response) {
                $scope.tasks = response.data;
            },
                function (response) {
                    // error
                    initializeAndOpenErrorPopup(response);
                });
        };

        $scope.deleteTask = function (index) {

            if (confirm("Do you really want to delete this task?")) {
                $http.delete(
                    '/api/task',
                    {
                        params: { id: $scope.tasks[index].Id }
                    }
                ).then(function (response) {
                    $scope.tasks.splice(index, 1);
                },
                function (response) {
                    // error
                    initializeAndOpenErrorPopup(response);
                });
            }

        };

        $scope.editTask = function (index) {
            $scope.title = $scope.tasks[index].Title;
            $scope.description = $scope.tasks[index].Description;
            $scope.id = index;
            isExist = true;

            // fix it
            $scope.isVisiblePopup = true;
        };

        $scope.createTask = function () {
            // Clear updating information date
            $scope.title = '';
            $scope.description = '';
            $scope.id = 0;
            isExist = false;

            // fix it
            $scope.isVisiblePopup = true;
        };

        function initializeAndOpenErrorPopup(response) {

            console.log("Status code: " + response.status);
            console.log("Status text: " + response.statusText);
            console.log("Message: " + response.data);

            $scope.isVisibleErrorPopup = true;
            $scope.errorStatusCode = response.status;
            $scope.errorHeader = response.statusText;
            $scope.errorMessage = response.data;
        };

    }]);

})();
