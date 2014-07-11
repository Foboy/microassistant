angular.module('microassistant', ['ngTouch', 'ngAnimate', 'models.user', 'models.respic', 'models.product', 'models.sales', 'models.finance', 'models.client']).
 config(['$provide', '$compileProvider', '$httpProvider', function ($provide, $compileProvider, $httpProvider) {

     $httpProvider.interceptors.push(function () {
         return {
             'response': function (response) {
                 if (response && typeof response.data === 'object') {
                     if (response.data.Error == 11) {
                         setTimeout(function () { angular.section("account-login"); }, 1);
                     }
                 }
                 return response || $q.when(response);
             }
         };
     });

     function makeEventLisenerDirective(directiveName, eventName) {
         $compileProvider.directive(directiveName, ['$parse', function ($parse) {
             return {
                 compile: function ($element, attr) {
                     var fn = $parse(attr[directiveName]);
                     return function (scope, element, attr) {
                         element.on(angular.lowercase(eventName), function (event) {
                             scope.$apply(function () {
                                 fn(scope, { $event: event });
                             });
                         });
                     };
                 }
             };
         }]);
     }

     makeEventLisenerDirective('ngRefresh', 'pullrefresh');
     makeEventLisenerDirective('ngTouch', 'touch');
     makeEventLisenerDirective('ngTap', 'tap');
 }])
    .constant('$sitecore', $sitecore)
    .constant('$pagination', { pageindex: 0, pagesize: 10 })
    .value('$anchorScroll', angular.noop)
    .service('$dataCache', ['$cacheFactory', function ($cacheFactory) {
        this.getCache = function (key) {
            var cache = $cacheFactory.get(key);
            if (!cache) {
                cache = $cacheFactory(key);
            }
            return cache;
        };
        this.getUserCache = function () {
            return this.getCache('userCacheKey');
        };
        this.getListCache = function (catalogKey) {
            return this.getCache(catalogKey);
        };
    }])
    .service('$routeParams', function () {
        return {};
    })
    .service('$environment', function () {
        return Lungo.Core.environment();;
    });

function MainCtrl($scope, $rootScope, $http, $filter, usermodel, respicmodel, $environment) {

    var updataUser = function () {
        usermodel.loadCurrentUser(function (data) {
            $rootScope.CurrentUser = data.Data;
            if (data.Data.PicId > 0) {
                respicmodel.get(data.Data.PicId, function (data) {
                    $rootScope.HeadPicUrl = $sitecore.wrapsrc(data.Data.PicUrl);
                });
            }
            else {
                $rootScope.HeadPicUrl = 'img/Adimg/tx.png';
            }
        }, function () {
            $rootScope.CurrentUser = {};
        });
    }

    $scope.env = angular.copy( $environment);

    $scope.$on('onLoginSuccess', function () {
        updataUser();
    });
    updataUser();

    $scope.loadSection = function (name) {
        angular.loadSection(name);
    }

    $scope.showMenu = function (ev) {
        angular.showMenu(ev, Lungo.Element.Cache.section);
    };

    $scope.hideMenu = function (ev) {
        angular.hideMenu(ev);
    };

    $scope.menuToSection = function (sectionId, resource) {
        var target = Lungo.dom("#" + sectionId);
        if (resource) {
            if (target.length) {
            }
            else {
                Lungo.Resource.load(resource);
                Lungo.Boot.Data.init("#" + sectionId);
                target = Lungo.dom("#" + sectionId);
                //target.trigger('load');
            }
        }
        //Lungo.dom("#contentWraper").append(Lungo.dom("#" + sectionId));
        //angular.section(sectionId);
        if (sectionId != Lungo.Element.Cache.section.attr('id'))
        {
            angular.changeMenuContent(target);
            var current = Lungo.Element.Cache.section;
            current.removeClass(Lungo.Constants.CLASS.SHOW);
            target.addClass(Lungo.Constants.CLASS.SHOW);

            Lungo.Section.show(current, target);
            Lungo.Router.step(sectionId);

            //Lungo.Element.Cache.section.removeClass(Lungo.Constants.CLASS.SHOW);
            //Lungo.dom("#" + sectionId).addClass(Lungo.Constants.CLASS.SHOW);
        }


        angular.hideMenu({});
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
        else if (num >= 20) {
            result += chinese.charAt(Math.floor(num / 10) - 1) + '十';
        }
        if ((num % 10) > 0)
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
}

