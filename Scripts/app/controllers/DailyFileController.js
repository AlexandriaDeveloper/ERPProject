(function () {
    'use strict';

    angular
        .module('app')

    .controller('AddDailyFileController', AddDailyFileController)
      .controller('DetailsDailyFileController', DetailsDailyFileController)
     .controller('DeleteDailyFileController', DeleteDailyFileController)
    ;



    AddDailyFileController.$inject = ['$location', '$scope', '$routeParams', 'DailyFile', 'fileUpload'];
    DetailsDailyFileController.$inject = ['$location', '$scope', '$routeParams', 'DailyFile', 'fileUpload'];
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

    function DetailsDailyFileController($location, $scope, $routeParams, DailyFile, fileUpload) {
        /* jshint validthis:true */
        $scope.title = "تعديل ملف ";
        $scope.fileInfo = DailyFile.get({ Id: $routeParams.Id });
        console.log($scope.fileInfo);

        $scope.myFilter = function (item) {
          //  return item === $scope.fileInfo.;
        };



        //$scope.addDailyfile = function (fileinf) {
        //    var file = $scope.myFile;
        
        //   // var uploadUrl = '/api/DailyFiles/PostFormData';
        //   //fileUpload.uploadFileToUrl(file, uploadUrl);
        //    console.log(file);
        //    console.log($scope.fileInfo);
        //  //  $scope.fileInfo.DailyId = $routeParams.Id;
        //    //$scope.fileInfo.$save(fileinf, function () {
        //    //    $location.url('/daily/info/' + $routeParams.Id);
        //    //});

        //    DailyFile.update(fileinf, function() {
                
        //        $location.url('/daily/info/' + fileinf.DailyId);
        //    });


        //};
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
