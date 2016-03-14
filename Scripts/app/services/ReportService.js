(function () {
    'use strict';

    var DailyService = angular.module('ReportService', ['ngResource']);

    DailyService.factory('Report', ['$resource',
        function ($resource) {
            return $resource('/api/Daily/:Id', {}, {
                query: {
                    'method': 'GET',
                    'params': {},
                    isArray: true
                },
                getemployee: {
                    'url': '/api/Report/GetEmployees/',
                    'method': 'GET',
                    'params': { Code: '@Code', NationalId: '@NationalId' },
                    isArray: false
                },
                get: {
                    'url': '/api/Daily/Get/:Id',
                    'method': 'GET',
                    'params': { Id: '@Id' },
                    isArray: false
                },
                getclosed: {
                    'url': '/api/Daily/GetClosed/:Id',
                    'method': 'GET',
                    'params': { Id: '@Id' },
                    isArray: false
                },
                save: {
                    'url': '/api/Daily/Post/:model',
                    'method': 'Post',
                    'params': { model: '@model' }

                },
                remove: {
                    'url': '/api/Daily/Delete/:Id',
                    'method': 'Delete',
                    'params': { Id: '@Id' }

                }

            });
        }]);

})();