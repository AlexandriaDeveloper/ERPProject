(function() {

    'use strict';
    myConfig.$inject = ['$routeProvider', '$locationProvider'];
    angular.module('app', [
            // Angular modules 
            'ngRoute',
            'ngAnimate',
            // Custom modules 
            'HomeService',
            'EmployeeService',
            'DepartmentService',
            'PositionService',
            //Custom Directive

            // 3rd Party Modules
            'smart-table',
            'ui.bootstrap'
    ]).config(myConfig).service('fileUpload', ['$http', function ($http) {
        this.uploadFileToUrl = function (file, uploadUrl) {
            var fd = new FormData();
            fd.append('file', file);

            $http.post(uploadUrl, fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            })

            .success(function () {
            })

            .error(function () {
            });
        }
    }]);
    
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
                    controller: 'UploadEmployeeController'
                })
                .otherwise('/');
            $locationProvider.html5Mode(true);
        };
    

})();
