(function () {
    'use strict';

    var DailyService = angular.module('DailyService', ['ngResource']);

    DailyService.factory('Daily', ['$resource',
        function ($resource) {
            return $resource('/api/Daily/:Id', {}, {
                query: {
                    'method': 'GET',
                    'params': {},
                    isArray: true
                },
                get: {
                    'url': '/api/Daily/Get/:Id',
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