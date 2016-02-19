
(function () {
    'use strict';
    config.$inject = ['$routeProvider', '$locationProvider'];
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
    ]).config(config)

        //custom Directives
        .directive('myModal', function () {
        return {
            templateUrl:'../../templates/helper/modal.html'
        }
    });
   

    function config($routeProvider, $locationProvider) {
        $routeProvider.when('/',
        {
   
            templateUrl: 'templates/home.html',
            controller: 'HomeController'
        })
        .when("/employee/search",
        {
            templateUrl: 'templates/employees/search.html',
            controller: 'EmployeeController'
        })
           .when("/employee/add",
        {
            templateUrl: 'templates/employees/add.html',
            controller: 'AddEmployeeController'
        })
            .when("/employee/edit/:Id",
        {
            templateUrl: 'templates/employees/edit.html',
            controller: 'EditEmployeeController'
        })


            .otherwise('/');;
        $locationProvider.html5Mode(true);
    }
})();