angular.module("LearnAngular", ["angular-loading-bar"])

//LearnAngularController (must have the "$scope")!!!
.controller("LearnAngularController",
    ["$scope",
        function ($scope) {
            $scope.msg = "All Good!";
        }
    ]
)

//SecondController
.controller("SecondController",
    ["$scope",
        function ($scope) {
            $scope.msg2 = "All Good also!";
        }
    ]
)
