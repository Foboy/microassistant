angular.module('product', ['ngRoute']).
  config(function($routeProvider, $locationProvider) {
  $routeProvider.
      when('/product', {template: '&nbsp;', controller: ProductListCtrl})
	  .when('/product/:catalogId/:pageIndex', {template: '&nbsp;', controller: ProductListCtrl})
	  .otherwise({redirectTo: '/product'});
  	  $locationProvider.hashPrefix('!');
}).value('$anchorScroll', angular.noop);