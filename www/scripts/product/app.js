angular.module('product', ['ngRoute','ngAnimate']).
  config(function($routeProvider, $locationProvider) {
  $routeProvider.
      when('/product', {template: '/product', controller: ProductListCtrl, controllerAs: 'pclr'})
	  .when('/product/:catalogId', {template: '/product/:catalogId', controller: ProductListCtrl, controllerAs: 'pclr'})
	  .when('/product/:catalogId/:pageIndex', {template: '/product/:catalogId/:pageIndex', controller: ProductListCtrl, controllerAs: 'pclr'})
	  .otherwise({redirectTo: '/product'});
});