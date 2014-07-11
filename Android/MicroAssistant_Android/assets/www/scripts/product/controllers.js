function ProductMainCtrl($scope, $routeParams, $http, $location, productmodel, $environment) {

    var productPageSize = 10;
    $scope.navStyle = {};
    $scope.showActionsProduct = 0;
    $scope.currentCatalogId = 0;
    $scope.currentPageIndex = 0;
    $scope.hasMoreProducts = false;

    $scope.refreshList = function () {
        $scope.products = [];
        $scope.getCatProducts($scope.currentCatalogId, -1);
    }

    $scope.showAction = function (pid) {
        $scope.showActionsProduct = pid;
    }

    $scope.showMoreProducts = function () {
        $scope.getCatProducts($scope.currentCatalogId, 1);
    }

    //获取产品列表
    $scope.getCatProducts = function (catalogId, pageIndex) {
        console.log(catalogId + '_' + pageIndex);
        $scope.currentCatalogId = catalogId;
        productmodel.getproducts(catalogId, pageIndex, 2, function (data) {
            if (data.Error) {
                alert(data.ErrorMessage);
            }
            $scope.hasMoreProducts = data.hasMore;

            $scope.products = data.Items || [];

            /*
            if (data.Data && data.Data.Items.length == productPageSize) {
                $scope.hasMoreProducts = true;
            }
            else {
                $scope.hasMoreProducts = false;
            }
            if (pageIndex <= 0) {
                $scope.products = data.Data.Items || [];
            }
            else {
                $scope.products = $scope.products || [];
                if (angular.isArray(data.Data.Items)) {
                    angular.forEach(data.Data.Items, function (value) {
                        $scope.products.push(value);
                    });
                }
            }
            $scope.currentPageIndex = pageIndex >= 0 ? pageIndex : 0;
            */
        });
    };

    $scope.activeCat = function (catalogId) {
        $scope.currentCatalogId = catalogId;
        $scope.products = [];
        $scope.getCatProducts(catalogId, 0);
    };

    //获取产品分类列表
    $scope.showCatalogs = function () {
        productmodel.getcatalogs(0, 50, function (data) {
            $scope.catalogs = data.Items || [];
            if ($scope.catalogs && $scope.catalogs.length) {
                $scope.activeCat($scope.catalogs[0].PTypeId);
            }
            var navWidth = $scope.catalogs.length * 100;
            if (navWidth > $environment.screen.width) {
                $scope.navStyle = { width: navWidth + 'px' };
            }
            else {
                $scope.navStyle = {};
            }
        });
    };

    $scope.addCatalog = function () {
        $('#productCatalogAddModal').modal('show');
        $scope.ProductCatalogForm.$setPristine();
    };

    $scope.addCatalogSubmit = function () {
        if ($scope.ProductCatalogForm.$valid) {
            $scope.showerror = false;
            $http.post($sitecore.urls["productAddCat"], { fatherid: 0, pTypeName: $scope.AddedCatalog.PTypeName, ptypepicid: $scope.AddedCatalog.PicId || 0 }).success(function (data) {
                console.log(data);
                if (data.Error) {
                    alert(data.ErrorMessage);
                }
                else {
                    $scope.catalogs = $scope.catalogs || [];
                    var catalog = angular.copy($scope.AddedCatalog);
                    catalog.PTypeId = data.Id;
                    catalog.PicId = $scope.AddedCatalog.PicId;
                    $scope.catalogs.push(catalog);
                    $location.path("/product/" + data.Id + "/1");
                    //$scope.catalogs = $scope.catalogs || [];
                    //var catalog = angular.copy($scope.AddedCatalog)
                    //catalog.PTypeId = data.Id;
                    //catalog.PicId = $scope.AddedCatalog.PicId;
                    //$scope.catalogs.push(catalog);
                    $('#productCatalogAddModal').modal('hide');
                }
            }).
            error(function (data, status, headers, config) {
                $scope.product = {};
            });
        }
        else {
            $scope.showerror = true;
        }
    };


    $scope.editProduct = function () {
        $scope.$broadcast('EventEditPoduct', this.product);
    };

    $scope.addPurchase = function () {
        console.log("EventAddPurchase");
        console.log(this);
        $scope.$broadcast('EventAddPurchase', this);
    };

    $scope.showProductDetail = function () {
        $scope.$broadcast('EventShowPoductDetail', this.product);
    };

    
    $scope.$on('onLoginSuccess', function (event) {
        $scope.showCatalogs($routeParams.catalogId, 0);
    });
    $scope.showCatalogs($routeParams.catalogId, 0);
    //bootstro.start('.bootstro', {
    //    url : 'partials/product/help.json',
    //    nextButtonText : '继续 &raquo;',
    //    prevButtonText : '&laquo; 返回',
    //    finishButtonText : '<i class="icon-ok"></i> 跳过帮助',
    //}); 
}

//产品详细
function ProductDetailCtrl($scope, $routeParams, $http, $location) {
    console.log("deatil")
    console.log($scope)
    var product;

    $scope.$on('EventShowPoductDetail', function (event, showproduct) {
        console.log("EventShowPoductDetail");
        console.log(showproduct);
        product = showproduct;
        $("#productDetailBox").animate({ width: "600px" }, 500);
        $scope.tabIndex = 1;
        if (!$scope.product || product.PId != $scope.product.PId) {
            $http.post($sitecore.urls["productDetail"], { pid: product.PId }).success(function (data) {
                console.log(data);
                $scope.product = data.Data;
            }).
            error(function (data, status, headers, config) {
                $scope.product = {};
            });
        }
    });

    $scope.hideProductDetail = function () {
        $scope.tabIndex = 0;
        $("#productDetailBox").animate({ width: "0px" }, 500, function () { });
    };

    $scope.productInfo = function () {
        $scope.tabIndex = 1;
    };
    //库存信息
    $scope.productStore = function () {
        $scope.tabIndex = 2;
        if (!$scope.stores || !$scope.stores.length || product.PId != $scope.stores[0].PId) {
            $http.post($sitecore.urls["productStoresList"], { pid: product.PId, pageIndex: 0, pageSize: 10, type: 1 }).success(function (data) {
                $scope.stores = data.Data.Items;
            }).
            error(function (data, status, headers, config) {
                $scope.stores = {};
            });
        }
    };
    //采购单信息
    $scope.productPurchase = function () {
        $scope.tabIndex = 3;
        if (!$scope.purchases || !$scope.purchases.length || product.PId != $scope.purchases[0].PId) {
            $http.post($sitecore.urls["productStoresList"], { pid: product.PId, pageIndex: 0, pageSize: 10, type: 0 }).success(function (data) {
               $scope.purchases = data.Data.Items;
            }).
            error(function (data, status, headers, config) {
                $scope.purchases = {};
            });
        }
    };

}

//编辑产品
function ProductEditCtrl($scope, $routeParams, $http, $location) {
    console.log($scope)
    $scope.$on('EventEditPoduct', function (event, product) {
        console.log("EventEditPoduct");
        console.log(product);
        $scope.productEditPage = 1;
        $scope.PTypes = angular.copy($scope.$parent.$parent.catalogs);
        console.log($scope.PTypes)
        if (product) {
            $scope.EditProduct = angular.copy(product);
            if (angular.isArray($scope.PTypes)) {
                angular.forEach($scope.PTypes, function (value) {
                    if (value.PTypeId == product.PTypeId)
                        $scope.EditProduct.PType = value;
                });
            }
        }
        else {
            $scope.ProductEditFormOne.$setPristine();
            $scope.ProductEditFormTwo.$setPristine();
            $scope.EditProduct = { Unit: '个' };
            if (angular.isArray($scope.PTypes)) {
                angular.forEach($scope.PTypes, function (value) {
                    if (value.PTypeId == $scope.ActCatalogId)
                        $scope.EditProduct.PType = value;
                });
            }
        }
        $('#productEditModal').modal('show');
    });

    $scope.ProductEditSubmit = function () {
        if (!$scope.ProductEditFormOne.$valid) {
            $scope.showerror1 = true;
            $scope.productEditPage = 1;
            return;
        }
        else if (!$scope.ProductEditFormTwo.$valid) {
            $scope.showerror2 = true;
            $scope.productEditPage = 2;
            return;
        }
        else {
            $scope.showerror1 = false;
            $scope.showerror2 = false;
            $scope.EditProduct.PTypeId = $scope.EditProduct.PType.PTypeId;
            $scope.EditProduct.PTypeName = $scope.EditProduct.PType.PTypeName;
            $scope.showerror = false;
            $http.post($scope.EditProduct.PId ? $sitecore.urls["productUpdate"] : $sitecore.urls["productAdd"], { pid: $scope.EditProduct.PId, pname: $scope.EditProduct.PName, ptypeid: $scope.EditProduct.PType.PTypeId, unit: $scope.EditProduct.Unit, pinfo: $scope.EditProduct.PInfo, LowestPrice: $scope.EditProduct.LowestPrice, MarketPrice: $scope.EditProduct.MarketPrice }).success(function (data) {
                console.log(data);
                if (data.Error) {
                    alert(data.ErrorMessage);
                }
                else {
                    if ($scope.ActCatalogId == $scope.EditProduct.PType.PTypeId) {
                        if ($scope.EditProduct.PId) {
                            angular.forEach($scope.products, function (value) {
                                if (value.PId == $scope.EditProduct.PId)
                                    angular.extend(value, $scope.EditProduct);
                            });
                        }
                        else {
                            var currentproduct = angular.copy($scope.EditProduct);
                            currentproduct.StockCount = 0;
                            currentproduct.PId = data.Id;
                            $scope.products.push(currentproduct);
                        }
                    }
                    $('#productEditModal').modal('hide');
                }
                $scope.product = data;
            }).
            error(function (data, status, headers, config) {
                $scope.product = {};
            }).lock({ selector: '#productEditModal' });
        }
    };
}

//添加采购单
function ProductPurchaseCtrl($scope, $routeParams, $http, $location) {
    console.log($scope)
    var from;
    $scope.$on('EventAddPurchase', function (event, fromscope) {
        console.log("EventAddPurchase");
        console.log(fromscope);
        from = fromscope;
        $('#addPurchaseModal').modal('show');
    });

    $scope.ProductAddPurchaseSubmit = function () {
        if ($scope.ProductAddPurchaseForm.$valid) {
            $scope.showerror = false;
            $http.post($sitecore.urls["productAddStores"], { pid: from.product.PId, num: $scope.AddedPurchase.PNum, price: $scope.AddedPurchase.Price }).success(function (data) {
                console.log(data);
                if (data.Error) {
                    alert(data.ErrorMessage);
                }
                else {
                    if (from && angular.isArray(from.purchases)) {
                        $scope.AddedPurchase.PId = data.Id;
                        from.purchases.push(angular.copy($scope.AddedPurchase));
                    }
                    if (from && from.product) {
                        from.product.StockCount = (from.product.StockCount || 0);
                    }
                    $('#addPurchaseModal').modal('hide');
                }
            }).
            error(function (data, status, headers, config) {
                //$scope.product = {};
            }).lock({ selector: '#addPurchaseModal' });
        }
        else {
            $scope.showerror = true;
        }
    };
}