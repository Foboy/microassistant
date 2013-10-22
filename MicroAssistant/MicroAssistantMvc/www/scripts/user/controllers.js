function UserLoginMainCtrl($scope, $http, $location) {
    $scope.UserLogin = function () {
        console.log(angular.toJson($scope.User));
        if ($scope.UserLoginForm.$valid) {
            $scope.showerror = false;
            $http.post($sitecore.urls["userLogin"], { account: $scope.User.email, pwd: $scope.User.pwd }).success(function (data) {
                if (data.Error) {
                    alert(data.ErrorMessage);
                }
                else {
                    window.location.href = "index.html";
                }
                console.log(data);
            }).
            error(function (data, status, headers, config) {
            });
        }
        else {
            $scope.showerror = true;
        }
    }
}

function UserRegisterMainCtrl($scope, $http) {
    $scope.UserRegister = function () {
        console.log(angular.toJson($scope.User));
        if ($scope.UserRegisterForm.$valid) {
            $scope.showerror = false;
            $http.post($sitecore.urls["userRegister"], { account: $scope.User.email, pwd: $scope.User.pwd, entId: $scope.User.enterprise || 0 }).success(function (data) {
                if (data.Error)
                {
                    alert(data.ErrorMessage);
                }
                else {
                    window.location.href = "index.html";
                }
                console.log(data);
            }).
            error(function (data, status, headers, config) {
                alert("error")
            });
        }
        else {
            $scope.showerror = true;
        }
    }
}

function EnterpriseRegisterMainCtrl($scope, $http) {
    $scope.EnterpriseRegister = function () {
        console.log(angular.toJson($scope.Enterprise));
        if ($scope.EnterpriseRegisterForm.$valid) {
            $scope.showerror = false;
            $http.post($sitecore.urls["enterpriseRegister"], { entName: $scope.Enterprise.name, account: $scope.Enterprise.email, pwd: $scope.Enterprise.pwd }).success(function (data) {
                if (data.Error) {
                    alert(data.ErrorMessage);
                }
                else {
                    window.location.href = "index.html";
                }
                console.log(data);
            }).
            error(function (data, status, headers, config) {
                alert("error")
            });
        }
        else {
            $scope.showerror = true;
        }
    }
}
