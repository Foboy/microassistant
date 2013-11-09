
function SalesMainCtrl($scope, $routeParams, $http, $location){
	$scope.steps = $routeParams.steps;
	if(!$scope.steps)
	    $scope.steps = "chance";
	$scope.SalesChanceCount = 0;
	$scope.SalesVisitCount = 0;
	$scope.SalesContractCount = 0;
	$scope.SalesAfterCount = 0;
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
		        if (!$scope.cvisits) {
		            $http.post($sitecore.urls["salesChanceVisitList"], { pageIndex: $routeParams.pageIndex || 0, pageSize: 20 }).success(function (data) {
		                console.log(data);
		                $scope.ActPageIndex = $routeParams.pageIndex || 0;
		                $scope.cvisits = data.Data.Items;
		            }).
                    error(function (data, status, headers, config) {
                        $scope.cvisits = [];
                    });
		        }
				break;
			case 'contract':
			    if (!$scope.contracts) {
			        $http.post($sitecore.urls["salesConractList"], { pageIndex: $routeParams.pageIndex || 0, pageSize: 20 }).success(function (data) {
			            console.log(data);
			            $scope.ActPageIndex = $routeParams.pageIndex || 0;
			            $scope.contracts = data.Data.Items;
			        }).
                    error(function (data, status, headers, config) {
                        $scope.contracts = [];
                    });
			    }
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
	$scope.addCurrentStepItem = function (step) {
	    var steps = step || $scope.steps;
	    switch (steps)
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

    $http.post($sitecore.urls["salesGetMarketingCount"], {}).success(function (data) {
        console.log(data);
        $scope.SalesChanceCount = data.Data[0];
        $scope.SalesVisitCount = data.Data[1];
        $scope.SalesContractCount = data.Data[2];
        $scope.SalesAfterCount = 0;
	}).
    error(function (data, status, headers, config) {
        $scope.afters = [];
    });
	
}

function SalesChanceDetailCtrl($scope, $routeParams, $http, $location, $filter) {
    var fromscope,chance,selectdate;
    $("#chanceDetailBox").hide();
    $scope.$on('EventChanceDetail', function (event, from) {
        $("#chanceDetailBox").show();
        $("#chanceDetailBox").animate({ width: "350px" }, 300, function () {
            $("#chanceDetailBox .form_datetime").datetimepicker({
                minView:2,
                language:'zh-CN',
                format: "yyyy/mm/dd",
                autoclose: true,
                todayBtn: true,
                pickerPosition: "bottom-left"
            })
            .on('changeDate', function (ev) {
                $scope.$apply(function () {
                    selectdate = ev.date;
                    $scope.chance.FormatAddTime = ev.target.value
                });
                console.log();
            });
        });
        fromscope = from;
        chance = from.chance;
        chance.FormatAddTime = $scope.parseJsonDate(chance.AddTime, 'yyyy/MM/dd');
        chance.CustomerType = chance.CustomerType + "";
        chance.ChanceType = chance.ChanceType + "";
        $scope.oldchance = chance;
        $scope.chance = angular.copy(chance);
        

		//加载机会数据
    });

    $scope.chanceDetailHasChanged = function () {
        return !angular.equals($scope.oldchance, $scope.chance);
    };
	
	$scope.hideChanceDetail = function(callback){
	    $("#chanceDetailBox").animate({ width: "0px" }, 300, function () {
	        $("#chanceDetailBox").hide();
	        if (typeof callback === 'function')
	        {
	            callback();
	        }
	    });
	};

	$scope.SalesChanceDetailSubmit = function () {
	    if ($scope.salesChanceDetailForm.$valid) {
	        $scope.showerror = false;
	        $http.post($sitecore.urls["salesChanceEdit"], {
	            cid: chance.IdmarketingChance,
	            chanceType: $scope.chance.ChanceType,
	            customerType: $scope.chance.CustomerType,
	            username: $scope.chance.ContactName,
	            chanceDetail: $scope.chance.Remark,
	            tel: $scope.chance.Tel,
	            phone: $scope.chance.Phone,
	            email: $scope.chance.Email,
	            qq: $scope.chance.Qq,
	            Isasyn: $scope.chance.Isasyn
	        }).success(function (data) {
	            console.log(data);
	            if (data.Error) {
	                alert(data.ErrorMessage);
	            }
	            else {
	                fromscope.chance = $scope.chance;
	                fromscope.chance.AddTime = selectdate;
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

	$scope.visit = function () {
	    $scope.hideChanceDetail(function () {
	        $scope.$apply(function () {
	            $scope.showVisitDetail();
	        });
	    });
	};
};
function SalesVisitDetailCtrl($scope, $routeParams, $http, $location) {
    var chance;
    $("#visitDetailBox").hide();
    $scope.$on('EventVisitDetail', function (event, from) {
        console.log(from)
        chance = from.chance || from.cvisit;
        $scope.chance = chance;
        $scope.NewRate = $scope.chance.Rate;
        $scope.chanceVisitDetail();
        $scope.visitFormReset();
        $("#visitDetailBox").show();
		$("#visitDetailBox").animate({width:"900px"},500);
		//加载机会数据
    });
    

    $scope.visitFormReset = function () {
        $scope.EditVisit = { VisitType: 1, Address: '' };
        $scope.SalesAddChanceVisitFrom.$setPristine();
    };
	
	$scope.hideVisitDetail = function(){
	    $("#visitDetailBox").animate({ width: "0px" }, 500, function () { $("#visitDetailBox").hide(); });

	};

	$scope.chanceVisitDetail = function () {

	    if (!$scope.chance_visits || chance.IdmarketingChance != $scope.IdmarketingChance) {
	        $http.post($sitecore.urls["salesChanceVisitsList"], { cid: chance.IdmarketingChance, pageIndex: 0, pageSize: 20 }).success(function (data) {
	            console.log(data);
	            if (data.Error) {
	                alert(data.ErrorMessage);
	            }
	            else {
	                $scope.IdmarketingChance = chance.IdmarketingChance;
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
	        $http.post($sitecore.urls["salesAddChanceVisits"], { cid: chance.IdmarketingChance, visitType: $scope.EditVisit.VisitType, remark: $scope.EditVisit.Remark, amount: 0, address: $scope.combineAdderess() }).success(function (data) {
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
	                chance.LastVisitTime = new Date();
	                chance.VisitNum = (chance.VisitNum || 0) + 1;
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
	        $http.post($sitecore.urls["salesRateChange"], { cid: chance.IdmarketingChance, num: $scope.NewRate }).success(function (data) {
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
	$scope.addNewVisit = function (ev) {
	    $("#visitEditBox").prev('.li-boxs').show();
	    $(ev.target).after($("#visitEditBox"));
	    $scope.addvisitpanleshow = true;
	};

	$scope.addNewVisitCancel = function () {
	    $("#visitEditBox").prev('.li-boxs').show();
	    $scope.addvisitpanleshow = false;
	};

	$scope.editExistVisit = function (ev) {
	    $(ev.target).parents('.li-boxs').hide().after($("#visitEditBox"));
	    $scope.addvisitpanleshow = true;
	};
};
function SalesContractDetailCtrl($scope, $routeParams, $http, $location) {
    $("#contractDetailBox").hide();
    $scope.tabIndex = 1;
    $scope.$on('EventContractDetail', function (event,from) {
        $("#contractDetailBox").show();
        $("#contractDetailBox").animate({ width: "500px" }, 400);
        $scope.contract = from.contract;

	});
	
	$scope.hideContractDetail = function(){
	    $("#contractDetailBox").animate({ width: "0px" }, 400, function () { $("#contractDetailBox").hide(); });
	    
	};

	$scope.contractDynamic = function () {
	    $scope.tabIndex = 2;
	}
	
};
function SalesAfterDetailCtrl($scope, $routeParams, $http, $location){
};

function SalesChanceEditCtrl($scope, $routeParams, $http, $location, $element) {
    var from;
    console.log('SalesChanceEditCtrl')
    console.log($element)
    $scope.$on('EventAddChance', function (event, fromscope) {
        console.log("EventAddChance");
        console.log(fromscope);
        console.log($scope);
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
                        $scope.EditChance.AddTime = new Date();
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

function SalesContractEditCtrl($scope, $routeParams, $http, $location) {
    var from;
    $scope.$on('EventAddContract', function (event, fromscope) {
        console.log("EventAddContract");
        console.log(fromscope);
        from = fromscope;
        $scope.EditContract = { HowtopayListCount: 3, Howtopay: 0 };
        $scope.salesContractEditPage = 1;
        $scope.ContractPayChanced();
        $('#salesContractEditModal').modal('show');
        $("#salesContractEditPageOne .form_datetime").datetimepicker({
            minView: 2,
            language: 'zh-CN',
            format: "yyyy/mm/dd",
            autoclose: true,
            todayBtn: true,
            pickerPosition: "bottom-left"
        })
        .on('changeDate', function (ev) {
            $scope.$apply(function () {
                console.log(ev);
                console.log($scope.EditContract);
                var modelName = $(ev.target).attr('ng-model');
                if (modelName.indexOf('StartTime') >= 0)
                {
                    $scope.EditContract.StartTime = ev.target.value;
                }
                else if (modelName.indexOf('EndTime') >= 0)
                {
                    $scope.EditContract.EndTime = ev.target.value;
                }
                else if (modelName.indexOf('ContractTime') >= 0) {
                    $scope.EditContract.ContractTime = ev.target.value;
                }
            });
        });

        $('#salesContractUpload').cuploadify({
            'formData': {
                'token': 'sss'
            },
            'swf': 'js/uploadify/uploadify.swf',
            'uploader': '/Upload/Uploader/71358f72c447e0ec2ecba71636907898?queryData=width-126,height-126&imageWidth=470&imageHeight=0',
            'height': 70,
            'width': 190,
            onUploadComplete: function (response) {

            }
        });
    });

    $scope.$on('animate-enter', function (event, element) {
        $(element).find('input.form_datetime_dynamic').datetimepicker({
            minView: 2,
            language: 'zh-CN',
            format: "yyyy/mm/dd",
            autoclose: true,
            todayBtn: true,
            pickerPosition: "bottom-left"
        })
        .on('changeDate', function (ev) {
            $scope.$apply(function () {
                console.log(ev);
                var index = $(ev.target).attr('item-index');
                angular.forEach($scope.EditContract.HowtopayList, function (value, key) {
                    if (value.$index == index)
                        value.PayTime = ev.target.value;

                });
            });
        });
    });

    $scope.ContractPayChanced = function () {
        var count = $scope.EditContract.Howtopay == 1 ? $scope.EditContract.HowtopayListCount : 0;
        if ($scope.EditContract.HowtopayList && $scope.EditContract.HowtopayList.length) {
            for (var i = 0; i < $scope.EditContract.HowtopayList.length; i++) {
                if (i < count) {
                    $scope.EditContract.HowtopayList[i].enable = 'display';
                }
                else {
                    $scope.EditContract.HowtopayList[i].enable = 'hidden';
                }
            }
        }
        else {
            $scope.EditContract.HowtopayList = new Array(24);
            for (var i = 0; i < $scope.EditContract.HowtopayList.length; i++) {
                var $index = i;
                $scope.EditContract.HowtopayList[i] = { $index: $index, enable: 'hidden' };
            }
            for (var i = 0; i < count; i++) {
                $scope.EditContract.HowtopayList[i].enable = 'display';
            }
        }
    }

    $scope.SalesContractEditSubmit = function () {
        if (!$scope.SalesContractEditFormOne.$valid)
        {
            $scope.showerror1 = true;
            $scope.salesContractEditPage = 1;
            return;
        }
        else if (!$scope.SalesContractEditFormTwo.$valid) {
            $scope.showerror2 = true;
            $scope.salesContractEditPage = 2;
            return;
        }
        else {
            $scope.showerror1 = false;
            $scope.showerror2 = false;

            var paylist = [];
            angular.forEach($scope.EditContract.HowtopayList, function (value, key) {
                if (value.enable == 'display')
                    paylist.push({ InstalmentsNo: key, Amount: value.Amount, PayTime: value.PayTime, ReceivedTime: value.PayTime, Isreceived: 1, ContractNo: $scope.EditContract.ContractNo });
                
            });

            if (paylist.length == 0)
                paylist.push({ InstalmentsNo: 1, Amount: $scope.EditContract.Amount, PayTime: $scope.EditContract.StartTime, ReceivedTime: $scope.EditContract.StartTime, Isreceived: 1, ContractNo: $scope.EditContract.ContractNo });

            $http.post($scope.EditContract.ContractInfoId ? $sitecore.urls["salesAddConract"] : $sitecore.urls["salesAddConract"], { ContractNo: $scope.EditContract.ContractNo, CName: $scope.EditContract.CName, CustomerName: $scope.EditContract.CustomerName, StartTime: $scope.EditContract.StartTime, EndTime: $scope.EditContract.EndTime, ContractTime: $scope.EditContract.ContractTime, Amount: $scope.EditContract.Amount, HowToPay: $scope.EditContract.Howtopay, HowtopayList: paylist }).success(function (data) {
                console.log(data);
                if (data.Error) {
                    alert(data.ErrorMessage);
                }
                else {
                    $scope.EditContract.ContractInfoId = data.Id;
                    $scope.contracts.push(angular.copy($scope.EditContract));
                    $('#salesContractEditModal').modal('hide');
                }
                $scope.product = data;
            }).
            error(function (data, status, headers, config) {
                $scope.product = {};
            });
        }
    };

}
