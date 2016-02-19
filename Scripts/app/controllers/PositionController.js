(function () {
    'use strict';

    angular
        .module('app')
        .controller('PositionController', PositionController);

    PositionController.$inject = ['$location']; 

    function PositionController($location) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'PositionController';

        activate();

        function activate() { }
    }
})();
