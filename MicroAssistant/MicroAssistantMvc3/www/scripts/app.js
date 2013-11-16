angular.module('microassistant', ['ngRoute']).
  config(['$provide', '$routeProvider', '$locationProvider', function ($provide, $routeProvider, $locationProvider) {
      $routeProvider
          .when('/home', { templateUrl: 'partials/home.html', controller: HomeMainCtrl })
          .when('/user', { templateUrl: 'partials/userinfo.html', controller: UserMainCtrl })
          .when('/mytimeshaft', { templateUrl: 'partials/mytimeshaft.html', controller: UserTimeShaftCtrl })
          .when('/staffmangement', { templateUrl: 'partials/staffmangement.html', controller: StaffMangementCtrl })
          .when('/changepassword', { templateUrl: 'partials/changepassword.html', controller: UserMainCtrl })
          .when('/changeemail', { templateUrl: 'partials/changeemail.html', controller: UserMainCtrl })
          .when('/enterpriseinfo', { templateUrl: 'partials/enterpriseinfo.html', controller: EnterPriseInfoCtrl })
          .when('/product/:catalogId?/:pageIndex?', { templateUrl: 'partials/product.html', controller: ProductMainCtrl })
          .when('/sales/:steps?/:pageIndex?', { templateUrl: 'partials/sales.html', controller: SalesMainCtrl })
          .when('/finance/:steps?/:pageIndex?', { templateUrl: 'partials/finance.html', controller: FinanceMainCtrl })
          .when('/client/:sorts?/:pageIndex?', { templateUrl: 'partials/client.html', controller: ClientMainCtrl })
          .otherwise({ redirectTo: '/home' });
      $locationProvider.hashPrefix('!');

  }]).value('$anchorScroll', angular.noop);

function MainCtrl($scope, $routeParams, $http, $location, $filter) {
    //退出登录
    $scope.UserLoginOut = function () {
        $http.post($sitecore.urls["Logout"], {}).success(function (data) {
            if (data.Error) {
                window.location.href = "login.html";
            } else {
                window.location.href = "login.html";
            }
        }).error(function (data, status, headers, config) {
            alert('error');
        })
    }



    $scope.$on('$routeChangeSuccess', function () {
        $scope.checkpage();
    });

    $scope.CurrentUser = null;

    $scope.checkpage = function () {
        if ($location.path().indexOf('/home') == 0 || $location.path().indexOf('/user') == 0 || $location.path().indexOf('/changepassword') == 0 || $location.path().indexOf('/changeemail') == 0 || $location.path().indexOf('/mytimeshaft') == 0 || $location.path().indexOf('/staffmangement') == 0 || $location.path().indexOf('/enterpriseinfo') == 0) {
            $scope.page = "home";
        }
        else if ($location.path().indexOf('/product') == 0) {
            $scope.page = "product";
        }
        else if ($location.path().indexOf('/sales') == 0) {
            $scope.page = "sales";
        }
        else if ($location.path().indexOf('/finance') == 0) {
            $scope.page = "finance";
        }
        else if ($location.path().indexOf('/client') == 0) {
            $scope.page = "client";
        }
        else {
            $scope.page = "home";
        }
    };
    $scope.checkpage();

    $scope.hasPermission = function (id) {
        if ($scope.CurrentUser && $scope.CurrentUser.userFuns && $scope.CurrentUser.userFuns.length) {
            for (var i = 0; i < $scope.CurrentUser.userFuns.length; i++) {
                if ($scope.CurrentUser.userFuns[i].IdsysFunction == id)
                    return true;
            }
        }
        return false;
    };

    $scope.permissionCheck = function () {
        $scope.productPermission = $scope.hasPermission(1);
        $scope.salesPermission = $scope.hasPermission(17);
        $scope.financePermission = $scope.hasPermission(11);
        $scope.clientPermission = $scope.hasPermission(6);
        $scope.bossPerission = $scope.hasPermission(27);
        $scope.enterpriseManagementPerission = $scope.hasPermission(25);
        $scope.staffManagementPerission = $scope.hasPermission(26);
    };

    $scope.parseNumberToChinese = function (num) {
        console.log(num);
        if (!isNaN(num))
            num = Math.abs(num);
        if (isNaN(num) || num == 0)
            return '零';
        var result = '';
        var chinese = '一二三四五六七八九';
        if (num >= 10 && num < 20) {
            result += '十';
        }
        else if (num >= 20)
        {
            result += chinese.charAt(Math.floor(num / 10) - 1) + '十';
        }
        if ((num % 10) >0)
            result += chinese.charAt((num % 10) - 1);
        return result;
    }

    $scope.parseJsonDate = function (datestr, format) {
        //console.log(typeof (new Date()));
        var date;
        if (!datestr) {
            date = new Date();
        }
        else if (typeof datestr == 'object') {
            date = datestr;
        }
        else if (typeof datestr == 'string') {
            if ((/Date/ig).test(datestr)) {
                datestr = datestr.replace(/\//g, '');
                date = eval(datestr.replace(/Date\(([\-\d]+)\)/gi, "new Date($1)"));
                //console.log(date);
                //console.log(datestr.replace(/Date\((\d+)\)/gi, "new Date($1)"));
                //console.log(date);
            }
            else
                return datestr;
        }
        else {
            date = new Date();
        }
        if (format)
            return $filter('date')(date, format);
        return date;
    };
    //$scope.parseAge = function (datestr) {
    //    var birthday = new Date($scope.parseJsonDate(datestr).replace(/:/g, "\/"));
    //    var d = new Date();
    //    var age = d.getFullYear() - birthday.getFullYear() - ((d.getMonth() < birthday.getMonth() || d.getMonth() == birthday.getMonth() && d.getDate() < birthday.getDate()) ? 1 : 0);
    //    console.log(age);
    //    return age;
    //}

    $scope.setUserHeadImg = function () {
        $("#salesUserHeadImageIframe").attr({ src: 'partials/others/photocutter.html' })
        $('#userHeadImageSelectModal').modal('show');
    }
    $scope.HeadPicUrl = 'img/Adimg/h1.jpg';
    utilities.registeriframelistener("event:userHeadImageSeted", function () {
        $('#userHeadImageSelectModal').modal('hide');
        var imgUrl = arguments[0];
        $scope.$apply(function () {
            $scope.HeadPicUrl = imgUrl;
        });
    });

    $scope.PLog = function(obj)
    {
        console.log(obj);
    }

    $http.post($sitecore.urls["userCurrentUser"], {}).success(function (data) {
        console.log(data);
        if (data.Error) {
            alert(data.ErrorMessage);
        }
        $scope.CurrentUser = data.Data;
        $scope.permissionCheck();
        if (data.Data.PicId > 0)
        {
            $http.post($sitecore.urls["GetPic"], { picid: data.Data.PicId }).success(function (picdata) {
                $scope.HeadPicUrl = picdata.Data.PicUrl;
            }).
            error(function (data, status, headers, config) {
                $scope.HeadPicUrl = '';
            });
        }
    }).
    error(function (data, status, headers, config) {
        $scope.CurrentUser = {};
    });


}