(function() {
    'use strict';

    angular
        .module('app')
        .controller('EmployeeController', EmployeeController)
        .controller('AddEmployeeController', AddEmployeeController)
        .controller('EditEmployeeController', EditEmployeeController)
        .controller('DeleteEmployeeController', DeleteEmployeeController)
        .controller('UploadEmployeeController', UploadEmployeeController);
  


    /*Services injected*/

    EmployeeController.$inject = [ '$scope', 'Employee', '$log']; 
    AddEmployeeController.$inject = ['$location', '$scope', 'Employee', 'Departments', 'Position', '$timeout'];
    EditEmployeeController.$inject = ['$location', '$scope', 'Employee', 'Departments', 'Position', '$timeout', '$routeParams'];
    DeleteEmployeeController.$inject = ['$location', '$scope', 'Employee', 'Departments', 'Position', '$timeout', '$routeParams'];
    UploadEmployeeController.inject = ['$scope', 'fileUpload'];
      


    /*Controllers*/


    function EmployeeController($scope, Employee, $log) {
        /* jshint validthis:true */

        console.log(Employee.query({}));

        function loadData() {

            $scope.rowCollection = [];
            Employee.query({}, function(data) {
                angular.forEach(data, function(v, k) {
                    $scope.rowCollection = data;

                });
            });
        }


        loadData();
        console.log('done');






//filter scope
        $scope.namePredicate = 'Name';
        $scope.nationalIdPredicate = "NationalId";
        $scope.codePredicate = "Code";
        $scope.positionPredicate = 'Position.Name';
        $scope.departmentPredicate = 'Department.Name';
         $scope.sallaryPredicate = 'Sallary';
        $scope.modalshow = false;
       
    }

    //test


    function AddEmployeeController($location, $scope, Employee, Departments, Position, $timeout) {
        /* jshint validthis:true */
        $scope.showMsg = false;
        $scope.doFade = false;

        $scope.title = "أضافة موظف";
        $scope.onlyNumbers = /^\d+$/;
        $scope.genders = [
            { value: 1, label: 'ذكر' },
            { value: 2, label: 'أنثى' }
        ];


        $scope.departments = Departments.query();
        $scope.positions = Position.query();
        $scope.selectedEmp = new Employee();
        $scope.addEmployee = function(employee) {
            //  console.log(employee);
            $scope.selectedEmp.Gender = $scope.selectedGender;
            $scope.selectedEmp.DepartmentId = $scope.selectedDepartment;
            $scope.selectedEmp.PositionId = $scope.selectedPosition;


            $scope.selectedEmp.$save(function() {

                $scope.showMsg = true;
                $timeout(function() {
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
        $scope.onlyNumbers = /^\d+$/;
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

        console.log($scope.selectedEmp);
        $scope.selectedEmp.$promise.then(function(data) {
            $scope.selectedDepartment = data.DepartmentId;
            $scope.selectedPosition = data.PositionId;
            $scope.selectedGender = data.Gender;

        });


        $scope.showMsg = false;
        //$scope.doFade = false;

        $scope.title = "تعديل موظف";


        // console.log($scope.selectedDepartment);


        $scope.addEmployee = function(employee) {
            $scope.selectedEmp.Gender = $scope.selectedGender;
            $scope.selectedEmp.DepartmentId = $scope.selectedDepartment;
            $scope.selectedEmp.PositionId = $scope.selectedPosition;
            $scope.selectedEmp.$save(function() {

                $scope.showMsg = true;
                $timeout(function() {
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

    function DeleteEmployeeController($location, $scope, Employee, Departments, Position, $timeout, $routeParams) {
        $scope.selectedEmp = Employee.get({ Id: $routeParams.Id });
        //   Employee.delete($routeParams);
        //  $location.url("/employee/search");

        $scope.deleteEmployee = function() {

            $scope.selectedEmp.$remove({ entityId: $scope.selectedEmp.Id }, function(result) {

                console.log(result.msg);
                $location.url("/employee/search");

            });

        };

    }

    function UploadEmployeeController($scope, fileUpload) {
        console.log("Upload fle");
        $scope.uploadFile = function() {
            var file = $scope.myFile;

            console.log('file is ');
            console.dir(file);

            var uploadUrl = "api/FileUpload/GetFormData";
            fileUpload.uploadFileToUrl(file, uploadUrl);

            //$scope.upload = [];
            //$scope.UploadedFiles = [];

            //$scope.startUploading = function ($files) {
            //    //$files: an array of files selected
            //    for (var i = 0; i < $files.length; i++) {
            //        var $file = $files[i];
            //        (function (index) {
            //            $scope.upload[index] = FileUploader.upload({
            //                url: "./api/fileupload", // webapi url
            //                method: "POST",
            //                file: $file
            //            }).progress(function (evt) {
            //            }).success(function (data, status, headers, config) {
            //                // file is uploaded successfully
            //                $scope.UploadedFiles.push({ FileName: data.FileName, FilePath: data.LocalFilePath, FileLength: data.FileLength });
            //            }).error(function (data, status, headers, config) {
            //                console.log(data);
            //            });
            //        })(i);
            //    }
            //}

            //$scope.uploadFile = function() {
            //    var file = $scope.myFile;

            //    console.log('file is ');
            //    console.dir(file);

            //    var uploadUrl = "/Uploads";
            //    fileUpload.uploadFileToUrl(file, uploadUrl);
            //};


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



})();
