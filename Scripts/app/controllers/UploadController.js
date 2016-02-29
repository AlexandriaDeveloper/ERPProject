(function () {
    'use strict';

    angular
        .module('app')
        .controller('UploadController', UploadController);

    UploadController.$inject = ['$scope', 'fileUpload', '$timeout', '$location'];

    function UploadController($scope, fileUpload, $timeout, $location) {

        $scope.progressbar = false;
        $scope.progresswidth = 0;
     
        $scope.uploadData = function (upload) {
            console.log($scope.myFile);
      
            var file = $scope.myFile;
            var file2 = $scope.myFile2;
           
         



            var uploadUrl = '/api/FileUpload/PostFormData';
           fileUpload.uploadFileToUrl(file, uploadUrl);
           //fileUpload.uploadFileToUrl(file2, uploadUrl);
            //$scope.progressbar = true;
            //var timeUp = function () {
            //    if ($scope.progresswidth < 95) {

            //        $scope.progresswidth += 1;

            //        $timeout(timeUp, 800);
            //    //    console.log($scope.progresswidth);
            //    }
            //}
            //$timeout(timeUp, 800);

            //var upload = new Upload;
            //upload.name = "mohamed";
            //console.log(upload);
            //upload.$save({}, function () {
            //    $scope.progresswidth = 100;
            //    console.log("Done");

            //    $scope.progressbar = false;
            //    $scope.progresswidth = 0;

            //    $location.url("/employee/search");
            //});


        }





    }
})();
