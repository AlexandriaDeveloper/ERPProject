(function() {
    'use strict';

    angular
        .module('app')
        .directive('employeeDirective', EmployeeDirective);

    EmployeeDirective.$inject = [];

    function EmployeeDirective() {
        return {
            
            restrict: 'E',
            templateUrl: '../../templates/employees/directives/employeeData.html'
        }
    }

})();