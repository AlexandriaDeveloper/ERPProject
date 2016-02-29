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
                }
            });
        }]);

})();