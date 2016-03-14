(function () {
    'use strict';

    angular
        .module('app')
        .controller('ReportController', ReportController);

    ReportController.$inject = ['$scope','Report']; 

    function ReportController($scope,Report) {
        $scope.title = 'ReportController';
        $scope.empinfo = {};
        $scope.getEmpData= function(emp) {
            console.log(emp);
            $scope.empinfo = Report.getemployee({ Code: emp.Code, NationalId: emp.NationalId });
            console.log($scope.empinfo);
        }

     
    
    }
})();
