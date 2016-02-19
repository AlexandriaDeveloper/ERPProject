(function () {
    'use strict';

    angular
        .module('app')
        .controller('mymodalController', mymodalController);

    mymodalController.$inject = ['$location']; 

    function mymodalController($location) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'mymodalController';
        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
        activate();

        function activate() { }
    }
})();
