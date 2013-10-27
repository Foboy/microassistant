function FinanceMainCtrl($scope, $routeParams, $http, $location) {
    $scope.steps = $routeParams.steps;
    if (!$scope.steps)
        $scope.steps = "receivable";//Ӧ�տ��
    console.log($routeParams);
    $scope.loadCurrentStepList = function (pageSize) {
        switch ($scope.steps) {
            case 'receivable'://Ӧ�տ��
                $http.post($sitecore.urls["receivablesfinanceList"], { pageIndex: $routeParams.pageIndex || 1, pageSize: pageSize }).success(function (data) {
                    console.log(data.Data);
                    $scope.ActPageIndex = $routeParams.pageIndex || 1;
                    $scope.receivables = data.Data;
                }).error(function (data, status, headers, config) {
                    $scope.receivables = [];
                });
                break;
            case 'payable'://Ӧ�����
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
        $scope.$broadcast('EventShowReceivableDetail', this.receivableitem);//Ӧ�տ�����
    };
    $scope.MakeSurePayable = function () {
        $scope.$broadcast('EventMakeSurePayable', this.payableitem);//ȷ�ϸ���
    };
    $scope.loadCurrentStepList(10);
}
function FinaceDetailCtrl($scope, $routeParams, $http, $location) {
    console.log($scope)
    $scope.$on('EventShowReceivableDetail', function (event, item) {
        console.log("EventShowReceivableDetail");
        console.log(item);
        $("#receivablesDetailBox").animate({ width: "600px" }, 500);
        $scope.receivableDetail(item);
    });
    $scope.receivableDetail = function (item) {
        console.log(item);
        $http.post($sitecore.urls["receivablesDetail"], {
            contractNo: itme.contractNo
        }).success(function (data) {
            console.log(data.Data);
            $scope.receivableDetailInfo = data.Data;
        }).error(function (data, status, headers, config) {
            $scope.receivableDetailInfo = [];
        });
    };
    $scope.$on('EventMakeSurePayable', function (event, item) {
        console.log(item);
        $("#makesurePayBox").modal('show');
        $scope.makeSurePay(item);
        $scope.cancelPay();
    });
    $scope.makeSurePay = function (item) {
        console.log(item);
        $http.post($scope.urls["makeSurePay"], { PCode: item.PCode }).success(function (data) {
            console.log(data.Data);
            $("#makesurePayBox").modal('hide');
        }).error(function (data, status, headers, config) {
            console.log(data.Data);
        })
    };
    $scope.cancelPay = function () {
        $("#makesurePayBox").modal('hide');
    };

    $scope.makeSureTimesReceivable = function (item)//ȷ�Ϸ����տ�
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
