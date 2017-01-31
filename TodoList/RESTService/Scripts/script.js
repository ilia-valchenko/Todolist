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
            console.log("Show popup");
            $scope.isVisiblePopup = true;
            console.log("The variable is: " + $scope.isVisiblePopup);
        };

        $scope.hidePopup = function () {
            console.log("Hide popup");
            $scope.isVisiblePopup = false;
        };

        $scope.submitNewTask = function () {
            console.log("Title: " + $scope.title);
            console.log("Description: " + $scope.description);

            var data = { "title": $scope.title, "description": $scope.description };

            $http.post(
                '/api/task',
                JSON.stringify(data),
                {
                    headers: { 'Content-Type': 'application/json' }
                }
            ).then(function (response) {
                console.log("A new task was successfully sent.");
                console.log(response.data);
                $scope.tasks.push(response.data);
            },
                function () {
                    console.log("Something went wrong. New task wasn't added.");
                });
        };

        $scope.getQueryResults = function () {

            var data = {
                query: {
                    match: {
                        Title: $scope.query
                    }
                }
            };

            $http.post(
                'http://localhost:9200/taskmanager/tasks/_search',
                JSON.stringify(data),
                {
                    // Avoid CORS
                    headers: {
                        'Access-Control-Allow-Origin': '*',
                        'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS',
                        'Access-Control-Allow-Headers': 'Origin, Content-Type, X-Auth-Token',
                        'Content-Type': 'application/json'
                    }
                }
            ).then(function (response) {
                console.log("Tasks which were given by query:");

                $scope.tasks = [];
                var res = response.data.hits.hits;

                for (var i = 0; i < res.length; i++)
                    $scope.tasks.push(res[i]._source);
            },
                function () {
                    console.log("Something went wrong. New task wasn't added.");
                });
        };

    }]);

})();
