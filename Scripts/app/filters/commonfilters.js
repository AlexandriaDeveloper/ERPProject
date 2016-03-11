(function() {
    'use strict';

    angular
        .module('app')
        .filter('startFrom', function () {
            console.log("hello filter");
            return function (data, start) {

                if (data) {
                    start = +start;
                    return data.slice(start);
                }
                return [];
            }

        });



})();