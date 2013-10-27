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

	$scope.parseNumberToChinese = function(num)
	{
	    console.log(num);
	    if (!isNaN(num))
	        num = Math.abs(num);
	    if (isNaN(num) || num == 0)
	        return '初次';
	    var result = '第';
	    var chinese = '一二三四五六七八九';
	    if (num > 10)
	    {
	        result += chinese.charAt(Math.floor(num / 10))+'十';
	    }
	    result += chinese.charAt(num % 10);
	    return result;
	}

	$scope.parseJsonDate = function (datestr) {
	    console.log(typeof (new Date()));
	    var date;
	    if (!datestr) {
	        date = new Date();
	    }
	    else if (typeof datestr == 'object') {
	        return datestr;
	    }
	    else if (typeof datestr == 'string') {
	        datestr = datestr.replace(/\//g, '');
	        date = eval(datestr.replace(/Date\((\d+)\)/gi, "new Date($1)"));
	        console.log(datestr.replace(/Date\((\d+)\)/gi, "new Date($1)"));
	        console.log(date);
	    }
	    else {
	        date = new Date();
	    }
	    return date;
	};
}