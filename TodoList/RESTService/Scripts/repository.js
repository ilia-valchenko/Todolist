var repository = angular.module('Repository', []);

repository.service('Tasks', ['$http', function ($http) {
    return {
        getAll: function () {
            return $http.get('http://localhost:53137/api/task');
        }
    };
}]);
