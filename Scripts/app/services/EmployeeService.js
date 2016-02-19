(function () {
    'use strict';

    var EmployeeService = angular.module('EmployeeService', ['ngResource']);

    EmployeeService.factory('Employee', ['$resource',
        function ($resource) {
            return $resource('/api/Employee/:Id', {}, {
                query: {
                    'method': 'GET',
                    'params': {},
                    isArray:true
                },
                get: {
                    'method': 'GET',
                    'params': { Id: '@Id' },
                    isArray: false
                },
            });
        }]);

})();