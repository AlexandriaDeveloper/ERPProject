(function () {
    'use strict';

    var ExpensessTypeService = angular.module('ExpensessTypeService', ['ngResource']);

    ExpensessTypeService.factory('ExpensessType', ['$resource',
        function ($resource) {
            return $resource('/api/ExpensessType/:Id', {}, {
                query: {
                    'method': 'GET',
                    'params': {},
                    isArray: true
                }
            });
        }]);
})();