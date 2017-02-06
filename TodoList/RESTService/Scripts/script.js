(function () {
    var todoListModule = angular.module('TodoList', ['Repository']);

    todoListModule.controller('TodoListController', ['$scope', '$http', 'Tasks', function ($scope, $http, Tasks) {

        $scope.tasks = [];
        $scope.isVisiblePopup = false;
        var isExist = false;
        
        function Init() {
            Tasks.getAll().then(function (response) {
                var objectsFromJson = response.data;

                for (var i = 0; i < objectsFromJson.length; i++)
                    $scope.tasks.push(objectsFromJson[i]);
            }), function () {
                alert("Oops! Something went wrong. I can't get list of tasks.");
            };
        };

        Init();

        $scope.showPopup = function () {
            $scope.isVisiblePopup = true;
        };

        $scope.hidePopup = function () {
            $scope.isVisiblePopup = false;
        };

        $scope.submitNewTask = function () {

            console.log("Into submit new task function. isExist: " + isExist);

            if (isExist) {
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
                    function () {
                        console.log("Something went wrong. The new task with id: " + $scope.tasks[$scope.id].Id + " wasn't updated.");
                    });
            }
            else {

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
                    function () {
                        console.log("Something went wrong. A new task wasn't added.");
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
                function () {
                    console.log("Something went wrong. Query finished unsuccessfully.");
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
                function () {
                    console.log("Something went wrong. The was't deleted.");
                });
            }

        };

        $scope.editTask = function (index) {

            console.log("Edit task function.");

            $scope.title = $scope.tasks[index].Title;
            $scope.description = $scope.tasks[index].Description;
            $scope.id = index;
            isExist = true;

            $scope.isVisiblePopup = true;
        };

        $scope.createTask = function () {
            // Clear updating information date
            $scope.title = '';
            $scope.description = '';
            $scope.id = 0;
            isExist = false;

            $scope.isVisiblePopup = true;
        };

    }]);

})();
