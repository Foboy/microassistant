function FinanceMainCtrl($scope, $routeParams, $http, $location, financemodel) {
    var $parent = $scope.$parent;
    $scope.steps = $routeParams.steps;
    if (!$scope.steps) {
        $scope.steps = "receivable";//Ӧ�տ��
        $parent.receivableActPageIndex = 1;
    } else {
        $parent.payableActPageIndex = 1;
    }
    $scope.hasMoreRecords = false;

    $scope.refreshList = function () {
        $scope.loadCurrentStepList(-1);
    }

    $scope.showMoreRecords = function () {
        $scope.loadCurrentStepList(1);
    }
    $scope.loadCurrentStepList = function (pageIndex) {
        financemodel.getlist($scope.steps, pageIndex, 2, function (data) {
            if (data.Error) {
                alert(data.ErrorMessage);
            }
            $scope.hasMoreRecords = data.hasMore;
            switch ($scope.steps) {
                case 'receivable':
                    $scope.receivables = data.Items || [];
                    break;
                case 'payable':
                    $scope.payables = data.Items || [];
                    break;
            }
        });
        /*
        if (pageIndex == 0) pageIndex = 1;
        switch ($scope.steps) {
            case 'receivable'://Ӧ�տ��
                $http.post($sitecore.urls["receivablesfinanceList"], { pageIndex: pageIndex - 1, pageSize: 10 }).success(function (data) {
                    if (data.Error) {
                        alert(data.ErrorMessage, 'e');
                    } else {
                        $scope.receivables = data.Data.Items;
                        $parent.receivableActPageIndex = pageIndex;
                        //$parent.pages = utilities.paging(data.Data.RecordsCount, pageIndex, 10, '#finance/' + $scope.steps + '/{0}');
                    }
                }).error(function (data, status, headers, config) {
                    $scope.receivables = [];
                })//.lock({ selector: '#receivableList' });
                break;
            case 'payable'://Ӧ�����
                $http.post($sitecore.urls["payablesfinanceList"], { pageIndex: pageIndex - 1, pageSize: 10 }).success(function (data) {
                    if (data.Error) {
                        alert(data.ErrorMessage, 'e');
                    } else {
                        $scope.payables = data.Data.Items;
                        $parent.payableActPageIndex = pageIndex;
                        //$parent.pages = utilities.paging(data.Data.RecordsCount, pageIndex, 10, '#finance/' + $scope.steps + '/{0}');
                    }
                    
                }).error(function (data, status, headers, config) {
                    $scope.payables = [];
                })//.lock({ selector: '#payablesList' });
                break;
        }*/
    };

    $scope.ShowReceivableDetail = function (dataItem) {
        $scope.$broadcast('EventShowReceivableDetail', dataItem);//Ӧ�տ�����
    };
    $scope.MakeSurePayable = function () {
        $scope.$broadcast('EventMakeSurePayable', this.payableitem);//ȷ�ϸ���
    };
    

    $scope.$on('onLoginSuccess', function (event) {
        $scope.loadCurrentStepList(0);
    });

    $scope.changeStep = function (step) {
        $scope.steps = step;
        $scope.loadCurrentStepList(0);
    }
}
function FinaceDetailCtrl($scope, $routeParams, $http, $location) {
    $scope.$on('EventShowReceivableDetail', function (event, item) {
        $scope.tabIndex = 1;
        $("#receivablesDetailBox").show();
        $scope.receivableDetail(item);
        $("#receivablesDetailBox").animate({ width: "865px" }, 400);
    });
    //�����տ�����
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
        $http.post($sitecore.urls["makeSurePay"], { PCode: $scope.MakeItem.PCode, PId: $scope.MakeItem.PId, Num: $scope.MakeItem.PNum }).success(function (data) {
            if (!data.Error) {
                $("#makesurePayBox").modal('hide');
                $scope.loadCurrentStepList($routeParams.pageIndex || 1);
            } else {
                alert("ȷ��ʧ�ܣ�");
            }
        }).error(function (data, status, headers, config) {
            $("#makesurePayBox").modal('hide');
        })
    };
    $scope.cancelPay = function () {
        $("#makesurePayBox").modal('hide');
    };
    $scope.makeSureTimesReceivable = function (item)//ȷ�Ϸ����տ�
    {
        $http.post($sitecore.urls["makeSureTimesReceivable"], {
            contractNo: item.ContractNo,
            rNum: item.InstalmentsNo
        }).success(function (data) {
            if (!data.Error) {
                $scope.hideReceivableDetail();
                $scope.loadCurrentStepList($routeParams.pageIndex || 1);
            }
        }).error(function (data, status, headers, config) {
            $scope.hideReceivableDetail();
        });
    };
};
