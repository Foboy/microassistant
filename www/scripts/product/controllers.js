function ProductListCtrl($scope, $http){
  $http.get('scripts/json/productCat.json').success(function(data) {
	  console.log(data);
    $scope.catalogs = data;
  });
  $http.get('scripts/json/productList.json').success(function(data) {
	  console.log(data);
    $scope.products = data;
  });
}

function ProductDetailCtrl(){}