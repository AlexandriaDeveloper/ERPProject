(function () {
    'use strict';

    angular
        .module('app')
        .controller('EmployeeController', EmployeeController)
        .controller('AddEmployeeController', AddEmployeeController)
        .controller('EditEmployeeController', EditEmployeeController)
        .controller('ModalInstanceCtrl', ModalInstanceCtrl);

    EmployeeController.$inject = ['$scope',
        'Employee', '$uibModal', '$log', '$rootScope'];
    AddEmployeeController.$inject = ['$location', '$scope', 'Employee', 'Departments', 'Position', '$timeout'];
    EditEmployeeController.$inject = ['$location', '$scope', 'Employee', 'Departments', 'Position', '$timeout', '$routeParams'];
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'selectedEmp'];


    /*Controllers*/


    function EmployeeController($scope, Employee, $uibModal, $log) {
        /* jshint validthis:true */


        function loadData() {

            $scope.rowCollection = [];
            Employee.query({}, function (data) {
                angular.forEach(data, function (v, k) {
                    $scope.rowCollection = data;

                });
            });
        }


        loadData();
        console.log('done');
        //     $scope.getById = function (Id) {
        //         var index = $scope.rowCollection.indexOf(Id);
        //         $scope.selectedEmp = $scope.rowCollection[index];
        //         $scope.modalOpen('lg',$scope.selectedEmp);
        //     }
        //     $scope.modalOpen = function (size,selectedEmp) {
        //         console.log(' open Event');
        //          //if (typeof selectedEmp === 'undefined') {
        //          //    selectedEmp = new Employee;
        //         //}

        //         var modalInstance = $uibModal.open({
        //            // animation: $scope.animationsEnabled,
        //             templateUrl: '../../templates/helper/modal.html',
        //             controller: 'ModalInstanceCtrl',
        //             size: size,
        //             resolve: {
        //                 selectedEmp: function () {

        //                     return $scope.selectedEmp;
        //                 }
        //             }
        //         });
        //         modalInstance.result.then(function (selectedEmp2) {
        //             console.log(selectedEmp2);
        //                 selectedEmp2.$save();          
        //                 selectedEmp2 = {};          
        //             //  $uibModalInstance.close();
        //             //  $scope.selectedEmp = selectedEmp2;
        //         }, function() {
        //           console.log('Modal dismissed at: ' + new Date());
        //         });
        //     };

        //     $scope.toggleAnimation = function () {

        //         $scope.animationsEnabled = !$scope.animationsEnabled;

        //     };
        //loadData();




        //filter scope
        $scope.namePredicate = 'Name';
        $scope.nationalIdPredicate = "NationalId";
        $scope.codePredicate = "Code";
        $scope.modalshow = false;
        //get


        // $scope.selectedEmp = new Employee();
        //function addEmployee(emp) {
        //     console.log(emp.Id);
        //     if (typeof emp.Id === 'undefined') {


        //         $scope.selectedEmp.$save();
        //         $scope.rowCollection.push(emp);
        //     } else {
        //         $scope.selectedEmp.$save();
        //     }
        //     $uibModalInstance.close();

        // };


    }

    //test


    function AddEmployeeController($location, $scope, Employee, Departments, Position, $timeout) {
        /* jshint validthis:true */
        $scope.showMsg = false;
        $scope.doFade = false;

        $scope.title = "أضافة موظف";

        $scope.genders = [
            { value: 1, label: 'ذكر' },
            { value: 2, label: 'أنثى' }];
    

        $scope.departments = Departments.query();
        $scope.positions = Position.query();
        $scope.selectedEmp = new Employee();
        $scope.addEmployee = function (employee) {
            //  console.log(employee);
            $scope.selectedEmp.Gender = $scope.selectedGender;
            $scope.selectedEmp.DepartmentId = $scope.selectedDepartment;
            $scope.selectedEmp.PositionId = $scope.selectedPosition;


            $scope.selectedEmp.$save(function () {

                $scope.showMsg = true;
                $timeout(function () {
                    $scope.showMsg = false;

                }, 3000);

                $location.url("/employee/search");

            }, function(error) {
                console.log(error);
                _displayServerValidationError($scope, error);
                $location.url();
            });
          
        }

    }

    function EditEmployeeController($location, $scope, Employee, Departments, Position, $timeout, $routeParams) {
        /* jshint validthis:true */


     





        $scope.title = "تعديل بيانات موظف";
   
        $scope.departments = Departments.query();

        $scope.positions = Position.query();
        $scope.genders = [
            { value: 1, label: 'ذكر' },
            { value: 2, label: 'أنثى' }
        ];
      //  $scope.selectedDepartment = $scope.selectedEmp.DepartmentId;


        $scope.GetValue = function(selectedGender) {
            $scope.selectedGender = selectedGender;
           
        }

        $scope.selectedEmp = Employee.get({ Id: $routeParams.Id });

     
       $scope.selectedEmp.$promise.then(function (data) {
           $scope.selectedDepartment = data.DepartmentId;
           $scope.selectedPosition = data.PositionId;
           $scope.selectedGender = data.Gender;
      
       });
 

        $scope.showMsg = false;
        //$scope.doFade = false;

       $scope.title = "تعديل موظف";


        // console.log($scope.selectedDepartment);


        $scope.addEmployee = function (employee) {
            $scope.selectedEmp.Gender = $scope.selectedGender;
            $scope.selectedEmp.DepartmentId = $scope.selectedDepartment;
            $scope.selectedEmp.PositionId = $scope.selectedPosition;
            $scope.selectedEmp.$save(function () {

                $scope.showMsg = true;
                $timeout(function () {
                    $scope.showMsg = false;

                }, 3000);

                $location.url("/employee/search");

            }, function (error) {
                console.log(error);
                _displayServerValidationError($scope, error);
                $location.url();
            });


        }


    }

    function _displayServerValidationError($scope, error) {

        $scope.validationError = [];
        var errorObj = error.data.ModelState;
        if (errorObj) {

            for (var key in errorObj) {
              
                    var errorMessage = errorObj[key][0];
                    $scope.validationError.push(errorMessage);
               
            }
        }


    }

    function ModalInstanceCtrl($scope, $uibModalInstance, selectedEmp) {
        console.log(selectedEmp);
        $scope.selectedEmp = selectedEmp;
        //$scope.selected = {
        //    item: $scope.items[0]
        //};

        $scope.ok = function () {

            $uibModalInstance.close($scope.selectedEmp);
        };

        $scope.cancel = function () {

            $uibModalInstance.dismiss('cancel');
        };
    };

})();
