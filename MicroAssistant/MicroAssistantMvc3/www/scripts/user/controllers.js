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

    $(document).keyup(function (e) {
        if (e.keyCode == 13) {
            $scope.$apply(function () {
                $scope.UserLogin();
            });
        }
    });
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

    $(document).keyup(function (e) {
        if (e.keyCode == 13) {
            $scope.$apply(function () {
                $scope.UserRegister();
            });
        }
    });
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

    $(document).keyup(function (e) {
        if (e.keyCode == 13) {
            $scope.$apply(function () {
                $scope.EnterpriseRegister();
            });
        }
    });
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
                //$scope.parseAge(data.Data.Birthday)
                $scope.UserInfo.Age = 12;
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
    //修改密码
    $scope.ChangePwdSumbit = function (data) {
        if ($scope.ChangePwdForm.$valid) {
            if (data.surepwd != data.newpwd) {
                $scope.ChangePwdForm.newpwd.$valid = false;
                $scope.showerror = true;
            } else {
                $scope.showerror = false;
                $http.post($sitecore.urls["UpdatePwd"], { oldpwd: data.oldpwd, newpwd: data.newpwd }).success(function (data) {
                    if (data.Error) {
                        alert(data.ErrorMessage);
                    }
                    console.log(data);
                }).
                error(function (data, status, headers, config) {
                    alert("error")
                });
            }
        }
        else {
            $scope.showerror = true;
        }
    };
    //修改邮箱
    $scope.ChangeEmailSubmit = function (data) {
        if ($scope.ChangeEmailForm.$valid) {
            $scope.showerror = false;
            $http.post($sitecore.urls["UpdateEmail"], { email: data.newemail, pwd: data.pwd }).success(function (data) {
                if (data.Error) {
                    alert(data.ErrorMessage);
                } else {
                    alert("修改成功");
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

function UserTimeShaftCtrl($scope, $http, $location) {
    $scope.LoadMyTimeShaft = function () {
        $http.post($sitecore.urls["SearchUserTimeMachine"], {}).success(function (data) {
            if (!data.Error) {
                $scope.UserTimeShafts = data.Data;
            } else {
               
            }
            console.log(data);
        }).
              error(function (data, status, headers, config) {
                  alert("error")
              });
    }
    $scope.LoadMyTimeShaft();
    $http.ShowAddCompany = function () {
        $scope.$broadcast('EventAddCompany', this.timeshaftItem);
    }
}
function AddCompanyCtrl($scope, $http, $location)
{
    var formdata;
    $scope.$on("EventAddCompany", function (event, data) {
        formdata = data;
        $("#AddCompanyBox").modal('show');
    });
    $scope.AddCompanySubmit = function () {
        if ($scope.AddCompanyForm.$valid) {
            $scope.showerror = false;
            $http.post($sitecore.urls["AddOrUpdateEnterPriseClient"], { customerEntId: $scope.EnterpriseItem.CustomerEntId, entName: $scope.EnterpriseItem.EntName, industy: $scope.EnterpriseItem.Industy, contactUsername: $scope.EnterpriseItem.ContactUsername, contactMobile: $scope.EnterpriseItem.ContactMobile, phone: $scope.EnterpriseItem.ContactPhone, email: $scope.EnterpriseItem.ContactEmail, qq: '', address: $scope.EnterpriseItem.Address, Detail: $scope.EnterpriseItem.Detail }).success(function (data) {
                if (data.Error) {
                    alert(data.ErrorMessage);
                }
                else {
                    $("#AddEnterpriseBox").modal('hide');
                    $scope.loadCurrentSortList();
                }
            }).
            error(function (data, status, headers, config) {
                //$scope.product = {};
            });
        }
        else {
            $scope.showerror = true;
        }
    };
}