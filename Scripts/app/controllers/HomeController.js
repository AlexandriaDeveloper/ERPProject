(function () {
    'use strict';

    angular
        .module('app')
        .controller('HomeController', HomeController);

    HomeController.$inject = [
        '$scope', 'Home', '$rootScope'];

    function HomeController($scope, Home, $rootScope) {
        $scope.title = Home.get({});
        console.log(Home.get({}));
        $rootScope.message = "Hello from root";
    }
})();
