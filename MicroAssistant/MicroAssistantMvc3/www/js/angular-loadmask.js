/*
*   create by cray (285025600@qq.com)
*   2013/11/21
*/
(function (window, angular, undefined) {
    'use strict';
    angular.module('ngLoadMask', ['ng']).
      config(['$provide', '$locationProvider', '$httpProvider', function ($provide, $locationProvider, $httpProvider) {

          $provide.decorator('$http', ['$delegate', '$q', function ($oldhttp) {
              //console.log($oldhttp);

              var forEach = angular.forEach;
              var extend = angular.extend;

              var $http = function (requestConfig) {
                  var loadingConfit;
                  var result = $oldhttp(requestConfig).
                      success(function () {
                          if (loadingConfit && loadingConfit.hideOnSuccess) {
                              $(loadingConfit.selector).unmask();
                          }
                      }).
                      error(function () {
                          if (loadingConfit && loadingConfit.hideOnError) {
                              $(loadingConfit.selector).unmask();
                          }
                      });
                  result.lock = function (config) {
                      loadingConfit = extend({
                          hideOnSuccess: true,
                          hideOnError: true,
                          offsetY:0
                      },config);
                      var elt = $(loadingConfit.selector);
                      if (!elt.isMasked()) {
                          elt.mask('');
                      }
                      return result;
                  };
                  //console.log(result);
                  return result;
              }

              createShortMethods('get', 'delete', 'head', 'jsonp');
              createShortMethodsWithData('post', 'put');

              return $http;

              function createShortMethods(names) {
                  forEach(arguments, function (name) {
                      $http[name] = function (url, config) {
                          return $http(extend(config || {}, {
                              method: name,
                              url: url
                          }));
                      };
                  });
              }


              function createShortMethodsWithData(name) {
                  forEach(arguments, function (name) {
                      $http[name] = function (url, data, config) {
                          return $http(extend(config || {}, {
                              method: name,
                              url: url,
                              data: data
                          }));
                      };
                  });
              }

          }]);

      }]);
})(window, window.angular);