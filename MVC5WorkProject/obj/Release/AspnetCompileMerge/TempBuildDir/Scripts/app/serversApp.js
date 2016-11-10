angular.module('serversApp', ['MVC5WorkProject.ServersController'])

//.directive('loadingButton', function () {
//    return {
//        restrict: 'A',
//        scope: {
//            spin: '='
//        },
//        link: function (scope, element, attrs) {
//            $(element).ladda();

//            scope.$watch('spin', function(newValue, oldValue) {
//                if (newValue) {
//                    $(element).ladda('start');
//                } else {
//                    $(element).ladda('stop');
//                }
//            });
//        }
//    };
//});