(function () {

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
            })



            .otherwise('/');
        $locationProvider.html5Mode(true);
    };

})();