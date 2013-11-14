function FinanceMainCtrl($scope, $routeParams, $http, $location) {
    $scope.steps = $routeParams.steps;
    if (!$scope.steps)
        $scope.steps = "receivable";//应收款步骤
    console.log($routeParams);
    $scope.loadCurrentStepList = function (pageSize) {
        switch ($scope.steps) {
            case 'receivable'://应收款步骤
                $http.post($sitecore.urls["receivablesfinanceList"], { pageIndex: $routeParams.pageIndex || 0, pageSize: pageSize }).success(function (data) {
                    console.log(data.Data);
                    //$scope.ActPageIndex = $routeParams.pageIndex || 0;
                    $scope.receivables = data.Data.Items;
                }).error(function (data, status, headers, config) {
                    $scope.receivables = [];
                });
                break;
            case 'payable'://应付款步骤
                $http.post($sitecore.urls["payablesfinanceList"], { pageIndex: $routeParams.pageIndex || 0, pageSize: pageSize }).success(function (data) {
                    console.log(data.Data);
                    $scope.ActPageIndex = $routeParams.pageIndex || 0;
                    $scope.payables = data.Data.Items;
                    
                }).error(function (data, status, headers, config) {
                    $scope.payables = [];
                });
                break;
        }
    };

    $scope.ShowReceivableDetail = function (dataItem) {
        $scope.$broadcast('EventShowReceivableDetail', dataItem);//应收款详情
    };
    $scope.MakeSurePayable = function () {
        $scope.$broadcast('EventMakeSurePayable', this.payableitem);//确认付款
    };
    $scope.loadCurrentStepList(10);
}
function FinaceDetailCtrl($scope, $routeParams, $http, $location) {
    $scope.$on('EventShowReceivableDetail', function (event, item) {
        $scope.tabIndex = 1;
        $("#receivablesDetailBox").show();
        $scope.receivableDetail(item);
        $("#receivablesDetailBox").animate({ width: "500px" }, 400);
    });
    $scope.hideReceivableDetail = function () {
        $("#receivablesDetailBox").animate({ width: "0px" }, 400, function () { $("#receivablesDetailBox").hide(); });

    };
    $scope.receivableDetail = function (item) {
        $http.post($sitecore.urls["receivablesDetail"], {
            contractNo: item.ContractNo
        }).success(function (data) {
            if (!data.Error) {
                $scope.receivableDetailInfos = data.Data;
                if (data.Data != null) {
                    $scope.howpaylists = data.Data.HowtopayList;
                }
            } else {
                $scope.receivableDetailInfos = [];
            }
        }).error(function (data, status, headers, config) {
            $scope.receivableDetailInfos = [];
        });
    };
    $scope.IsReceviableItem = function (item) {

    }
    $scope.$on('EventMakeSurePayable', function (event, item) {
        $scope.MakeItem = item;
        $("#makesurePayBox").modal('show');
    });
    $scope.makeSurePay = function () {
        $http.post($sitecore.urls["makeSurePay"], { PCode: $scope.MakeItem.PCode }).success(function (data) {
            if (!data.Error) {
                $("#makesurePayBox").modal('hide');
            } else {
                alert("确认失败！");
            }
        }).error(function (data, status, headers, config) {
            $("#makesurePayBox").modal('hide');
        })
    };
    $scope.cancelPay = function () {
        $("#makesurePayBox").modal('hide');
    };
    $scope.makeSureTimesReceivable = function (item)//确认分期收款
    {
        console.log(item);
        $http.post($sitecore.urls["makeSureTimesReceivable"], {
            contractNo: itme.contractNo,
            rNum:item.rNum
        }).success(function (data) {
            console.log(data.Data);
        }).error(function (data, status, headers, config) {
          
        });
    };
};
