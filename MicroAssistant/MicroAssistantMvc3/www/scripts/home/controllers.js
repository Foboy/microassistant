function HomeMainCtrl($scope, $routeParams, $http, $location){
	$scope.steps = $routeParams.steps;
	if(!$scope.steps)
		$scope.steps = "chance";
	console.log($routeParams)
	$scope.EntContactors = [];

    //获取企业通讯录
	$scope.SearchEntContactors = function () {
	    $http.post($sitecore.urls["SearchEntContactors"], {}).success(function (data) {
	        if (!data.Error) {
	            $scope.EntContactors = data.Data || [];
	        } else {
	            alert(data.ErrorMessage, 'e');
	        }
	    }).error(function (data, status, headers, config) {
	        alert(data.ErrorMessage, 'e');
	        $scope.EntContactors = [];
	    });
	}
	$scope.SearchEntContactors();
}
