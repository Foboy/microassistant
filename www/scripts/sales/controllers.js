var map;
function SalesMainCtrl($scope, $routeParams, $http, $location){

	utilities.loadscript("http://api.map.baidu.com/api?v=1.5&ak=88d118f650d753222ebb16aa51d5590a",function(){
		utilities.loadscript("http://api.map.baidu.com/library/MarkerTool/1.2/src/MarkerTool_min.js",function(){
		map = new BMap.Map("MapContent"); 
		var point = new BMap.Point(116.404, 39.915);  // 创建点坐标  
		map.centerAndZoom(point, 15);                 // 初始化地图，设置中心点坐标和地图级别
		map.enableScrollWheelZoom();
		map.addControl(new BMap.NavigationControl()); 
			});
	});


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
		var mkrTool = new BMapLib.MarkerTool(map, {autoClose: true, followText: "选择地图位置"});
		mkrTool.addEventListener("markend", function(evt){ 
			var mkr = evt.marker;

		});
		mkrTool.open(); //打开工具 
      	var icon = new BMap.Icon("http://api.map.baidu.com/library/MarkerTool/1.2/examples/images/transparent.gif", new BMap.Size(12, 21)); 
		icon.setImageOffset(new BMap.Size(0, 0));
		icon.setAnchor(new BMap.Size(0, 0));
    	mkrTool.setIcon(icon); 

		$('#visitLocationSelectModal').modal('show');
	};
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
