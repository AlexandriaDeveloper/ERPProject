(function () {
    'use strict';

    angular
        .module('app')
        .controller('HomeController', HomeController);

    HomeController.$inject = [
        '$scope', 'Home'];

    function HomeController($scope, Home) {
        $scope.title = Home.get({});
        console.log(Home.get({}));
  
    }
})();
