
(function () {
    'use strict';
    config.$inject = ['$routeProvider', '$locationProvider'];
    angular.module('app', [
        // Angular modules 
        'ngRoute'

        // Custom modules 

        // 3rd Party Modules
        
    ]).config(config);

    function config($routeProvider, $locationProvider) {
        $routeProvider.when('/',
        {
   
            templateUrl: 'templates/home.html',
            controller: 'HomeController'
        })
        .when("templates/home.html",
        {
            templateUrl: '/',
            controller: 'HomeController'
        });
        $locationProvider.html5Mode(true);
    }
})();