(function() {
    'use strict';

    angular
        .module('app')
        .directive('employeeDirective', EmployeeDirective)
    .directive('dailyDirective',DailyDirective);

    EmployeeDirective.$inject = [];

    function EmployeeDirective() {
        return {
            
            restrict: 'E',
            templateUrl: '../../templates/employees/directives/employeeData.html'
        }
    }
    function DailyDirective() {
        return {

            restrict: 'E',
            templateUrl: '../../templates/daily/directives/dailyData.html'
        }
    }
})();