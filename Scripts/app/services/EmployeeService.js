(function () {
    'use strict';

    var EmployeeService = angular.module('EmployeeService', ['ngResource']);

    EmployeeService.factory('Employee', ['$resource',
        function ($resource) {
            return $resource('/api/Employee/:Id/:file', {}, {
                query: {
                    'method': 'GET',
                    'params': {},
                    isArray: true
                },
                get: {
                    'method': 'GET',
                    'params': { Id: '@Id' },
                    isArray: false
                },
                upload: {
                    method: "POST",
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined },
                    'params': { file: 'file',id:11 }
                }
            });
        }]);

})();
//var myApp = angular.module('app', []);
