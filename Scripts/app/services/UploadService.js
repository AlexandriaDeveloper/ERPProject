(function() {
    'use strict';
   // var PositionService = angular.module('FileUploadService', ['$http']);

    angular
       .module('app').service('fileUpload', [
        '$http', function ($http) {

        
            this.uploadFileToUrl = function(file, uploadUrl) {
                var fd = new FormData();
                fd.append('file', file);
                $http.post(uploadUrl, fd, {
             
                        transformRequest: angular.identity,
                        headers: { 'Content-Type': undefined }
                    })
                    .success(function (data) {
                        console.log(data);
                    })
                    .error(function() {
                    });
            }
               
            }
    ]);
})();
//(function () {
//    'use strict';

  
//    var UploadService = angular.module('UploadService', ['ngResource']);
//    UploadService.factory('Upload', ['$resource',
//        function($resource) {
//            return $resource('/api/FileUpload/', {}, {
//                query: {
//                    'method': 'GET',
//                    'params': {},
//                    isArray: true
//                }
//            });
//      } ]);
      
//})();