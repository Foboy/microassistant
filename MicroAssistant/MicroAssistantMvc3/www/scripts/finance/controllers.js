function FinanceMainCtrl($scope, $routeParams, $http, $location) {
    var $parent = $scope.$parent;
    $scope.steps = $routeParams.steps;
    if (!$scope.steps) {
        $scope.steps = "receivable";//应收款步骤
        $parent.receivableActPageIndex = 1;
    } else {
        $parent.payableActPageIndex = 1;
    }
    $scope.loadCurrentStepList = function (pageIndex) {
        if (pageIndex == 0) pageIndex = 1;
        switch ($scope.steps) {
            case 'receivable'://应收款步骤
                $http.post($sitecore.urls["receivablesfinanceList"], { pageIndex: pageIndex - 1, pageSize: 10 }).success(function (data) {
                    if (data.Error) {
                        alert(data.ErrorMessage, 'e');
                    } else {
                        $scope.receivables = data.Data.Items;
                        $parent.receivableActPageIndex = pageIndex;
                        $parent.pages = utilities.paging(data.Data.RecordsCount, pageIndex, 10, '#!finance/' + $scope.steps + '/{0}');
                    }
                }).error(function (data, status, headers, config) {
                    $scope.receivables = [];
                }).lock({ selector: '#receivableList' });
                break;
            case 'payable'://应付款步骤
                $http.post($sitecore.urls["payablesfinanceList"], { pageIndex: pageIndex - 1, pageSize: 10 }).success(function (data) {
                    if (data.Error) {
                        alert(data.ErrorMessage, 'e');
                    } else {
                        $scope.payables = data.Data.Items;
                        $parent.payableActPageIndex = pageIndex;
                        $parent.pages = utilities.paging(data.Data.RecordsCount, pageIndex, 10, '#!finance/' + $scope.steps + '/{0}');
                    }
                    
                }).error(function (data, status, headers, config) {
                    $scope.payables = [];
                }).lock({ selector: '#payablesList' });
                break;
        }
    };

    $scope.ShowReceivableDetail = function (dataItem) {
        $scope.$broadcast('EventShowReceivableDetail', dataItem);//应收款详情
    };
    $scope.MakeSurePayable = function () {
        $scope.$broadcast('EventMakeSurePayable', this.payableitem);//确认付款
    };
    $scope.loadCurrentStepList($routeParams.pageIndex || 1);
}
function FinaceDetailCtrl($scope, $routeParams, $http, $location) {
    $scope.$on('EventShowReceivableDetail', function (event, item) {
        $scope.tabIndex = 1;
        $("#receivablesDetailBox").show();
        $scope.receivableDetail(item);
        $("#receivablesDetailBox").animate({ width: "500px" }, 400);
    });
    //隐藏收款详情
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
        }).lock({ selector: '#receivablesDetailBox' });
    };
    $scope.$on('EventMakeSurePayable', function (event, item) {
        $scope.MakeItem = item;
        $("#makesurePayBox").modal('show');
    });
    $scope.makeSurePay = function () {
        $http.post($sitecore.urls["makeSurePay"], { PCode: $scope.MakeItem.PCode }).success(function (data) {
            if (!data.Error) {
                $("#makesurePayBox").modal('hide');
                $scope.loadCurrentStepList(10);
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
        $http.post($sitecore.urls["makeSureTimesReceivable"], {
            contractNo: item.ContractNo,
            rNum: item.InstalmentsNo
        }).success(function (data) {
            if (!data.Error) {
                $scope.hideReceivableDetail();
            }
            console.log(data.Data);
        }).error(function (data, status, headers, config) {
            $scope.hideReceivableDetail();
        });
    };
};
