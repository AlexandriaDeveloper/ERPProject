(function () {
    'use strict';

    angular
        .module('app')
        .controller('UploadController', UploadController);

    UploadController.$inject = ['$scope', 'fileUpload', '$timeout', '$location'];

    function UploadController($scope, fileUpload, $timeout, $location) {
        $scope.diablebtn = false;
        $scope.progressbar = false;
        $scope.progresswidth = 0;
     
        $scope.uploadData = function (upload) {

            $scope.diablebtn = true;
            console.log($scope.myFile);
      
            var file = $scope.myFile;
         //   var file2 = $scope.myFile2;
           
         



            var uploadUrl = '/api/FileUpload/PostFormData';
           fileUpload.uploadFileToUrl(file, uploadUrl).then(function() {

               $scope.diablebtn = false;
               $location.url('/employee/search');
           });
       
        
        }





    }
})();
