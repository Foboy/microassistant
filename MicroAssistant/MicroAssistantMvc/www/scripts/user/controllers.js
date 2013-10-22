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

function UserRegisterMainCtrl($scope, $http) {
    console.log("sss")
    $scope.UserRegister = function () {
        console.log(angular.toJson($scope.User));
        if ($scope.UserRegisterForm.$valid) {
            $scope.showerror = false;
            $http.post($sitecore.urls["userRegister"], { account: $scope.User.email, pwd: $scope.User.pwd, entId: $scope.User.enterprise || 0 }).success(function (data) {
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