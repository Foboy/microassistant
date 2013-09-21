function ProductBaseCtrl($scope, $routeParams, $http, $animate){
	console.log("pbc")
	console.log($animate)
  //获取产品列表
  $scope.getCatProducts = function(catalogId, pageIndex){
	$http.get($sitecore.urls["productList"],{params:{catalogid:catalogId}}).success(function(data) {
		console.log(data);
	  $scope.products = data;
	}).
	error(function(data, status, headers, config) {
	  $scope.products = [];
	});
  };
  
  $scope.activeCat = function(catalogId, pageIndex){
	  console.log($scope);
	  var actived = false;
	  for(var i=0;i<$scope.catalogs.length;i++)
	  {
		if($scope.catalogs[i].PTypeId == catalogId)
		{
			actived = true;
			$scope.catalogs[i].active = true;
			$scope.getCatProducts($scope.catalogs[i].PTypeId, pageIndex);
		}
		else
		{
			$scope.catalogs[i].active = false;
		}
	  }
	  if(!actived)
	  {
		  $scope.catalogs[0].active = true;
		  $scope.getCatProducts($scope.catalogs[0].PTypeId, pageIndex);
	  }
  };
  
  //获取产品分类列表
  $scope.getCatalogs = function(catalogId, pageIndex){
	  if($scope.catalogs)
	  {
		  if(catalogId)
		  {
			$scope.activeCat(catalogId, pageIndex);
		  }
	  }
	  else
	  {
		$http.get($sitecore.urls["productCat"]).success(function(data) {
		  $scope.catalogs = data;
		  console.log($scope.catalogs)
		  if($scope.catalogs && $scope.catalogs.length)
		  {
			  if(catalogId)
			  {
				  $scope.activeCat(catalogId, pageIndex);
			  }
			  else
			  {
				  $scope.activeCat($scope.catalogs[0].PTypeId, pageIndex);
			  }
		  }
		}).
		error(function(data, status, headers, config) {
		  $scope.catalogs = [];
		});
	  }
  };	
  $scope.productDetail = function(){
	  console.log(this)
	$http.get($sitecore.urls["productDetail"],{params:{productId:this.product.PName}}).success(function(data) {
	  console.log(data);
	  $scope.actProduct = data;
	  $("#productDetail").animate({width:"500px"},500);
	}).
	error(function(data, status, headers, config) {
	  $scope.actProduct = {};
	});
  };
  $scope.hideProductDetail = function(){
	  $("#productDetail").animate({width:"0px"},500);
	  //$('#myModal').modal();
  };
  $scope.closeProductDetail = function(){
	  $("#productDetail").animate({width:"0px"},0);
	  //$('#myModal').modal();
  };
}

function ProductListCtrl($scope, $routeParams, $http){
		console.log($scope.$parent)
		console.log($routeParams)
		$scope.$parent.getCatalogs($routeParams.catalogId,$routeParams.pageIndex);
}
