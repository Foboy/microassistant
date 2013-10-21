
function SalesMainCtrl($scope, $routeParams, $http, $location){
	$scope.steps = $routeParams.steps;
	if(!$scope.steps)
		$scope.steps = "chance";
	console.log( $routeParams)
	//显示列表内容
	$scope.loadCurrentStepList = function(){
		switch($scope.steps)
		{
			case 'chance':
				$http.get($sitecore.urls["productList"],{params:{pageIndex:$routeParams.pageIndex||1}}).success(function(data) {
					console.log(data);
				  $scope.ActPageIndex = $routeParams.pageIndex||1;
				  $scope.chances = data;
				}).
				error(function(data, status, headers, config) {
				  $scope.chances = [];
				});
				break;
			case 'visit':
				$http.get($sitecore.urls["productList"],{params:{pageIndex:$routeParams.pageIndex||1}}).success(function(data) {
					console.log(data);
				  $scope.ActPageIndex = $routeParams.pageIndex||1;
				  $scope.visits = data;
				}).
				error(function(data, status, headers, config) {
				  $scope.visits = [];
				});
				break;
			case 'contract':
				$http.get($sitecore.urls["productList"],{params:{pageIndex:$routeParams.pageIndex||1}}).success(function(data) {
					console.log(data);
				  $scope.ActPageIndex = $routeParams.pageIndex||1;
				  $scope.contracts = data;
				}).
				error(function(data, status, headers, config) {
				  $scope.contracts = [];
				});
				break;
			case 'after':
				$http.get($sitecore.urls["productList"],{params:{pageIndex:$routeParams.pageIndex||1}}).success(function(data) {
					console.log(data);
				  $scope.ActPageIndex = $routeParams.pageIndex||1;
				  $scope.afters = data;
				}).
				error(function(data, status, headers, config) {
				  $scope.afters = [];
				});
				break;
		}
	};
	$scope.addCurrentStepItem = function(){
		switch($scope.steps)
		{
			case 'chance':
				$scope.$broadcast('EventAddChance');
				break;
			case 'visit':
				$scope.$broadcast('EventAddVisit');
				break;
			case 'contract':
				$scope.$broadcast('EventAddContract');
				break;
			case 'after':
				$scope.$broadcast('EventAddAfter');
				break;
		}
	};
	
	$scope.showChanceDetail = function(){
		$scope.$broadcast('EventChanceDetail');
	};
	$scope.showVisitDetail = function(){
		$scope.$broadcast('EventVisitDetail');
	};
	$scope.showContractDetail = function(){
		$scope.$broadcast('EventContractDetail');
	};
	$scope.showAfterDetail = function(){
		$scope.$broadcast('EventAfterDetail');
	};

}

function SalesChanceDetailCtrl($scope, $routeParams, $http, $location){
	$scope.$on('EventChanceDetail',function(event){
		$("#chanceDetailBox").animate({width:"350px"},300);
		//加载机会数据
	});
	
	$scope.hideChanceDetail = function(){
	  $("#chanceDetailBox").animate({width:"0px"},300,function(){});
  	};
};
function SalesVisitDetailCtrl($scope, $routeParams, $http, $location){
	$scope.$on('EventVisitDetail',function(event){
		$("#visitDetailBox").animate({width:"900px"},500);
		//加载机会数据
	});
	
	$scope.hideVisitDetail = function(){
	  $("#visitDetailBox").animate({width:"0px"},500,function(){});
  	};
	
	$scope.selectVisitLocation = function(){
		$("#salesVisitMapIframe").attr({src:'partials/others/locationselectmap.html?lat='+$scope.visit_lat+'&lng='+$scope.visit_lng})
		$('#visitLocationSelectModal').modal('show');
	};
	
	utilities.registeriframelistener("visitlocation",function(){
		$('#visitLocationSelectModal').modal('hide');
		$scope.visit_lat = arguments[0];
		$scope.visit_lng = arguments[1];
	});
};
function SalesContractDetailCtrl($scope, $routeParams, $http, $location){
	$scope.$on('EventContractDetail',function(event){
		$("#contractDetailBox").animate({width:"500px"},400);
		//加载机会数据
	});
	
	$scope.hideContractDetail = function(){
	  $("#contractDetailBox").animate({width:"0px"},400,function(){});
  	};
};
function SalesAfterDetailCtrl($scope, $routeParams, $http, $location){
};
