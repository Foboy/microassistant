function ClientMainCtrl($scope, $routeParams, $http, $location) {
    $scope.sorts = $routeParams.steps;
    if (!$scope.steps)
        $scope.steps = "enterprise";//企业
    console.log($routeParams);
    $scope.loadCurrentSortList = function () {
        switch ($scope.steps) {
            case 'enterprise'://获取企业用户
                $http.post($sitecore.urls["SearchCustomerEntByOwnerId"], {}).success(function (data) {
                    console.log(data.Data);
                    $scope.enterpriseclients = data.Data;
                }).error(function (data, status, headers, config) {
                    $scope.enterpriseclients = [];
                });
                break;
            case 'personal'://获取个人用户
                $http.post($sitecore.urls["SearchCustomerPrivByOwnerId"], {}).success(function (data) {
                    console.log(data.Data);
                    $scope.personalclients = data.Data;
                }).error(function (data, status, headers, config) {
                    $scope.personalclients = [];
                });
                break;
        }
    };
    $scope.loadCurrentSortList();
}