(function () {
    'use strict';

    angular
        .module('app')
        .controller('DailyController', DailyController)
        .controller('AddDailyController', AddDailyController)
        .controller('EditDailyController', EditDailyController)
        .controller('DeleteDailyController', DeleteDailyController)
         .controller('InfoDailyController', InfoDailyController);

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
    InfoDailyController.$inject = [
   '$scope', 'DailyFile', 'ExpensessType', '$location', '$routeParams'
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
           //$scope.selectedDaily.$save(selectedDaily, function (result) {
           //    $location.url('daily/index');

           //});
           Daily.save(selectedDaily, function(result) {
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
            $scope.selectedDaily = data.result;
            $scope.selectedDaily.DailyDay = new Date(data.result.DailyDay);
         

        });

        $scope.closeDaily = function (selectedDaily) {

           
            $scope.selectedDaily.Open = false;
            selectedDaily.Open = false;
            var currentDate = new Date();
            var totalAmount = 0;
      


            selectedDaily.ClosedDate = currentDate.toLocaleDateString();
            
            Daily.save(selectedDaily, function () {
                $location.url('daily/index');
            });

       

        }



        $scope.addDaily = function (selectedDaily) {

            console.log($scope.selectedDaily.DailyDay);
            console.log(selectedDaily);
            //$scope.selectedDaily.$save(selectedDaily, function (result) {
            //    $location.url('daily/index');

            //});
            Daily.save(selectedDaily, function() {
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
            //$scope.selectedDaily.$remove({ Id: $routeParams.Id }, function (result) {
            
            //    $location.url('daily/index');

            //})

            Daily.remove({ Id: $routeParams.Id }, function () {
                $location.url('daily/index');
            });


        };

        }

    function InfoDailyController($scope, DailyFile, ExpensessType, $location, $routeParams) {

        $scope.routeId = $routeParams.Id;
        $scope.total = 0;
        $scope.number = 0;
        $scope.selectedDaily = DailyFile.query({ Id: $routeParams.Id });
        $scope.state = DailyFile.getParent({ Id: $routeParams.Id });
        console.log($scope.state);
        $scope.selectedDaily.$promise.then(function (data) {

            angular.forEach(data, function(value) {
                $scope.total += value.FileTotalAmount;
                $scope.number += value.EmployeesNumber;
            });

        });
      
     

       //$.forEach($scope.selectedDaily, function (data) {
       //     console.log(data);
       // });


        //for (var i = 0; i < $scope.selectedDaily.length  ; i++) {
        //    $scope.total += $scope.selectedDaily[i].FileTotalAmount;
        //    $scope.number += $scope.selectedDaily[i].EmployeesNumber;
        //    console.log($scope.total);
        //    console.log($scope.number);


        //    console.log($scope.selectedDaily[i].FileTotalAmount);
        //    console.log($scope.selectedDaily[i].EmployeesNumber);
        //}
        //console.log($scope.total);
        //console.log($scope.number);
    }


})();
