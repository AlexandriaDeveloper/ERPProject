(function () {
    'use strict';

    angular
        .module('app')

    .controller('AddDailyFileController', AddDailyFileController)
      .controller('DetailsDailyFileController', DetailsDailyFileController)
     .controller('DeleteDailyFileController', DeleteDailyFileController)
      .controller('EmployeeInfoController', EmployeeInfoController)
       .controller('ExpenssesController', ExpenssesController)
    .controller('DeleteExpenssesController', DeleteExpenssesController)
    ;



    AddDailyFileController.$inject = ['$location', '$scope', '$routeParams', 'DailyFile', 'fileUpload'];
    DetailsDailyFileController.$inject = ['$location', '$scope', '$routeParams', 'DailyFile'];
    DeleteDailyFileController.$inject = ['$location', '$scope', '$routeParams', 'DailyFile'];
    EmployeeInfoController.$inject = ['$location', '$scope', '$routeParams', 'DailyFile'];
    ExpenssesController.$inject = ['$location', '$scope', '$routeParams', 'DailyFile'];
    DeleteExpenssesController.$inject = ['$location', '$scope', '$routeParams', 'DailyFile'];
    function AddDailyFileController($location, $scope, $routeParams, DailyFile, fileUpload) {
        /* jshint validthis:true */

        $scope.title = "اضافة ملف جديد";
        $scope.fileInfo = new DailyFile();
        var uploadUrl = '/api/DailyFiles/PostDailyFile';

        


        $scope.addDailyfile = function (fileinf) {
            
            var files = $scope.myFile;
            console.log(files);
            $scope.fileInfo.DailyId = $routeParams.Id;

            fileUpload.uploadDataFileToUrl(files, $routeParams.Id, $scope.fileInfo, uploadUrl).then(function() {
                $location.url('/daily/info/' + fileinf.DailyId);
          
            });
          
            //DailyFile.save(fileinf, function () {
              
            //    $location.url('/daily/info/' + fileinf.DailyId);
            //});

        };


    }

    function DetailsDailyFileController($location, $scope, $routeParams, DailyFile) {
        /* jshint validthis:true */
        $scope.title = "تعديل ملف ";

 
        $scope.fileInfo = DailyFile.get({ Id: $routeParams.Id });
        console.log($scope.fileInfo);
        $scope.rowCollection = [];
        $scope.fileInfo.$promise.then(function(data) {
            console.log(data.DailyFileDetailses);
       //   $scope.rowCollection = data.DailyFileDetailses;

            angular.forEach(data, function (v, k) {
                $scope.rowCollection = data.DailyFileDetailses;

            });
        });
        $scope.namePredicate = 'Employee.Name';
        $scope.nationalIdPredicate = "Employee.NationalId";
        $scope.codePredicate = "Employee.Code";
        $scope.positionPredicate = 'Net';

    }

    function DeleteDailyFileController($location, $scope, $routeParams, DailyFile) {
        $scope.edit = true;
        $scope.selectedDailyFile = DailyFile.get({Id: $routeParams.Id });
        $scope.deleteItem = function () {

            console.log($routeParams.Id);
             DailyFile.remove({ Id: $routeParams.Id }, function() {

                 $location.url('/daily/info/' + $scope.selectedDailyFile.DailyId);

             });
        }


       
    }

    function EmployeeInfoController($location, $scope, $routeParams, DailyFile) {
        console.log($routeParams.Id);
        $scope.empInfo = DailyFile.getEmployeeInfo({ Id: $routeParams.Id });
        console.log($scope.empInfo);
        $scope.updateEmpInfo = function (EmpInfo) {
            console.log(EmpInfo);
            DailyFile.updateEmpInfo({ EmpInfo: EmpInfo });

        }
    }


    function ExpenssesController($location, $scope, $routeParams, DailyFile) {
  
    //    $scope.empInfo = DailyFile.getEmployeeInfo({ Id: $routeParams.Id });
        $scope.empinfo = {};
        $scope.getEmpData = function (emp) {
         
            $scope.empinfo = DailyFile.getemployee({ Code: emp.Code, NationalId: emp.NationalId });
           
        }




        $scope.updateEmpInfo = function (EmpInfo2) {

           
            console.log(EmpInfo2);


            DailyFile.updateEmpInfo({
                DailyFileId: $routeParams.Id,
                EmployeeId: EmpInfo2.Id,
                Net: EmpInfo2.Net
            }).$promise.then(function() {
                
                $location.url('/daily/dailyfile/edit/' + $routeParams.Id);
            });
            
        }
    }

    function DeleteExpenssesController($location, $scope, $routeParams, DailyFile) {

        $scope.selectedDetails = DailyFile.getFileDetails({ Id: $routeParams.Id });
        console.log($scope.selectedDetails);

        $scope.deleteDetails= function(data) {
            console.log(data);
            DailyFile.DeleteEmpInfo({ Id: $routeParams.Id }).$promise.then(function() {
                $location.url('/daily/dailyfile/edit/' + data.dailyFileId);

            });
            
        }
    }
})();
