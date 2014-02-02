function UserLoginMainCtrl($scope, $http, $location, $rootScope, usermodel) {
    $scope.User = { email: 'aaa@aa.com', pwd: 'aaa' };
    $scope.hasPermission = function (id) {
        if ($rootScope.CurrentUser && $rootScope.CurrentUser.userFuns && $rootScope.CurrentUser.userFuns.length) {
            for (var i = 0; i < $rootScope.CurrentUser.userFuns.length; i++) {
                if ($rootScope.CurrentUser.userFuns[i].IdsysFunction == id)
                    return true;
            }
        }
        return false;
    };
    $scope.UserLogin = function () {
        if ($scope.UserLoginForm.$valid) {
            $scope.showerror = false;
            //$.showMsg("登陆成功", 's');
            usermodel.login($scope.User.email, $scope.User.pwd, function (data) {
                $rootScope.$broadcast('onLoginSuccess', data);
                angular.section("main");
            });
        }
        else {
            $scope.showerror = true;
        }

    }
}

function UserRegisterMainCtrl($scope, $http, $rootScope, usermodel) {
    console.log($scope);
    $scope.UserRegister = function () {
        console.log(angular.toJson($scope.User));
        if ($scope.UserRegisterForm.$valid) {
            $scope.showerror = false;
            usermodel.register($scope.User.name, $scope.User.email, $scope.User.pwd, $scope.User.enterprise, function (data) {
                $rootScope.$broadcast('onLoginSuccess', data);
                angular.section("main");
            });
        }
        else {
            $scope.showerror = true;
        }
    }

}

function EnterpriseRegisterMainCtrl($scope, $http, $rootScope, usermodel) {
    $scope.EnterpriseRegister = function () {
        console.log(angular.toJson($scope.Enterprise));
        if ($scope.EnterpriseRegisterForm.$valid) {
            $scope.showerror = false;
            usermodel.entRegister($scope.Enterprise.name, $scope.Enterprise.email, $scope.Enterprise.pwd, function (data) {
                $rootScope.$broadcast('onLoginSuccess', data);
                angular.section("main");
            });
        }
        else {
            $scope.showerror = true;
        }
    }

    $scope.loginToSystem = function () {
        $.fancybox.close();
        $.pagePreLoading("index.html", function () { window.location.href = "index.html"; });
    };

}


function UserMainCtrl($scope, $http, $location) {
    $scope.GetCurrentUserInfo = function () {
        $http.post($sitecore.urls["GetCurrentUserInfo"], {}).success(function (data) {
            if (data.Error) {
                alert(data.ErrorMessage);
            } else {
                $scope.UserInfo = data.Data;
                $scope.UserInfo.Age = $scope.parseAgeFromBirthday($scope.UserInfo.Birthday);
                if ($scope.UserInfo != null && $scope.UserInfo.Sex == 0) {
                    $scope.UserInfo.Sex = 1;
                }
            }
        }).error(function (data, status, headers, config) {
            alert('error');
        }).lock({ selector: '#EditUserFormZone' });
    };
    $scope.GetCurrentUserInfo();
    $scope.EditUserInfo = function (data) {
        if ($scope.EditUserForm.$valid) {
            $http.post($sitecore.urls["EditCurrentUserInfo"], { username: data.UserName, nickname: data.UserAccount, sex: data.Sex, age: data.Age }).success(function (data) {
                if (data.Error) {
                    alert(data.ErrorMessage, 'e');
                } else {
                    alert("修改成功", 's');
                }
            }).error(function (data, status, headers, config) {
                alert('error');
            }).lock({ selector: '#EditUserFormZone' });
        }
    };
    //修改密码
    $scope.ChangePwdSumbit = function (data) {
        if ($scope.ChangePwdForm.$valid) {
            if (data.surepwd != data.newpwd) {
                $scope.ChangePwdForm.newpwd.$valid = false;
                alert("新密码和确认密码不一致", 'w');
                $scope.showerror = true;
            } else {
                $scope.showerror = false;
                $http.post($sitecore.urls["UpdatePwd"], { oldpwd: data.oldpwd, newpwd: data.newpwd }).success(function (data) {
                    if (data.Error) {
                        alert(data.ErrorMessage, 'e');
                    } else {
                        alert("修改成功", 's')
                    }
                }).
                error(function (data, status, headers, config) {
                    alert("error")
                }).lock({ selector: '#ChangePwdFormZone' });
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
                    alert(data.ErrorMessage, 'e');
                } else {
                    alert("修改成功", 's');
                }
                console.log(data);
            }).
            error(function (data, status, headers, config) {
                alert("error", 'w')
            }).lock({ selector: '#ChangeEmailFormZone' });

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
                alert(data.Error, 'e');
            }
        }).
              error(function (data, status, headers, config) {
                  alert("error", 'w');
              }).lock({ selector: '#mytimeshaftZone' });
    }
    $scope.LoadMyTimeShaft();
    $scope.ShowAddCompany = function () {
        $scope.$broadcast('EventAddCompany');
    }
}
function AddCompanyCtrl($scope, $http, $location) {
    $scope.$on("EventAddCompany", function (event) {
        $scope.AddComItem = { username: $scope.CurrentUser.UserName };
        $("#AddCompanyBox").modal('show');
    });
    $scope.AddCompanySubmit = function (data) {
        if ($scope.AddCompanyForm.$valid) {
            $scope.showerror = false;
            $http.post($sitecore.urls["EditeUserEntCode"], { username: data.username, entCode: data.enterpriseid }).success(function (data) {
                if (!data.Error) {
                    alert("保存成功", 's');
                    $("#AddCompanyBox").modal('hide');
                    $scope.LoadMyTimeShaft();
                }
                else {
                    alert(data.ErrorMessage, 'e');
                }
            }).
            error(function (data, status, headers, config) {
                //$scope.product = {};
            }).lock({ selector: '#AddCompanyBox' });
        }
        else {
            $scope.showerror = true;
        }
    };
}
function StaffMangementCtrl($scope, $http, $location) {
    $(".peop-box").live("mouseenter", function () {
        $(this).find(".s-c").show();
    });
    $(".peop-box").live("mouseleave", function () {
        $(this).find(".s-c").hide();
    });
    $scope.selectedRoleid = 0;
    $scope.SearchEntRole = function () {
        $http.post($sitecore.urls["SearchEntRole"], {}).success(function (data) {
            if (!data.Error) {
                $scope.SysRoles = data.Data;
                $scope.SelectedOptions = [];
                for (var i = 1; i < $scope.SysRoles.length; i++) {
                    $scope.SelectedOptions.push($scope.SysRoles[i]);
                }
                $scope.SearchUserListByRoleId(0);
            } else { $scope.SysRoles = []; }
        }).error(function (data, status, headers, config) {
            $scope.SysRoles = [];
        });
    }
    $scope.SearchEntRole();//初始化
    $scope.SearchUserListByRoleId = function (RoleId) {
        if (RoleId != null) {
            $scope.selectedRoleid = RoleId;
            $http.post($sitecore.urls["SearchUserListByRoleId"], { roleId: RoleId }).success(function (data) {
                if (!data.Error) {
                    $scope.SysUsers = data.Data;
                    for (var i = 0; i < data.Data.length; i++) {
                        for (var j = 0; j < $scope.SelectedOptions.length; j++) {
                            if (data.Data[i].RoleId == $scope.SelectedOptions[j].RoleId) {
                                data.Data[i].SelectedItem = $scope.SelectedOptions[j];
                            }
                        }
                    }

                } else { $scope.SysUsers = []; }
            }).error(function (data, status, headers, config) {
                $scope.SysUsers = [];
            }).lock({ selector: '#staffmanagementZone' });
        }
    };
    $scope.ChangeUserRole = function (item, user) {
        $http.post($sitecore.urls["UpdateUserRole"], { userId: user.UserId, roleId: item.RoleId }).success(function (data) {
            if (!data.Error) {
                alert("修改成功", 's');
                $scope.SearchEntRole();
            } else {
                alert(data.ErrorMessage, 'e');
            }
        }).error(function (data, status, headers, config) {

        })
    }
}
function EnterPriseInfoCtrl($scope, $http, $location) {
    //修改企业code
    $scope.EditCurrentEntCode = function (data) {
        if ($scope.ChangeEnterprsieForm.Code.$valid) {
            $scope.showerror = false;
            $http.post($sitecore.urls["AdminEditEntCode"], { entCode: data.EntCode, entId: data.EntId }).success(function (data) {
                if (!data.Error) {
                    alert("修改成功", 's');
                } else { }
            }).error(function (data, status, headers, config) {
                $scope.SysUsers = [];
            }).lock({ selector: '#ChangeEnterprsieZone' });
        } else {
            $scope.showerror = true;
        }
    }
    //修改企业名字entName
    $scope.EditCurrentUserName = function (data) {
        if ($scope.ChangeEnterprsieForm.Name.$valid) {
            $scope.showerror = false;
            $http.post($sitecore.urls["AdminEditEntName"], { entName: data.UserName, entId: data.EntId }).success(function (data) {
                if (!data.Error) {
                    alert("修改成功", 's');
                } else { }
            }).error(function (data, status, headers, config) {
                $scope.SysUsers = [];
            }).lock({ selector: '#ChangeEnterprsieZone' });
        } else {
            $scope.showerror = true;
        }
    }
    //一键清空数据（boss）
    $scope.DeleteAllData = function () {
        $http.post($sitecore.urls["DeleteAllData"], {}).success(function (data) {
            if (!data.Error) {
                alert("清除成功", 's');
            } else {
                alert(data.ErrorMessage, 'e');
            }
        }).error(function (data, status, headers, config) {
            alert(data.ErrorMessage, 'e');
        }).lock({ selector: '#ChangeEnterprsieZone' });
    }
}