(function () {
    'use strict';

    var DepartmentService = angular.module('DepartmentService', ['ngResource']);

    DepartmentService.factory('Departments', ['$resource',
        function ($resource) {
            return $resource('/api/Departments/:Id', {}, {
                query: {
                    'method': 'GET',
                    'params': {},
                    isArray: true
                }
            });
        }]);

})();