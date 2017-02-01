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

            var data = {
                query: {
                    bool: {
                        should: [
                            { 
                                match: { 
                                    Title: {
                                        query: $scope.query,
                                        fuzziness: "AUTO",
                                        operator:  "and"
                                    }
                                }
                            },
                            { 
                                match: { 
                                    Description: {
                                        query: $scope.query,
                                        fuzziness: "AUTO",
                                        operator:  "and"
                                    }
                                }
                            }
                        ]
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
                $scope.tasks = [];
                var res = response.data.hits.hits;

                for (var i = 0; i < res.length; i++)
                    $scope.tasks.push(res[i]._source);
            },
                function () {
                    console.log("Something went wrong. Query finished unsuccessfully.");
                });
        };

    }]);

})();
