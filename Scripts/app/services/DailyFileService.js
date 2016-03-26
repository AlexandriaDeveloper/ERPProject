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
                getFileDetails: {
                    url: '/api/DailyFiles/GetDetailsFile/:Id',
                    method: 'GET',
                    params: { Id: '@Id' },
                    isArray: false

                },
                getEmployeeInfo: {
                    url: '/api/DailyFiles/GetEmployeeInfo/:Id',
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


                }, updateEmpInfo2: {
                    'url': '/api/DailyFiles/UpdateEmpInfo/',
                    'method': 'GET',
                    'params': { Code: '@Code', NationalId: '@NationalId' },
                    isArray: false
                }, updateEmpInfo: {
                    
                    url: '/api/DailyFiles/UpdateEmpInfo',
                    method: 'POST',
                    params: { DailyFileId: '@DailyFileId', EmployeeId: '@EmployeeId', Net: '@Net' },
                    isArray:false
                  


                }, getemployee: {
                    'url': '/api/Report/GetEmployees/',
                    'method': 'GET',
                    'params': { Code: '@Code', NationalId: '@NationalId' },
                    isArray: false
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


                },
                DeleteEmpInfo: {
                    'url': '/api/DailyFiles/DeleteEmpInfo/:Id',
                    'method': 'Delete',
                    'params': { Id: '@Id' }


        }

            });
        }]);

})();