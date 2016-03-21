(function () {
    'use strict';

    angular
        .module('app')
        .controller('DailyArchiveController', DailyArchiveController)
      .controller('TestArchiveController', TestArchiveController)
        .controller('CheckinfoArchiveController', CheckinfoArchiveController);

    DailyArchiveController.$inject = ['$scope', 'DailyArchive'];
    TestArchiveController.$inject = ['$scope', 'DailyArchive', '$routeParams','$window'];
    CheckinfoArchiveController.$inject = ['$scope', 'DailyArchive', 'Daily', 'ExpensessType','$location', '$routeParams'];


    function DailyArchiveController($scope, DailyArchive) {
        $scope.title = 'DailyArchiveController';

        //filter scope
        $scope.namePredicate = 'Name';
        $scope.expensessPredicate = "ExpensessType.Name";
        $scope.checkPredicate = "CheckGP";
        $scope.positionPredicate = 'Position.Name';
        $scope.departmentPredicate = 'Department.Name';



        $scope.rowCollection = {};
        $scope.dailiesArchive = DailyArchive.query({}).$promise.then(function(data) {
            console.log(data);
            $scope.rowCollection = data;
        });

        console.log($scope.rowCollection);


    }


    function TestArchiveController($scope, DailyArchive, $routeParams,$window) {


        //filter scope
        $scope.btnDisable = false;
        $scope.namePredicate = 'Name';
        $scope.descPredicate = 'daily.Description.Name';
        $scope.codePredicate = 'Employee.Code';
        $scope.netPredicate = 'Net';
        $scope.message = false;
        $scope.errorMsg = "";
        // $scope.title = 'DailyArchiveController';
        $scope.rowCollection = [];

        $scope.dailies = DailyArchive.test({ Id: $routeParams.Id });


        $scope.rowCollection = $scope.dailies;
        console.log($scope.dailies);



        $scope.ExportData = function (data) {
            $scope.btnDisable = true;
            $scope.message = true;

            DailyArchive.ExportExcel({ Id: $routeParams.Id }).$promise.then(function(data2) {

                console.log(data2.result);
                console.log(data2.errorMsg);
                $scope.errorMsg = data2.errorMsg;
                console.log("In Progress.....");
             var s = "http://localhost:1521/api/DailyArchive/ExportExcel2?filePath=" + data2.result;
            
                $window.open(s);
                console.log("done" + data2.result);
                $scope.message = false;
                $scope.btnDisable = false;
            });
          
        }

      
  
      


    }


    function CheckinfoArchiveController($scope, DailyArchive, Daily, ExpensessType,$location, $routeParams) {

        $scope.title = "بيانات اليومية";


        Daily.getclosed({ Id: $routeParams.Id }).$promise.then(function (data) {


            $scope.ExpensessType = ExpensessType.query({});
    
          
            $scope.selectedDaily = data.result;
            $scope.selectedDaily.DailyDay = new Date(data.result.DailyDay);


        });
      
        $scope.addDailyInfo= function(daily) {
            Daily.save(daily);
            $location.url("/daily/daily/archive/");
        }


    }
})();
