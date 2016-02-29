(function () {
    'use strict';

    angular
        .module('app')
        .controller('DailyController', DailyController)
        .controller('AddDailyController', AddDailyController)
        .controller('EditDailyController', EditDailyController)
        .controller('DeleteDailyController', DeleteDailyController);

    DailyController.$inject = [
        '$scope', 'Daily'];
    AddDailyController.$inject = [
        '$scope', 'Daily', 'ExpensessType','$location'
    ];
    EditDailyController.$inject = [
       '$scope', 'Daily', 'ExpensessType', '$location', '$routeParams'
    ];
    DeleteDailyController.$inject = [
   '$scope', 'Daily', 'ExpensessType', '$location', '$routeParams'
    ];
    function DailyController($scope, Daily) {

        $scope.format = 'yyyy/MM/dd';
        $scope.datefrom = new Date();
        $scope.dateto = new Date();

        $scope.dailies = Daily.query({});
        console.log($scope.dailies);
    }

    function AddDailyController($scope, Daily, ExpensessType, $location) {

        $scope.title = "يومية جديدة ";
        $scope.edit = false;
        //  $scope.selectedDaily.DailyDay = new Date();

        $scope.ExpensessType = ExpensessType.query();
        $scope.selectedDaily = new Daily();

        $scope.GetValue = function (expensessId) {
            $scope.selectedDaily.ExpensessTypeId = expensessId;
            console.log(expensessId);
        }


       $scope.addDaily= function(selectedDaily) {
           
           console.log($scope.selectedDaily.DailyDay);
           console.log(selectedDaily);
           $scope.selectedDaily.$save(selectedDaily, function (result) {
               $location.url('daily/index');

           });


       }
     

    }

    function EditDailyController($scope, Daily, ExpensessType, $location, $routeParams) {
        $scope.title = "تعديل اليومية ";
       //   $scope.selectedDaily.DailyDay = new Date();
        $scope.edit = true;
        $scope.ExpensessType = ExpensessType.query();
        $scope.selectedDaily = Daily.get({ Id: $routeParams.Id }, function(data) {

            console.log(data);
            $scope.selectedDaily.DailyDay = new Date(data.DailyDay);


        });

        $scope.closeDaily = function (selectedDaily) {

            console.log($scope.selectedDaily.DailyDay);
            console.log(selectedDaily);
            $scope.selectedDaily.Open = false;
            selectedDaily.Open = false;
            $scope.selectedDaily.$save(selectedDaily, function (result) {
                $location.url('daily/index');

            });

        }



        $scope.addDaily = function (selectedDaily) {

            console.log($scope.selectedDaily.DailyDay);
            console.log(selectedDaily);
            $scope.selectedDaily.$save(selectedDaily, function (result) {
                $location.url('daily/index');

            });


        }
        //console.log($scope.selectedDaily.DailyDay);
        //$scope.selectedDaily.DailyDay = new Date($scope.selectedDaily.DailyDay);
    }


    function DeleteDailyController($scope, Daily, ExpensessType, $location, $routeParams) {
        $scope.selectedDaily = Daily.get({ Id: $routeParams.Id });
        $scope.deleteDaily = function () {
            console.log($routeParams.Id);
            $scope.selectedDaily.$remove({ Id: $routeParams.Id }, function (result) {
            
                $location.url('daily/index');

            })};
        }


 

})();
