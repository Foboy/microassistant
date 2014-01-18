angular.module('microassistant', ['ngRoute','ui.router', 'ngLoadMask']).
  //config(['$provide', '$routeProvider', '$locationProvider',function ($provide, $routeProvider, $locationProvider) {
  //    $routeProvider
  //        .when('/home', { templateUrl: 'partials/home.html', controller: HomeMainCtrl })
  //        .when('/user', { templateUrl: 'partials/userinfo.html', controller: UserMainCtrl })
  //        .when('/mytimeshaft', { templateUrl: 'partials/mytimeshaft.html', controller: UserTimeShaftCtrl })
  //        .when('/staffmangement', { templateUrl: 'partials/staffmangement.html', controller: StaffMangementCtrl })
  //        .when('/changepassword', { templateUrl: 'partials/changepassword.html', controller: UserMainCtrl })
  //        .when('/changeemail', { templateUrl: 'partials/changeemail.html', controller: UserMainCtrl })
  //        .when('/enterpriseinfo', { templateUrl: 'partials/enterpriseinfo.html', controller: EnterPriseInfoCtrl })
  //        .when('/product/:catalogId?/:pageIndex?', { templateUrl: 'partials/product.html', controller: ProductMainCtrl })
  //        .when('/sales/:steps?/:pageIndex?', { templateUrl: 'partials/sales.html', controller: SalesMainCtrl })
  //        .when('/finance/:steps?/:pageIndex?', { templateUrl: 'partials/finance.html', controller: FinanceMainCtrl })
  //        .when('/client/:sorts?/:pageIndex?', { templateUrl: 'partials/client.html', controller: ClientMainCtrl })
  //        .otherwise({ redirectTo: '/home' });
  //    $locationProvider.hashPrefix('!');

//}]).value('$anchorScroll', angular.noop);
 config(['$provide', '$httpProvider', '$routeProvider', '$stateProvider', '$urlRouterProvider', function ($provide, $httpProvider,$routeProvider, $stateProvider, $urlRouterProvider) {

     $routeProvider
        .when('/home', { template: '', controller: function () { } })
        .when('/boss', { template: '', controller: function () { } })
        .when('/user', { template: '', controller: function () { } })
        .when('/mytimeshaft', { template: '', controller: function () { } })
        .when('/staffmangement', { template: '', controller: function () { } })
        .when('/changepassword', { template: '', controller: function () { } })
        .when('/changeemail', { template: '', controller: function () { } })
        .when('/enterpriseinfo', { template: '', controller: function () { } })
        .when('/product/:catalogId?/:pageIndex?', { template: '', controller: function () { } })
        .when('/sales/:steps?/:pageIndex?', { template: '', controller: function () { } })
        .when('/finance/:steps?/:pageIndex?', { template: '', controller: function () { } })
        .when('/client/:sorts?/:pageIndex?', { template: '', controller: function () { } })
        .otherwise({ redirectTo: '/home' });

     $stateProvider
         .state("main", {
             url: "",
             templateUrl: 'partials/main.html'
         })
         .state("boss", {
             url: "^/boss",
             templateUrl: 'partials/boss.html',
             controller: UserBossMainCtrl
         })
         .state('main.home', {
             url: '/home',
             templateUrl: 'partials/home.html',
             controller: HomeMainCtrl
         })
         .state("main.product", {
             url: "/product*path",
             templateUrl: 'partials/product.html',
             controller: ProductMainCtrl
         })
         .state('main.user', { url: '/user*path', templateUrl: 'partials/userinfo.html', controller: UserMainCtrl })
         .state('main.mytimeshaft', { url: '/mytimeshaft*path', templateUrl: 'partials/mytimeshaft.html', controller: UserTimeShaftCtrl })
         .state('main.staffmangement', { url: '/staffmangement*path', templateUrl: 'partials/staffmangement.html', controller: StaffMangementCtrl })
         .state('main.changepassword', { url: '/changepassword*path', templateUrl: 'partials/changepassword.html', controller: UserMainCtrl })
         .state('main.changeemail', { url: '/changeemail*path', templateUrl: 'partials/changeemail.html', controller: UserMainCtrl })
         .state('main.enterpriseinfo', { url: '/enterpriseinfo*path', templateUrl: 'partials/enterpriseinfo.html', controller: EnterPriseInfoCtrl })
         .state('main.sales', { url: '/sales*path', templateUrl: 'partials/sales.html', controller: SalesMainCtrl })
         .state('main.finance', { url: '/finance*path', templateUrl: 'partials/finance.html', controller: FinanceMainCtrl })
         .state('main.client', { url: '/client*path', templateUrl: 'partials/client.html', controller: ClientMainCtrl });

     $httpProvider.interceptors.push(function () {
         return {
             'response': function (response) {
                 if (response && typeof response.data === 'object') {
                     if (response.data.Error == 11)
                     {
                         setTimeout(function () { window.location.href = 'login.html'; }, 3000);
                     }
                 }
                 return response || $q.when(response);
             }
         };
     });
    }])
    .value('$anchorScroll', angular.noop)
    .run(
      ['$rootScope', '$state', '$stateParams',
      function ($rootScope, $state, $stateParams) {
          $rootScope.$state = $state;
          $rootScope.$stateParams = $stateParams;
      }]);;

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
    //右上角效果
    $scope.RightMenuMouserEnter = function () {
        var div = $("#myinfodiv");
        div.addClass("cur");
        $("#uinfolist").show();
        div.live("click", function () {
            $(this).removeClass("cur");
            $("#uinfolist").hide();
        });
    }
    $scope.RightMenuMouseLeave = function () {
        $("#myinfodiv").removeClass("cur");
        $("#uinfolist").hide();
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
        var date;
        if (!datestr ||datestr=="/Date(-62135596800000)/") {
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
    //根据生日计算年龄
    $scope.parseAgeFromBirthday = function (time) {
        var birthday = $scope.parseJsonDate(time);
        if (typeof birthday == 'object') {
            var d = new Date();
            var age = d.getFullYear() - birthday.getFullYear() - ((d.getMonth() < birthday.getMonth() || d.getMonth() == birthday.getMonth() && d.getDate() < birthday.getDate()) ? 1 : 0);
            return age;
        } else {
            return 0;
        }
    }
   
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
    $scope.HeadPicUrl = 'img/Adimg/tx.png';
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