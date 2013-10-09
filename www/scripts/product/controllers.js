function ProductMainCtrl($scope, $routeParams, $http, $location){
	console.log("pmc");
	console.log($scope);
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
				  //$location.path("/product/"+$scope.catalogs[0].PTypeId+"/1");
				  $scope.activeCat($scope.catalogs[0].PTypeId, pageIndex);
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
  
  
  $scope.editProduct = function(){
	  $scope.$broadcast('EventEditPoduct',this.product);
  };
  
  $scope.addPurchase = function(){
	  $scope.$broadcast('EventAddPurchase',this.product);
  };
  
  $scope.showProductDetail = function(){
	  $scope.$broadcast('EventShowPoductDetail',this.product);
  };
  
  $scope.getCatalogs($routeParams.catalogId,$routeParams.pageIndex || 1);
  $('#file_upload').cuploadify({
				'formData'     : {
					'token'     : 'sss'
				},
				'swf'      : 'js/uploadify/uploadify.swf',
				'uploader' : '/Upload/Uploader/71358f72c447e0ec2ecba71636907898?queryData=width-126,height-126&imageWidth=470&imageHeight=0',
				'height'   : 70,
				'width'    : 190
			});
}

//产品详细
function ProductDetailCtrl($scope, $routeParams, $http, $location){
	console.log("deatil")
	console.log($scope)
	$scope.$on('EventShowPoductDetail',function(event,product){
		console.log("EventShowPoductDetail");
		console.log(product);
		$("#productDetailBox").animate({width:"600px"},500);
	    $scope.productInfo();
	});
	
	$scope.hideProductDetail = function(){
	  $scope.tabIndex=0;
	  $("#productDetailBox").animate({width:"0px"},500,function(){});
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
  
}

//编辑产品
function ProductEditCtrl($scope, $routeParams, $http, $location){
	console.log($scope)
	$scope.PTypes = [{name:"家具",id:0},{name:"家电",id:1}];
	
	$scope.$on('EventEditPoduct',function(event,product){
		console.log("EventEditPoduct");
		console.log(product);
	    $scope.productEditPageOne = true;
	  	console.log($scope)
	    $('#productEditModal').modal('show');
	});
	
  $scope.editProductPageNext = function(){
	  console.log($scope.ProductEditForm);
	  if($scope.ProductEditForm.PName.$valid)
	  {
	  	$scope.productEditPageOne = false;
		$scope.showerror = false;
	  }
	  else
	  {
		$scope.showerror = true;
	  }
	  //$('#productEditModal').modal('show');
	  //console.log($scope.ProductEditForm.$setDirty());
	  //$scope.ProductEditForm.$setPristine();
  };
  
    $scope.ProductEditSubmit = function(){
	  console.log(angular.toJson($scope.EditProduct));
	  if($scope.ProductEditForm.$valid)
	  {
		  $scope.showerror = false;
		  $http.post($sitecore.urls["productEdit"],{product:angular.toJson($scope.EditProduct)}).success(function(data) {
			console.log(data);
			$scope.product = data;
		  }).
		  error(function(data, status, headers, config) {
			$scope.product = {};
		  });
	  }
	  else
	  {
		  $scope.showerror = true;
	  }
  };
}

//添加采购单
function ProductPurchaseCtrl($scope, $routeParams, $http, $location){
	console.log($scope)
	$scope.$on('EventAddPurchase',function(event,product){
		console.log("EventAddPurchase");
		console.log(product);
		$('#addPurchaseModal').modal('show');
	});
}