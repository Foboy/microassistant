angular.module('microassistant', ['ngRoute']).
  config(['$provide', '$routeProvider', '$locationProvider', function ($provide, $routeProvider, $locationProvider) {
      $routeProvider
          .when('/home', { templateUrl: 'partials/home.html', controller: HomeMainCtrl })
          .when('/user', { templateUrl: 'partials/userinfo.html', controller: UserMainCtrl })
          .when('/mytimeshaft', { templateUrl: 'partials/mytimeshaft.html', controller: UserTimeShaftCtrl })
          .when('/changepassword', { templateUrl: 'partials/changepassword.html', controller: UserMainCtrl })
          .when('/changeemail', { templateUrl: 'partials/changeemail.html', controller: UserMainCtrl })
          .when('/product/:catalogId?/:pageIndex?', { templateUrl: 'partials/product.html', controller: ProductMainCtrl })
          .when('/sales/:steps?/:pageIndex?', { templateUrl: 'partials/sales.html', controller: SalesMainCtrl })
          .when('/finance/:steps?/:pageIndex?', { templateUrl: 'partials/finance.html', controller: FinanceMainCtrl })
          .when('/client/:sorts?/:pageIndex?', { templateUrl: 'partials/client.html', controller: ClientMainCtrl })
          .otherwise({ redirectTo: '/home' });
      $locationProvider.hashPrefix('!');

      $provide.decorator('$animate', ['$delegate', '$injector', '$sniffer', '$rootElement', '$timeout', '$rootScope',
        function ($delegate, $injector, $sniffer, $rootElement, $timeout, $rootScope) {
            var enter = $delegate.enter;
            $delegate.enter = function (element, parent, after, done) {
                enter(element, parent, after);
                $rootScope.$broadcast('animate-enter', element);
            }
            return $delegate;
        }]);
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
        if ($location.path().indexOf('/home') == 0 || $location.path().indexOf('/user') == 0 || $location.path().indexOf('/changepassword') == 0 || $location.path().indexOf('/changeemail') == 0 || $location.path().indexOf('/mytimeshaft')==0) {
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

    $scope.parseNumberToChinese = function (num) {
        console.log(num);
        if (!isNaN(num))
            num = Math.abs(num);
        if (isNaN(num) || num == 0)
            return '初次';
        var result = '第';
        var chinese = '一二三四五六七八九';
        if (num > 10) {
            result += chinese.charAt(Math.floor(num / 10)) + '十';
        }
        result += chinese.charAt(num % 10);
        return result;
    }

    $scope.parseJsonDate = function (datestr, format) {
        debugger;
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
                date = eval(datestr.replace(/Date\((\d+)\)/gi, "new Date($1)"));
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
    }).
    error(function (data, status, headers, config) {
        $scope.CurrentUser = {};
    });
}