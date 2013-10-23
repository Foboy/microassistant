function ProductMainCtrl($scope, $routeParams, $http, $location){
	console.log("pmc");
	console.log($scope);
	$scope.ActCatalogId = 0;
	$scope.ActPageIndex = 1;
	
  //获取产品列表
  $scope.getCatProducts = function(catalogId, pageIndex){
      $http.get($sitecore.urls["productList"], { params: { typeid: catalogId, pageIndex: pageIndex-1,pageSize:20 } }).success(function (data) {
          console.log(data);
          if (data.Error) {
              alert(data.ErrorMessage);
          }
		  $scope.ActPageIndex = pageIndex;
		  $scope.products = data.Data.Items;
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
			$scope.ActCatalogId = $scope.catalogs[i].PTypeId;
			$scope.getCatProducts($scope.catalogs[i].PTypeId, pageIndex);
			break;
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
  $scope.showCatalogs = function(catalogId, pageIndex){
	  if(catalogId == $scope.ActCatalogId && pageIndex == actPageIndex)
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
	      $http.post($sitecore.urls["productCat"], {pageIndex:0,pageSize:50}).success(function (data) {
	          $scope.catalogs = data.Data || [];
	          if (data.Error) {
	              alert(data.ErrorMessage);
	          }
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
		  if($scope.ProductCatalogForm.$valid)
		  {
			  $scope.showerror = false;
		      $http.post($sitecore.urls["productAddCat"], { fatherid: 0, pTypeName: $scope.AddedCatalog.PTypeName, ptypepicid: $scope.AddedCatalog.PicId || 0 }).success(function (data) {
		          console.log(data);
		          if (data.Error) {
		              alert(data.ErrorMessage);
		          }
		          else {
		              $scope.catalogs = $scope.catalogs || [];
		              data.PTypeName = $scope.AddedCatalog.PTypeName;
		              data.PicId = $scope.AddedCatalog.PicId;
		              $scope.catalogs.push(data);
		              $scope.addCatalogFlag = false;
		          }
			  }).
			  error(function(data, status, headers, config) {
				$scope.product = {};
			  });
		  }
		  else
		  {
			  $scope.showerror = true;
		  }
	  }
	  else
	  {
		  $scope.addCatalogFlag = true;
		  $scope.showerror = false;
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
  
  $scope.showCatalogs($routeParams.catalogId,$routeParams.pageIndex || 1);
  $('#file_upload').cuploadify({
				'formData'     : {
					'token'     : 'sss'
				},
				'swf'      : 'js/uploadify/uploadify.swf',
				'uploader' : '/Upload/Uploader/71358f72c447e0ec2ecba71636907898?queryData=width-126,height-126&imageWidth=470&imageHeight=0',
				'height'   : 70,
				'width'    : 190,
				onUploadComplete : function(response){
					
				}
			});

  //bootstro.start('.bootstro', {
  //    url : 'partials/product/help.json',
  //    nextButtonText : '继续 &raquo;',
  //    prevButtonText : '&laquo; 返回',
  //    finishButtonText : '<i class="icon-ok"></i> 跳过帮助',
  //}); 
}

//产品详细
function ProductDetailCtrl($scope, $routeParams, $http, $location){
	console.log("deatil")
	console.log($scope)
	$scope.$on('EventShowPoductDetail',function(event,product){
		console.log("EventShowPoductDetail");
		console.log(product);
		$("#productDetailBox").animate({width:"600px"},500);
		$scope.productInfo(product);
	});
	
	$scope.hideProductDetail = function(){
	  $scope.tabIndex=0;
	  $("#productDetailBox").animate({width:"0px"},500,function(){});
  	};
	
	$scope.productInfo = function (product) {
	  $scope.tabIndex=1;
	    $http.post($sitecore.urls["productDetail"], { pid: product.PId }).success(function (data) {
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
function ProductEditCtrl($scope, $routeParams, $http, $location) {
    console.log($scope)

	$scope.$on('EventEditPoduct',function(event,product){
		console.log("EventEditPoduct");
		console.log(product);
	    $scope.productEditPageOne = true;
	    console.log($scope)
	    $scope.PTypes = angular.copy($scope.$parent.$parent.catalogs);
	    console.log($scope.PTypes)
	    $('#productEditModal').modal('show');
	});
	
  $scope.editProductPageNext = function(){

      if ($scope.ProductEditForm.PName.$valid && $scope.ProductEditForm.PType.$valid && $scope.ProductEditForm.PInfo.$valid)
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
	      $http.post($sitecore.urls["productEdit"], { pname: $scope.EditProduct.PName, ptypeid: $scope.EditProduct.PType.PTypeId, unit: $scope.EditProduct.Unit, pinfo: $scope.EditProduct.PInfo, LowestPrice: $scope.EditProduct.LowestPrice, MarketPrice: $scope.EditProduct.MarketPrice }).success(function (data) {
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
	
	$scope.ProductAddPurchaseSubmit = function(){
	  if($scope.ProductAddPurchaseForm.$valid)
	  {
		  $scope.showerror = false;
		  $http.post($sitecore.urls["productEdit"],{product:angular.toJson($scope.AddedPurchase)}).success(function(data) {
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