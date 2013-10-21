function UserLoginMainCtrl($scope, $http) {
    $scope.UserLogin = function () {
        console.log(angular.toJson($scope.EditProduct));
        if ($scope.UserLoginForm.$valid) {
            $scope.showerror = false;
            $http.post($sitecore.urls["userLogin"], { account: User.email, pwd: User.pwd }).success(function (data) {
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