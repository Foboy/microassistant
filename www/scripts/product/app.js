angular.module('product', ['ngRoute']).
  config(function($routeProvider, $locationProvider) {
  $routeProvider.
      when('/product', {templateUrl: 'partials/product/product-list.html',   controller: ProductListCtrl}).
      when('/product/:productId', {templateUrl: 'partials/product/product-detail.html', controller: ProductDetailCtrl}).
      otherwise({redirectTo: '/product'});
});