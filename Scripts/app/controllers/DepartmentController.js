(function () {
    'use strict';

    angular
        .module('app')
        .controller('DepartmentController', DepartmentController);

    DepartmentController.$inject = ['$scope', 'Departments'];

    function DepartmentController($scope, Departments) {
        $scope.title = 'DepartmentController';

        activate();

        function activate() { }
    }
})();
