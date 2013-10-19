function HomeMainCtrl($scope, $routeParams, $http, $location){
	$scope.steps = $routeParams.steps;
	if(!$scope.steps)
		$scope.steps = "chance";
	console.log( $routeParams)
}
