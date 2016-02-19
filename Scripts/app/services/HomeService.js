(function () {
    'use strict';

    var HomeService = angular.module('HomeService', ['ngResource']);

    HomeService.factory('Home', [ '$resource',
        function($resource) {
            return $resource('/api/Home/', {}, {
                get: { method: 'GET',isArray:false }
            });
        }]);

})();