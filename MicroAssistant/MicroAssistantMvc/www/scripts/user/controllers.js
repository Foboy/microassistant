function UserLoginMainCtrl($scope, $http) {
    $scope.UserLogin = function () {
        console.log(angular.toJson($scope.User));
        if ($scope.UserLoginForm.$valid) {
            $scope.showerror = false;
            $http.post($sitecore.urls["userLogin"], { account: $scope.User.email, pwd: $scope.User.pwd }).success(function (data) {
                console.log(data);
                $scope.product = data;
            }).
            error(function (data, status, headers, config) {
                $scope.product = {};
            });
        }
        else {
            $scope.showerror = true;
        }
    }
}