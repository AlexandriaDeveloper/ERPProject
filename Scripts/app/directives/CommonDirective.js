angular
     .module('app')
    .directive('fileModel', ['$parse', function ($parse) {
         return {
             restrict: 'A',
             link: function (scope, element, attrs) {
                 var model = $parse(attrs.fileModel);
                 var modelSetter = model.assign;

                 element.bind('change', function () {
                     scope.$apply(function () {
                         modelSetter(scope, element[0].files[0]);
                     });
                 });
             }
         };
    }]).directive('customCheck', function () {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                predicate: '@',
                value: '=ngModel'
            },
            require: '^stTable',
            template: '<input type="checkbox" ng-change="change()"></input>',
            link: function (scope, element, attr, ctrl) {
                ctrl.search(scope.value, scope.predicate); // sets initial value
                scope.change = function change() {
                    console.log(scope.value); // old value
                    console.log(scope); // expanding this in the console will show value has the new value
                    setTimeout(function() {
                        ctrl.search(scope.value, scope.predicate); //old value
                    }, 100);
                };
            }
        };
    }).directive('fileDownload', function ($compile) {
        var fd = {
            restrict: 'A',
            link: function (scope, iElement, iAttrs) {

                scope.$on("downloadFile", function (e, url) {
                    var iFrame = iElement.find("iframe");
                    if (!(iFrame && iFrame.length > 0)) {
                        iFrame = $("<iframe style='position:fixed;display:none;top:-1px;left:-1px;'/>");
                        iElement.append(iFrame);
                    }

                    iFrame.attr("src", url);


                });
            }
        };

        return fd;
    });
;