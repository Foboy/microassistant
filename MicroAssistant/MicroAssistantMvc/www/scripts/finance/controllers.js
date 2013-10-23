function FinanceMainCtrl($scope, $routeParams, $http, $location) {
    $scope.steps = $routeParams.steps;
    if (!$scope.steps)
        $scope.steps = "receivable";//Ӧ�տ��
    console.log($routeParams);
    $scope.loadCurrentStepList = function (pageSize) {
        switch ($scope.steps) {
            case 'receivable'://Ӧ�տ��
                $http.post($sitecore.urls["receivablesfinanceList"], { pageIndex: $routeParams.pageIndex || 1, pageSize: pageSize }).success(function (data) {
                    console.log(data);
                    $scope.ActPageIndex = $routeParams.pageIndex || 1;
                    $scope.receivables = data;
                }).error(function (data, status, headers, config) {
                    $scope.receivables = [];
                });
                break;
            case 'payable'://Ӧ�����
                $http.get($sitecore.urls["payablesfinanceList"], { params: { pageIndex: $routeParams.pageIndex || 1 } }).success(function (data) {
                    console.log(data);
                    $scope.ActPageIndex = $routeParams.pageIndex || 1;
                    $scope.payables = data;
                }).error(function (data, status, headers, config) {
                    $scope.payables = [];
                });
                break;
        }
    };
    $scope.loadCurrentStepList(10);
}
