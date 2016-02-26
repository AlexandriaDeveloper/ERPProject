(function () {
    'use strict';

  
    var UploadService = angular.module('UploadService', ['ngResource']);
    UploadService.factory('Upload', ['$resource',
        function($resource) {
            return $resource('/api/FileUpload/', {}, {
                query: {
                    'method': 'GET',
                    'params': {},
                    isArray: true
                }
            });
      } ]);
      
})();