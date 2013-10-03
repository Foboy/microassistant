function ProductBaseCtrl($scope, $routeParams, $http, $animate, $location){
	console.log("pbc")
	console.log($animate)
	console.log($routeParams)
	var actCatalogId = 0;
	var actPageIndex = 1;
  //获取产品列表
  $scope.getCatProducts = function(catalogId, pageIndex){
	  actCatalogId = catalogId;
	  actPageIndex = pageIndex;
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
		  $location.path("/product/"+$scope.catalogs[0].PTypeId+"/1");
		  //$scope.catalogs[0].active = true;
		  //$scope.getCatProducts($scope.catalogs[0].PTypeId, pageIndex);
	  }
  };
  
  //获取产品分类列表
  $scope.getCatalogs = function(catalogId, pageIndex){
	  if(catalogId == actCatalogId && pageIndex == actPageIndex)
	  	return;
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
				  $location.path("/product/"+$scope.catalogs[0].PTypeId+"/1");
				  //$scope.activeCat($scope.catalogs[0].PTypeId, pageIndex);
			  }
		  }
		}).
		error(function(data, status, headers, config) {
		  $scope.catalogs = [];
		});
	  }
  };	
  
  $scope.addCatalog = function(){
	  if($scope.addCatalogFlag)
	  {
		  
	  }
	  else
	  {
		  $scope.addCatalogFlag = true;
	  }
  };
  
  $scope.addCatalogCancel = function(){
	  $scope.addCatalogFlag = false;
  };
  
  $scope.setRouteParams = function(params){
	  $scope.routeParams = params;
  };
  
  $scope.editProduct = function(){
	  $scope.productEditPageOne = true;
	  
	  $('#productEditModal').modal('show');
	  console.log($scope.ProductEditForm.$setDirty());
	  //$scope.ProductEditForm.$setPristine();
  };
  
  $scope.editProductPageNext = function(){
	  console.log($scope.ProductEditForm.PName);
	  if($scope.ProductEditForm.PName.$valid)
	  	$scope.productEditPageOne = false;
	  else
	  {
	  	$scope.ProductEditForm.PName.$dirty = true;
		$scope.ProductEditForm.PName.$pristine = false;
	  }
	  //$('#productEditModal').modal('show');
	  //console.log($scope.ProductEditForm.$setDirty());
	  //$scope.ProductEditForm.$setPristine();
  };
  
  $scope.addPurchase = function(){
	  $('#addPurchaseModal').modal('show');
  };
  
  $scope.productInfo = function(){
	  $scope.tabIndex=1;
	  $http.get($sitecore.urls["productDetail"],{params:{productId:$routeParams.productId}}).success(function(data) {
		console.log(data);
		$scope.product = data;
	  }).
	  error(function(data, status, headers, config) {
		$scope.product = {};
	  });
  };
  
  $scope.productStore = function(){
	  $scope.tabIndex=2;
	  $http.get($sitecore.urls["productDetail"],{params:{productId:$routeParams.productId}}).success(function(data) {
		console.log(data);
		$scope.product = data;
	  }).
	  error(function(data, status, headers, config) {
		$scope.product = {};
	  });
  };
  
  $scope.productPurchase = function(){
	  $scope.tabIndex=3;
	  $http.get($sitecore.urls["productDetail"],{params:{productId:$routeParams.productId}}).success(function(data) {
		console.log(data);
		$scope.product = data;
	  }).
	  error(function(data, status, headers, config) {
		$scope.product = {};
	  });
  };
  
  $scope.showProductDetail = function(){
	  $("#productDetailBox").animate({width:"600px"},500);
	  $scope.productInfo();
  };
  
  $scope.hideProductDetail = function(){
	  $scope.tabIndex=0;
	  $("#productDetailBox").animate({width:"0px"},500,function(){
		  console.log($location)
		  $scope.$apply(function() {
			  $location.path("/product/"+$routeParams.catalogId+"/"+$routeParams.pageIndex);
		  });
	  });
	  
  };
  
  $scope.ProductEditSubmit = function(){
	  alert(angular.toJson($scope.EditProduct));
	  alert($scope.ProductEditForm.$valid);
  };
}

function ProductListCtrl($scope, $routeParams, $http, $location){
		console.log($scope.$parent)
		console.log($routeParams)
		$scope.$parent.setRouteParams($routeParams);
		$scope.$parent.getCatalogs($routeParams.catalogId,$routeParams.pageIndex || 1);
}
