
function SalesMainCtrl($scope, $routeParams, $http, $location){
	$scope.steps = $routeParams.steps;
	if(!$scope.steps)
		$scope.steps = "chance";
	console.log( $routeParams)
	//显示列表内容
	$scope.loadCurrentStepList = function(){
		switch($scope.steps)
		{
		    case 'chance':
		        if (!$scope.chances) {
		            $http.post($sitecore.urls["salesChanceList"], { pageIndex: $routeParams.pageIndex || 0, pageSize: 20 }).success(function (data) {
		                console.log(data);
		                if (data.Error) {
		                    alert(data.ErrorMessage);
		                }
		                $scope.ActPageIndex = $routeParams.pageIndex || 0;
		                $scope.chances = data.Data.Items;
		            }).
                    error(function (data, status, headers, config) {
                        $scope.chances = [];
                    });
		        }
				break;
			case 'visit':
				$http.get($sitecore.urls["productList"],{params:{pageIndex:$routeParams.pageIndex||0}}).success(function(data) {
					console.log(data);
				  $scope.ActPageIndex = $routeParams.pageIndex||0;
				  $scope.visits = data;
				}).
				error(function(data, status, headers, config) {
				  $scope.visits = [];
				});
				break;
			case 'contract':
				$http.get($sitecore.urls["productList"],{params:{pageIndex:$routeParams.pageIndex||0}}).success(function(data) {
					console.log(data);
				  $scope.ActPageIndex = $routeParams.pageIndex||0;
				  $scope.contracts = data;
				}).
				error(function(data, status, headers, config) {
				  $scope.contracts = [];
				});
				break;
			case 'after':
				$http.get($sitecore.urls["productList"],{params:{pageIndex:$routeParams.pageIndex||1}}).success(function(data) {
					console.log(data);
				  $scope.ActPageIndex = $routeParams.pageIndex||1;
				  $scope.afters = data;
				}).
				error(function(data, status, headers, config) {
				  $scope.afters = [];
				});
				break;
		}
	};
	$scope.addCurrentStepItem = function(){
		switch($scope.steps)
		{
			case 'chance':
			    $scope.$broadcast('EventAddChance', this);
				break;
			case 'visit':
			    $scope.$broadcast('EventAddVisit', this);
				break;
			case 'contract':
			    $scope.$broadcast('EventAddContract', this);
				break;
			case 'after':
			    $scope.$broadcast('EventAddAfter', this);
				break;
		}
	};
	$scope.showChanceDetail = function(){
	    $scope.$broadcast('EventChanceDetail', this);
	};
	$scope.showVisitDetail = function(){
	    $scope.$broadcast('EventVisitDetail', this);
	};
	$scope.showContractDetail = function(){
	    $scope.$broadcast('EventContractDetail', this);
	};
	$scope.showAfterDetail = function(){
	    $scope.$broadcast('EventAfterDetail', this);
	};

	$scope.loadCurrentStepList();
}

function SalesChanceDetailCtrl($scope, $routeParams, $http, $location){
	$scope.$on('EventChanceDetail',function(event){
		$("#chanceDetailBox").animate({width:"350px"},300);
		//加载机会数据
	});
	
	$scope.hideChanceDetail = function(){
	  $("#chanceDetailBox").animate({width:"0px"},300,function(){});
  	};
};
function SalesVisitDetailCtrl($scope, $routeParams, $http, $location) {
    var from;
    $scope.$on('EventVisitDetail', function (event, fromscope) {
        console.log(fromscope)
        from = fromscope;
        $scope.chance = from.chance;
        $scope.NewRate = $scope.chance.Rate;
        $scope.chanceVisitDetail();
        $scope.visitFormReset();
		$("#visitDetailBox").animate({width:"900px"},500);
		//加载机会数据
    });

    $scope.visitFormReset = function () {
        $scope.EditVisit = { VisitType: 1, Address: '' };
        $scope.SalesAddChanceVisitFrom.$setPristine();
    };
	
	$scope.hideVisitDetail = function(){
	  $("#visitDetailBox").animate({width:"0px"},500,function(){});
	};

	$scope.chanceVisitDetail = function () {
	    if (!$scope.chance_visits || from.chance.IdmarketingChance != $scope.IdmarketingChance) {
	        $http.post($sitecore.urls["salesChanceVisitsList"], { cid: from.chance.IdmarketingChance, pageIndex: 0, pageSize: 20 }).success(function (data) {
	            console.log(data);
	            if (data.Error) {
	                alert(data.ErrorMessage);
	            }
	            else {
	                $scope.chance_visits = data.Data.Vlist.Items;
	            }
	        }).
            error(function (data, status, headers, config) {
                $scope.chance_visits = {};
            });
	    }
	};
	
	$scope.selectVisitLocation = function(){
		$("#salesVisitMapIframe").attr({src:'partials/others/locationselectmap.html?lat='+$scope.visit_lat+'&lng='+$scope.visit_lng})
		$('#visitLocationSelectModal').modal('show');
	};

	$scope.markAddressOnMap = function () {
	    var address = $scope.parseAddress(this.visit.Address);
	    $("#salesVisitMapIframe").attr({ src: 'partials/others/locationselectmap.html?lookonly=true&lat=' + address.lat + '&lng=' + address.lng })
	    $('#visitLocationSelectModal').modal('show');
	};
	
	utilities.registeriframelistener("visitlocation",function(){
	    $('#visitLocationSelectModal').modal('hide');
	    var lat = arguments[0], lng = arguments[1], address = arguments[2];
	    $scope.$apply(function () {
	        $scope.visit_lat = lat;
	        $scope.visit_lng = lng;
	        $scope.EditVisit.Address = address;
	    });
	});

	$scope.parseAddress = function (address) {
	    address = address || '';
	    var result = { lat: 0, lng: 0, address: '' };
	    if (/\([\d\.\,\s]{3,}\)/.test(address)) {
	        var pointstr = address.match(/\([\d\.\,\s]{3,}\)/)[0];
	        result.address = address.replace(pointstr, '');
	        var point = pointstr.substring(1, pointstr.length-1).split(',');
	        result.lat = point[0];
	        result.lng = point[1];
	    }
	    else {
	        result.address = address;
	    }
	    console.log(result)
	    return result;
	};

	$scope.combineAdderess = function () {
	    if (!isNaN($scope.visit_lat) && !isNaN($scope.visit_lng)) {
	        return $scope.EditVisit.Address + '  ( ' + $scope.visit_lat + ', ' + $scope.visit_lng + ')';
	    }
	    else {
	        return $scope.EditVisit.Address;
	    }
	};

	$scope.addChanceVisit = function () {
	    if ($scope.SalesAddChanceVisitFrom.$valid) {
	        $scope.showerror = false;
	        $http.post($sitecore.urls["salesAddChanceVisits"], { cid: from.chance.IdmarketingChance, visitType: $scope.EditVisit.VisitType, remark: $scope.EditVisit.Remark, amount: 0, address: $scope.combineAdderess() }).success(function (data) {
	            console.log(data);
	            if (data.Error) {
	                alert(data.ErrorMessage);
	            }
	            else {
	                var addedvisit = angular.copy($scope.EditVisit);
	                addedvisit.Address = $scope.combineAdderess();
	                $scope.chance_visits.unshift(addedvisit);
	                $scope.addvisitpanleshow = false;
	                $scope.visitFormReset();
	            }
	        }).
            error(function (data, status, headers, config) {
                //$scope.product = {};
            });
	    }
	    else {
	        $scope.showerror = true;
	    }
	};

	$scope.changeRateSubmit = function () {
	    if ($scope.chance.Rate == $scope.NewRate)
	    {
	        $scope.rateEditPanleShow = false;
	        return;
	    }
	    if ($scope.SalesRateChangeFrom.$valid) {
	        $http.post($sitecore.urls["salesRateChange"], { cid: from.chance.IdmarketingChance, num: $scope.NewRate }).success(function (data) {
	            console.log(data);
	            if (data.Error) {
	                $scope.NewRate = $scope.chance.Rate;
	                alert(data.ErrorMessage);
	            }
	            else {
	                $scope.chance.Rate = $scope.NewRate;
	                $scope.rateEditPanleShow = false;
	            }
	        }).
            error(function (data, status, headers, config) {
                //$scope.product = {};
            });
	    }
	    else {
	        $scope.rateEditPanleShow = false;
	    }
	};
};
function SalesContractDetailCtrl($scope, $routeParams, $http, $location){
	$scope.$on('EventContractDetail',function(event){
		$("#contractDetailBox").animate({width:"500px"},400);
		//加载机会数据
	});
	
	$scope.hideContractDetail = function(){
	  $("#contractDetailBox").animate({width:"0px"},400,function(){});
  	};
};
function SalesAfterDetailCtrl($scope, $routeParams, $http, $location){
};

function SalesChanceEditCtrl($scope, $routeParams, $http, $location) {
    var from;
    $scope.$on('EventAddChance', function (event, fromscope) {
        console.log("EventAddChance");
        console.log(fromscope);
        from = fromscope;
        $scope.EditChance = {ChanceType:1,CustomerType:1,Isasyn:false};
        $('#addChanceModal').modal('show');
    });
    $scope.SalesAddChanceSubmit = function () {
        if ($scope.SalesAddChanceForm.$valid) {
            $scope.showerror = false;
            $http.post($sitecore.urls["salesAddChance"], {
                chanceType: $scope.EditChance.ChanceType,
                customerType: $scope.EditChance.CustomerType,
                username: $scope.EditChance.ContactName,
                chanceDetail: $scope.EditChance.Remark,
                tel: $scope.EditChance.Tel,
                phone: $scope.EditChance.Phone,
                email: $scope.EditChance.Email,
                qq: $scope.EditChance.Qq,
                Isasyn: $scope.EditChance.Isasyn
            }).success(function (data) {
                console.log(data);
                if (data.Error) {
                    alert(data.ErrorMessage);
                }
                else {
                    if (from && angular.isArray(from.chances)) {
                        $scope.EditChance.IdmarketingChance = data.Id;
                        from.chances.push(angular.copy($scope.EditChance));
                    }
                    $('#addChanceModal').modal('hide');
                }
            }).
            error(function (data, status, headers, config) {
                //$scope.product = {};
            });
        }
        else {
            $scope.showerror = true;
        }
    };
}
