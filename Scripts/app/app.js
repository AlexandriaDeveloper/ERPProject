(function () {

    'use strict';

    angular.module('app', [
        // Angular modules 
        'ngRoute',
        'ngAnimate',
        'ui.bootstrap',
        // Custom modules 
        'HomeService',
        'EmployeeService',
        'DepartmentService',
        'PositionService',
      'DailyService',
      'ExpensessTypeService',
      'DailyFileService',
      'DailyArchiveService',
        //Custom Directive

        // 3rd Party Modules
        'smart-table',
        'ui.bootstrap'
    ]);



})();
