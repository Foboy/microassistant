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
            $http.post($sitecore.urls["userRegister"], { username: $scope.User.name, account: $scope.User.email, pwd: $scope.User.pwd, entId: $scope.User.enterprise || 0 }).success(function (data) {
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


function UserMainCtrl($scope, $http, $location) {
    $scope.GetCurrentUserInfo = function () {
        $http.post($sitecore.urls["GetCurrentUserInfo"], {}).success(function (data) {
            if (data.Error) {
                alert(data.ErrorMessage);
            } else {
                $scope.UserInfo = data.Data;
                if ($scope.UserInfo != null && $scope.UserInfo.Sex == 0) {
                    $scope.UserInfo.Sex = 1;
                }
                $scope.UserInfo.Age = $scope.parseAge(data.Data.Birthday);
            }
        }).error(function (data, status, headers, config) {
            alert('error');
        })
    };
    $scope.GetCurrentUserInfo();
    $scope.EditUserInfo = function (data) {
        if ($scope.EditUserForm.$valid) {
            $http.post($sitecore.urls["EditCurrentUserInfo"], { username: data.UserName, nickname: data.UserAccount, sex: data.Sex, age: data.Age }).success(function (data) {
                if (data.Error) {
                    alert(data.ErrorMessage);
                } else {
                    alert("修改成功");
                }
            }).error(function (data, status, headers, config) {
                alert('error');
            })
        }
    };

}