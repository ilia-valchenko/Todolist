(function () {
    var todoListModule = angular.module('TodoList', ['Repository', 'ngSanitize']);

    todoListModule.controller('TodoListController', ['$scope', '$http', '$sce', 'Tasks', function ($scope, $http, $sce, Tasks) {

        $scope.$sce = $sce;
        $scope.tasks = [];
        var isExist = false;
        
        function Init() {
            Tasks.getAll().then(function (response) {
                var objectsFromJson = response.data;

                for (var i = 0; i < objectsFromJson.length; i++)
                    $scope.tasks.push(objectsFromJson[i]);
            }), function (response) {
                // Fill abd display error popup
                initializeAndOpenErrorPopup(response);
            };
        };

        // Initialize application
        Init();

        $scope.submitNewTask = function () {

            if (isExist) {
                var data = { "id": $scope.tasks[$scope.id].Id, "title": $scope.title, "description": $scope.description, "publishDate": $scope.tasks[$scope.id].PublishDate, "IsCompleted": $scope.tasks[$scope.id].IsCompleted };

                $http.put(
                    '/api/task',
                    JSON.stringify(data),
                    {
                        headers: { 'Content-Type': 'application/json' }
                    }
                ).then(function (response) {
                    $scope.tasks[$scope.id].Title = $scope.title;
                    $scope.tasks[$scope.id].Description = $scope.description;
                },
                    function (response) {
                        // Fill abd display error popup
                        initializeAndOpenErrorPopup(response);
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
                    $scope.tasks.push(response.data);
                },
                    function (response) {
                        // Fill abd display error popup
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
                    // Fill abd display error popup
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
                    // Fill abd display error popup
                    initializeAndOpenErrorPopup(response);
                });
            }

        };

        $scope.editTask = function (index) {
            $scope.title = $scope.tasks[index].Title;
            $scope.description = $scope.tasks[index].Description;
            $scope.id = index;
            isExist = true;

            // simplify it
            $scope.isVisiblePopup = true;
        };

        $scope.createTask = function () {
            // Clear updating information date
            $scope.title = '';
            $scope.description = '';
            $scope.id = 0;
            isExist = false;

            // simplify it
            $scope.isVisiblePopup = true;
        };

        function initializeAndOpenErrorPopup(response) {
            $scope.isVisibleErrorPopup = true;
            $scope.errorStatusCode = response.status;
            $scope.errorHeader = response.statusText;
            $scope.errorMessage = response.data;
        };

    }]);

})();
