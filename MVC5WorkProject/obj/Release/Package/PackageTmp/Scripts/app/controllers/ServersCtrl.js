angular.module('MVC5WorkProject.ServersController', ["angular-loading-bar"])
    .controller('ServersCtrl', ['$scope', '$http', function ($scope, $http) {

            //not important i think - can try to remove it
            $scope.model = {};

            //for show or hide the Add Cancel Form
            $scope.states = {
                showform: false,
                //for spinner (found in Index.html)
                //isAdding: false
            };

            //Creating a simple Server object
            $scope.new = {
                Server: {} //= empty object
            };

            //[HTTPGET] for IndexVM ActionResult in ServersController
            $http.get('/Servers/IndexVM').success(function (data) {
                $scope.model = data;
            });

            //Try to undertand what this does
            $scope.showform = function (showAdd) {
                $scope.states.showform = showAdd;
            };

            //[HTTPPOST] for the Edit Actionresult
            //if that's a success then
            $scope.addServer = function () {
                //spin it
                //$scope.states.isAdding = true;
                $http.post('/Servers/Edit', $scope.new.Server).success(function (data) {
                    //stop spinning
                    //scope.states.isAdding = false;
                    //to make the input appear on the screen
                    $scope.model.Server.push(data);
                    //to hide the form after input
                    $scope.showform(false);
                    //to clear the form values {} = empty object
                    //$scope.new.Server = {}; 
                });

            }

        }]);