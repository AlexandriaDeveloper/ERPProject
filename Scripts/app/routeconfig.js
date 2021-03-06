﻿(function () {

    myConfig.$inject = ['$routeProvider', '$locationProvider'];

    'use strict';
    angular.module('app')
        .config(myConfig);


    function myConfig($routeProvider, $locationProvider) {

        $routeProvider.when('/',
            {
                templateUrl: 'templates/home.html',
                controller: 'HomeController'
            })
            .when('/index.html',
            {
                templateUrl: 'templates/home.html',
                controller: 'HomeController'
            })
            .when('/employee/search',
            {
                templateUrl: 'templates/employees/search.html',
                controller: 'EmployeeController'
            })
            .when('/employee/add',
            {
                templateUrl: 'templates/employees/add.html',
                controller: 'AddEmployeeController'
            })
            .when('/employee/edit/:Id',
            {
                templateUrl: 'templates/employees/edit.html',
                controller: 'EditEmployeeController'
            })
            .when('/employee/delete/:Id',
            {
                templateUrl: 'templates/employees/delete.html',
                controller: 'DeleteEmployeeController'
            })
            .when('/employee/upload',
            {
                templateUrl: 'templates/employees/employeeUpload.html',
                controller: 'UploadController'
            })


            /* Daily Route*/
            .when('/daily/index',
            {
                templateUrl: 'templates/daily/index.html',
                controller: 'DailyController'
            }).when('/daily/add',
            {
                templateUrl: 'templates/daily/add.html',
                controller: 'AddDailyController'
            }).when('/daily/edit/:Id',
            {
                templateUrl: 'templates/daily/edit.html',
                controller: 'EditDailyController'
            }).when('/daily/delete/:Id',
            {
                templateUrl: 'templates/daily/delete.html',
                controller: 'DeleteDailyController'
            }).when('/daily/info/:Id',
            {
                templateUrl: 'templates/daily/info.html',
                controller: 'InfoDailyController'
            }).when('/daily/dailyfile/add/:Id',
            {
                templateUrl: 'templates/dailyfile/add.html',
                controller: 'AddDailyFileController'
            }).when('/daily/dailyfile/adddirect/:Id',
            {
                templateUrl: 'templates/dailyfile/adddirect.html',
                controller: 'AddDailyFileController'
            }).when('/daily/dailyfile/addexpensse/:Id',
            {
                templateUrl: 'templates/dailyfile/employee/addexpensse.html',
                controller: 'ExpenssesController'
            })
            .when('/daily/dailyfile/deleteexpensse/:Id',
            {
                templateUrl: 'templates/dailyfile/employee/deleteexpensse.html',
                controller: 'DeleteExpenssesController'
            })

            .when('/daily/dailyfile/edit/:Id',
            {
                templateUrl: 'templates/dailyfile/details.html',
                controller: 'DetailsDailyFileController'
            }).when('/daily/dailyfile/delete/:Id',
            {
                templateUrl: 'templates/dailyfile/delete.html',
                controller: 'DeleteDailyFileController'
            }).when('/daily/dailyfile/empinfo/:Id',
            {
                templateUrl: 'templates/dailyfile/employee/edit.html',
                controller: 'EmployeeInfoController'
            })



            .when('/daily/daily/archive/',
            {
                templateUrl: 'templates/dailyarchive/index.html',
                controller: 'DailyArchiveController'
            }).when('/daily/archive/test/:Id',
            {
                templateUrl: 'templates/dailyarchive/test.html',
                controller: 'TestArchiveController'
            }).when('/daily/archive/checkinfo/:Id',
            {
                templateUrl: 'templates/dailyarchive/checkinfo.html',
                controller: 'CheckinfoArchiveController'
            }).when('/report/employee/details',
            {
                templateUrl: 'templates/report/employees.html',
                controller: 'ReportController'
            }).otherwise('/');



           
        $locationProvider.html5Mode(true);
    };

})();