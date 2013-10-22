angular.module('microassistant', ['ngRoute']).
  config(function($routeProvider, $locationProvider) {
  $routeProvider
  	  .when('/home', {templateUrl: 'partials/home.html', controller: HomeMainCtrl})
	  .when('/product/:catalogId?/:pageIndex?', {templateUrl: 'partials/product.html', controller: ProductMainCtrl})
	  .when('/sales/:steps?/:pageIndex?', {templateUrl: 'partials/sales.html', controller: SalesMainCtrl})
	  .when('/finance/:steps?/:pageIndex?', { templateUrl: 'partials/finance.html', controller: FinanceMainCtrl })
	  .otherwise({redirectTo: '/home'});
  	  $locationProvider.hashPrefix('!');
}).value('$anchorScroll', angular.noop);

function MainCtrl($scope, $routeParams, $http, $location){

	$scope.$on('$routeChangeSuccess',function(){
		$scope.checkpage();
	});
	
	$scope.checkpage = function(){
		if($location.path().indexOf('/home')==0)
		{
			$scope.page="home";
		}
		else if($location.path().indexOf('/product')==0)
		{
			$scope.page="product";
		}
		else if($location.path().indexOf('/sales')==0)
		{
			$scope.page="sales";
		}
		else if($location.path().indexOf('/finance')==0)
		{
			$scope.page="finance";
		}
		else
		{
			$scope.page="home";
		}
	};
	$scope.checkpage();
}