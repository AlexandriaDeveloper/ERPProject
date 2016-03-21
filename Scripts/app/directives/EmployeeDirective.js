/// <reference path="C:\Users\Mohamed\Documents\Visual Studio 2015\Projects\ERPProject\ERPProject\templates/daily/directives/dailyData.html" />
/// <reference path="C:\Users\Mohamed\Documents\Visual Studio 2015\Projects\ERPProject\ERPProject\templates/daily/directives/dailyData.html" />
(function () {
    'use strict';

    function employeeDirective() {
        return {

            restrict: 'E',
            templateUrl: '../../templates/employees/directives/employeeData.html'
        }
    }
    function dailyDirective() {
        return {

            restrict: 'E',
            templateUrl: '../../templates/daily/directives/dailyData.html'
        }
    }
    function fileDirective() {
        return {

            restrict: 'E',
            templateUrl: '../../templates/dailyfile/directives/dailyFileData.html'



        }
    }

    function fileEmployeeDirective() {
        return {

            restrict: 'E',
            templateUrl: '../../templates/dailyfile/employee/directives/employeeinfoData.html'



        }
    }

    angular
        .module('app')
        .directive('employeeDirective', employeeDirective)
        .directive('dailyDirective', dailyDirective)
        .directive('fileDirective', fileDirective)
        .directive('fileEmployeeDirective', fileEmployeeDirective);
})();