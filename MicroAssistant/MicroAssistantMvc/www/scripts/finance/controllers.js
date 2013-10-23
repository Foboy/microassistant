function FinanceMainCtrl($scope, $routeParams, $http, $location) {
    $scope.steps = $routeParams.steps;
    if (!$scope.steps)
        $scope.steps = "receivable";//应收款步骤
    console.log($routeParams);
    $scope.loadCurrentStepList = function (pageSize) {
        switch ($scope.steps) {
            case 'receivable'://应收款步骤
                $http.post($sitecore.urls["receivablesfinanceList"], { pageIndex: $routeParams.pageIndex || 1, pageSize: pageSize }).success(function (data) {
                    console.log(data.Data);
                    $scope.ActPageIndex = $routeParams.pageIndex || 1;
                    $scope.receivables = data.Data;
                }).error(function (data, status, headers, config) {
                    $scope.receivables = [];
                });
                break;
            case 'payable'://应付款步骤
                $http.post($sitecore.urls["payablesfinanceList"], { pageIndex: $routeParams.pageIndex || 1, pageSize: pageSize }).success(function (data) {
                    console.log(data.Data);
                    $scope.ActPageIndex = $routeParams.pageIndex || 1;
                    $scope.payables = data.Data;
                }).error(function (data, status, headers, config) {
                    $scope.payables = [];
                });
                break;
        }
    };
  
    $scope.ShowReceivableDetail = function () {
        $scope.$broadcast('EventShowReceivableDetail', this.receivableitem);//应收款详情
    };
    $scope.loadCurrentStepList(10);
}
function FinaceDetailCtrl($scope, $routeParams, $http, $location) {
    console.log("deatil")
    console.log($scope)
    $scope.$on('EventShowReceivableDetail', function (event, product) {
        console.log("EventShowReceivableDetail");
        console.log(product);
        $("#productDetailBox").animate({ width: "600px" }, 500);
        $scope.productInfo();
    });
};
