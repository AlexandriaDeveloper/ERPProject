(function () {
    'use strict';

    angular
        .module('app')
        .controller('DailyArchiveController', DailyArchiveController)
      .controller('TestArchiveController', TestArchiveController);

    DailyArchiveController.$inject = ['$scope', 'DailyArchive'];
    TestArchiveController.$inject = ['$scope', 'DailyArchive', '$routeParams'];
    function DailyArchiveController($scope, DailyArchive) {
        $scope.title = 'DailyArchiveController';

        $scope.dailiesArchive = DailyArchive.query({});
        console.log($scope.dailiesArchive);
    }


    function TestArchiveController($scope, DailyArchive, $routeParams) {
       // $scope.title = 'DailyArchiveController';
        console.log("Hi");
        $scope.Daily = [];
        $scope.dailies = DailyArchive.test({ Id: $routeParams.Id });
        //$scope.dailies.$promise.then(function(data) {
           

        //    angular.forEach(data, function(result) {
        //        console.log(result.Net);
        //        $scope.Daily.push(result);
        //    });
        //});
        console.log($scope.dailies);





        $scope.ExportData= function(data) {
          
         

            DailyArchive.ExportExcel({ Id: $routeParams.Id });
            console.log($scope.dailies);
        }
    }
})();
