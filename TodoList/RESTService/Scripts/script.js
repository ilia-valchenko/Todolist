(function () {
    var todoListModule = angular.module('TodoList', ['Repository', 'ngSanitize']);

    todoListModule.controller('TodoListController', ['$scope', '$http', '$sce', 'Tasks', function ($scope, $http, $sce, Tasks) {

        $scope.$sce = $sce;
        $scope.tasks = [];
        var isExist = false;
        
        function Init() {
            Tasks.getAll().then(
                function successCallback(response) {
                    $scope.tasks = response.data;
                },
                function errorCallback(response) {
                    initializeAndOpenErrorPopup(response);
                }
            )
        };

        // Initialize application
        Init();

        $scope.submitNewTask = function () {

            if (isExist) {
                var data = { "id": $scope.tasks[$scope.id].Id, "title": $scope.title, "description": $scope.description };

                $http.put(
                    '/api/task',
                    JSON.stringify(data),
                    {
                        headers: { 'Content-Type': 'application/json' }
                    }
                ).then(
                    function successCallback(response) {
                        $scope.tasks[$scope.id].Title = $scope.title;
                        $scope.tasks[$scope.id].Description = $scope.description;
                    },
                    function errorCallback(response) {
                        initializeAndOpenErrorPopup(response);
                    }
               );
            }
            else {
                var data = { "title": $scope.title, "description": $scope.description };

                $http.post(
                    '/api/task',
                    JSON.stringify(data),
                    {
                        headers: { 'Content-Type': 'application/json' }
                    }
                ).then(
                    function successCallback(response) {
                        $scope.tasks.push(response.data);
                    },
                    function errorCallback(response) {
                        initializeAndOpenErrorPopup(response);
                    }
               );
            }

            $scope.isVisiblePopup = false;

        };

        $scope.getQueryResults = function () {
            $http.get(
                '/api/task/search',
                {
                    params: { query: $scope.query }
                }
            ).then(
                function successCallback(response) {
                    $scope.tasks = response.data;
                },
                function errorCallback(response) {
                    initializeAndOpenErrorPopup(response);
                }
            );
        };

        $scope.deleteTask = function (index) {
            if (confirm("Do you really want to delete this task?")) {
                $http.delete(
                    '/api/task',
                    {
                        params: { id: $scope.tasks[index].Id }
                    }
                ).then(
                    function successCallback(response) {
                        $scope.tasks.splice(index, 1);
                    },
                    function errorCallback(response) {
                        initializeAndOpenErrorPopup(response);
                    }
                );
            }

        };

        $scope.editTask = function (index) {
            $scope.title = $scope.tasks[index].Title.replace(/<(\/?em)>/gm, '');
           
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
