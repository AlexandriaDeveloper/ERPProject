(function () {
    'use strict';

    var DailyArchiveService = angular.module('DailyArchiveService', ['ngResource']);

    DailyArchiveService.factory('DailyArchive', ['$resource',
        function ($resource) {
            return $resource('/api/DailyArchive', {}, {
                query: {
                    url: '/api/DailyArchive/GetDailyArchives/',
                    method: 'GET',
            
                    isArray: true
                },
                get: {
                    url: '/api/DailyArchive/Get/:Id',
                    method: 'GET',
                    params: { Id: '@Id' },
                    isArray: false

                }, test: {
                    url: '/api/DailyArchive/Get/:Id',
                    method: 'GET',
                    params: { Id: '@Id' },
                    isArray: true

                },
                update: {
                    url: '/api/DailyArchive/PutDailyFile/:dailyFile',
                    method: 'POST',
                    params: { dailyFile: '@dailyFile' }


                }, 
                ExportExcel: {
                    url: '/api/DailyArchive/ExportExcel/:Id',
                    method: 'Get',                  
                    params: { Id: '@Id' }
                },
                save: {
                    url: '/api/DailyArchive/PostDailyFile/:dailyFile',
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