(function () {
    'use strict';

    var PositionService = angular.module('PositionService', ['ngResource']);

    PositionService.factory('Position', ['$resource',
        function ($resource) {
            return $resource('/api/Positions/:Id', {}, {
                query: {
                    'method': 'GET',
                    'params': {},
                    isArray: true
                }
            });
        }]);
})();