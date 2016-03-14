(function () {
    'use strict';
    // var PositionService = angular.module('FileUploadService', ['$http']);

    angular
       .module('app').service('fileUpload', [
        '$http', '$q', function ($http, $q) {

       
            this.uploadFileToUrl = function (file, uploadUrl) {
                var defferd = $q.defer();
           
                $http({
                    url:uploadUrl,
                    method: 'Post',
                    transformRequest: function(data) {
                        var formData = new FormData();
                        console.log(data);
                        //need to convert our json object to a string version of json otherwise
                        // the browser will do a 'toString()' on the object which will result 
                        // in the value '[Object object]' on the server.
                      
                        formData.append("file", file);
                        return formData;
                    },
                    headers: { 'Content-Type': undefined }
                })
                    .success(function (data) {
                        console.log(data);
                        defferd.resolve(data);
                    })
                    .error(function (error) {
                        console.log(error);
                        defferd.reject;
                    });
                return defferd.promise;
            }


            this.uploadDataFileToUrl = function (files, Id, model, uploadUrl) {
                var defferd = $q.defer();
                //var fd = new FormData();
                //fd.append('files', files);
                $http({
                    url: uploadUrl,
                    method: 'Post',
                    transformRequest: function (data) {
                        var formData = new FormData();
                        console.log(data);
                        //need to convert our json object to a string version of json otherwise
                        // the browser will do a 'toString()' on the object which will result 
                        // in the value '[Object object]' on the server.
                        formData.append("model", JSON.stringify(data.model));
                        formData.append("files", files);
                        //now add all of the assigned files
                        //for (var i = 0; i < data.files; i++) {
                        //    //add each file to the form data and iteratively name them
                        //    formData.append("files" + i, data.files[i]);
                        //    console.log(files[i]);
                        //}
                        return formData;
                    },
                    //Create an object that contains the model and files which will be transformed
                    // in the above transformRequest method
                    data: { files: files, model: model },
                    params: { Id: Id },
                    headers: { 'Content-Type': undefined }

                })
                    .success(function (data) {
                        console.log(data);
                        defferd.resolve(data);
                    })
                    .error(function () {
                        defferd.reject();
                    });
                return defferd.promise();
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