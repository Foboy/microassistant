function ProductListCtrl($scope, $http){
  $http.get($sitecore.urls["productCat"]).success(function(data) {
	  console.log(data);
    $scope.catalogs = data;
  });
  $http.get($sitecore.urls["productList"]).success(function(data) {
	  console.log(data);
    $scope.products = data;
  });
}

function ProductDetailCtrl(){}