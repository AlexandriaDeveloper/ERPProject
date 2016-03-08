(function () {
    'use strict';

    var DailyFileService = angular.module('DailyFileService', ['ngResource']);

    DailyFileService.factory('DailyFile', ['$resource',
        function ($resource) {
            return $resource('/api/DailyFiles/:Id', {}, {
                query: {
                    url: '/api/DailyFiles/GetDailyFiles/:Id',
                    method: 'GET',
                    params: { Id: '@Id' },
                    isArray: true
                },
                get: {
                    url: '/api/DailyFiles/GetDailyFile/:Id',
                    method: 'GET',
                    params: { Id: '@Id' },
                    isArray: false

                },
                getParent: {
                    url: '/api/Daily/GetParent/:Id',
                    method: 'GET',
                    params: { Id: '@Id' },
                    isArray: false

                },
                update: {
                    url: '/api/DailyFiles/PutDailyFile/:dailyFile',
                    method: 'POST',
                    params: { dailyFile: '@dailyFile' }


                },
                save: {
                    url: '/api/DailyFiles/PostDailyFile/:dailyFile',
                    method: 'POST',
                    params: { dailyFile: '@dailyFile' }


                },
                remove: {
                    'url': '/api/DailyFiles/DeleteDailyFile/:Id',
                    'method': 'Delete',
                    'params': { Id: '@Id' }


                }


            });
        }]);

})();