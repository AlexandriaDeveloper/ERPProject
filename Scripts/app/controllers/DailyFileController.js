(function () {
    'use strict';

    angular
        .module('app')

    .controller('AddDailyFileController', AddDailyFileController)
      .controller('DetailsDailyFileController', DetailsDailyFileController)
     .controller('DeleteDailyFileController', DeleteDailyFileController)
    ;



    AddDailyFileController.$inject = ['$location', '$scope', '$routeParams', 'DailyFile', 'fileUpload'];
    DetailsDailyFileController.$inject = ['$location', '$scope', '$routeParams', 'DailyFile'];
    DeleteDailyFileController.$inject = ['$location', '$scope', '$routeParams', 'DailyFile'];
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

        $scope.selectedDailyFile = DailyFile.get({Id: $routeParams.Id });
        $scope.deleteItem = function () {

            console.log($routeParams.Id);
             DailyFile.remove({ Id: $routeParams.Id }, function() {

                 $location.url('/daily/info/' + $scope.selectedDailyFile.DailyId);

             });
        }


       
    }

})();
