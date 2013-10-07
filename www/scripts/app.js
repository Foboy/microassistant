angular.module('microassistant', ['ngRoute']).
  config(function($routeProvider, $locationProvider) {
  $routeProvider
	  .when('/product/:catalogId?/:pageIndex?', {templateUrl: 'partials/product.html', controller: ProductMainCtrl})
	  .when('/sales/:steps?/:pageIndex?', {templateUrl: 'partials/sales.html', controller: SalesMainCtrl})
	  .otherwise({redirectTo: '/product'});
  	  $locationProvider.hashPrefix('!');
}).value('$anchorScroll', angular.noop);