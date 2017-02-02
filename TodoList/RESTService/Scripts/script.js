(function () {
    var todoListModule = angular.module('TodoList', ['Repository']);

    todoListModule.controller('TodoListController', ['$scope', '$http', 'Tasks', function ($scope, $http, Tasks) {

        $scope.tasks = [];
        $scope.isVisiblePopup = false;
        
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

            $scope.isVisiblePopup = false;
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
                function () {
                    console.log("Something went wrong. New task wasn't added.");
                });
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

    }]);

})();
