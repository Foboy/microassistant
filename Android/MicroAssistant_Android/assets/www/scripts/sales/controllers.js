
function SalesMainCtrl($scope, $routeParams, $http, $location, salesmodel) {
	$scope.steps = $routeParams.steps;
	if(!$scope.steps)
	    $scope.steps = "chance";
	var pageIndex = parseInt($routeParams.pageIndex || 1);
	$scope.SalesChanceCount = 0;
	$scope.SalesVisitCount = 0;
	$scope.SalesContractCount = 0;
	$scope.SalesAfterCount = 0;
	console.log($routeParams)
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
	$scope.editChance = function () {
	    $scope.$broadcast('EventAddChance', this);
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

	$scope.$on('onLoginSuccess', function (event) {

	});
	

	$scope.changeStep = function (step) {
	    $scope.steps = step;
	    $scope.loadCurrentStepList(0);
	}

    /*
    $http.post($sitecore.urls["salesGetMarketingCount"], {}).success(function (data) {
        console.log(data);
        $scope.SalesChanceCount = data.Data[0];
        $scope.SalesVisitCount = data.Data[1];
        $scope.SalesContractCount = data.Data[2];
        $scope.SalesAfterCount = 0;
	}).
    error(function (data, status, headers, config) {
        $scope.afters = [];
    });*/
	
}

function SalesChanceMainCtrl($scope, $rootScope, $routeParams, $http, $location, salesmodel) {
    $scope.steps = "chance";
    $scope.hasMoreRecords = false;

    $scope.refreshList = function () {
        $scope.loadCurrentStepList(-1);
    }

    $scope.showMoreRecords = function () {
        $scope.loadCurrentStepList(1);
    }
    //显示列表内容
    $scope.loadCurrentStepList = function (pageindex) {
        console.log(pageindex)
        salesmodel.getlist($scope.steps, pageindex, 10, function (data) {
            if (data.Error) {
                alert(data.ErrorMessage);
            }
            $scope.hasMoreRecords = data.hasMore;

            $scope.chances = data.Items || [];
            console.log($scope.chances);
        });
    };
    $scope.loadCurrentStepList(0);

    $scope.chanceDetail = function (ev) {
        ev.stopPropagation();
        ev.preventDefault();
        $rootScope.CurrentChance = this.chance;
        angular.loadSection('sales-chance-detail');
        //ev.stopPropagation();
        //ev.preventDefault();
        // data-view-section="sales-chance-detail" data-async="partials/sales/chancedetail.html"
    };
}

function SalesVisitMainCtrl($scope, $rootScope, $routeParams, $http, $location, salesmodel) {
    $scope.steps = "visit";
    $scope.hasMoreRecords = false;

    $scope.refreshList = function () {
        $scope.loadCurrentStepList(-1);
    }

    $scope.showMoreRecords = function () {
        $scope.loadCurrentStepList(1);
    }
    //显示列表内容
    $scope.loadCurrentStepList = function (pageindex) {
        console.log(pageindex)
        salesmodel.getlist($scope.steps, pageindex, 10, function (data) {
            if (data.Error) {
                alert(data.ErrorMessage);
            }
            $scope.hasMoreRecords = data.hasMore;

            $scope.visits = data.Items || [];
            console.log($scope.chances);
        });
    };
    $scope.loadCurrentStepList(0);

    $scope.visitDetail = function (ev) {
        console.log('visitDetail')
        ev.stopPropagation();
        ev.preventDefault();
        $rootScope.CurrentChance = angular.extend($rootScope.CurrentChance || {}, this.visit);
        angular.loadSection('sales-visit-detail');
        $rootScope.$broadcast('EventUpdateVisitDetail');
        //ev.stopPropagation();
        //ev.preventDefault();
        // data-view-section="sales-visit-detail" data-async="partials/sales/visitdetail.html"
    };

    $scope.visitToVisit = function (ev)
    {
        console.log('visitToVisit')
        ev.stopPropagation();
        ev.preventDefault();
        $rootScope.SaleOperationFrom = 'visitlist';
        $rootScope.CurrentChance = angular.extend($rootScope.CurrentChance || {}, this.visit);
        angular.loadSection('sales-visit-create');
        //  data-view-section="sales-visit-create" data-async="partials/sales/visitcreate.html" 
    }
}

function SalesContractMainCtrl($scope, $rootScope, $routeParams, $http, $location, salesmodel) {
    $scope.steps = "contract";
    $scope.hasMoreRecords = false;

    $scope.refreshList = function () {
        $scope.loadCurrentStepList(-1);
    }

    $scope.showMoreRecords = function () {
        $scope.loadCurrentStepList(1);
    }
    //显示列表内容
    $scope.loadCurrentStepList = function (pageindex) {
        console.log(pageindex)
        salesmodel.getlist($scope.steps, pageindex, 10, function (data) {
            if (data.Error) {
                alert(data.ErrorMessage);
            }
            $scope.hasMoreRecords = data.hasMore;

            $scope.contracts = data.Items || [];
            console.log($scope.contracts);
        });
    };
    $scope.loadCurrentStepList(0);

    $scope.contractDetail = function (ev) {
        console.log('contractDetail')
        ev.stopPropagation();
        ev.preventDefault();
        $rootScope.CurrentContract = this.contract;
        salesmodel.getcontractdetail(this.contract.ContractNo, function (data) {
            if (data.Error) {
                alert(data.ErrorMessage);
            }
            $rootScope.CurrentContract = data.Data;
        }, function () { });
        angular.loadSection('sales-contract-detail');
        
        $rootScope.$broadcast('EventUpdateContractDetail');
        //ev.stopPropagation();
        //ev.preventDefault();
        // data-view-section="sales-visit-detail" data-async="partials/sales/visitdetail.html"
    };
}

function SalesChanceDetailCtrl($scope, $rootScope, $routeParams, $http, $location, $filter, $timeout, salesmodel) {
    var fromscope, chance, selectdate;
    //$scope.chance = salesmodel.currentChance();
    console.log($scope.CurrentChance);
    $timeout(function () { }, 1);

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
            }).lock({ selector: '#chanceDetailBox' });
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

	$scope.chanceToVisit = function (ev) {
	    ev.stopPropagation();
	    ev.preventDefault();
	    $rootScope.SaleOperationFrom = 'chance';
	    angular.loadSection('sales-visit-create');
	    $rootScope.$broadcast('EventVisitCreate');
	};
};
function SalesVisitCreateCtrl($scope, $rootScope, $routeParams, $http, $location, $timeout, salesmodel) {

    $scope.EditVisit = { Address: '正在获取...', VisitType: 1 };

    var addressGetting = false;

    $scope.getGeoAddress = function () {
        console.log('地址获取中...');
        if (addressGetting)
            return;
        addressGetting = true;
        try {
            $scope.EditVisit.Address = '正在获取...';
            navigator.geolocation.getCurrentPosition(function (position) {
                salesmodel.getaddress(position.coords.latitude+'', position.coords.longitude+'', function (data) {
                    console.log(data);
                    if (data.status == 'OK') {
                        $scope.EditVisit.Address = data.result.formatted_address;
                    }
                    addressGetting = false;
                }, function () {
                    addressGetting = false;
                });
                /*
                var element = document.getElementById('geolocation');
                element.innerHTML = 'Latitude: ' + position.coords.latitude + '<br />' +
                                    'Longitude: ' + position.coords.longitude + '<br />' +
                                    'Altitude: ' + position.coords.altitude + '<br />' +
                                    'Accuracy: ' + position.coords.accuracy + '<br />' +
                                    'Altitude Accuracy: ' + position.coords.altitudeAccuracy + '<br />' +
                                    'Heading: ' + position.coords.heading + '<br />' +
                                    'Speed: ' + position.coords.speed + '<br />' +
                                    'Timestamp: ' + position.timestamp + '<br />';*/
            }, function () {
                addressGetting = false;
                $scope.EditVisit.Address = '地址获取失败，点击重新获取';
            });
        }
        catch (e) {
            addressGetting = false;
            $scope.EditVisit.Address = '地址获取失败，点击重新获取';
        }
    };
    $scope.getGeoAddress();
    $scope.$on('EventVisitCreate', function (event) {
        $scope.getGeoAddress();
    });
    $timeout(function () { }, 1);

    $scope.addChanceVisit = function () {
        if ($scope.SalesAddChanceVisitFrom.$valid) {
            $scope.EditVisit.IdmarketingChance = $rootScope.CurrentChance.IdmarketingChance;
            salesmodel.addvisit($scope.EditVisit, function (data) {
                console.log(data);
                alert('拜访成功！');
                $rootScope.$broadcast('EventUpdateVisitDetail');
                $timeout(function () {
                    angular.loadSection('sales-visit-detail');
                }, 2000);

            }, function () { });
        }
    };

    $scope.createCancel = function () {
        if ($rootScope.SaleOperationFrom == 'visit') {
            angular.loadSection('sales-visit-detail');
        }
        else if ($rootScope.SaleOperationFrom == 'visitlist') {
            angular.loadSection('sales-visit-list');
        }
        else {
            angular.loadSection('sales-chance-detail');
        }
    };
};

function SalesVisitDetailCtrl($scope, $rootScope, $routeParams, $http, $location, salesmodel) {
    var chance;
    $("#visitDetailBox").hide();
    var emptyVisit = { VisitType: 1, Address: '' };

    $scope.loadVisitDetailList = function (pageindex) {
        salesmodel.getvisitdetaillist($rootScope.CurrentChance.IdmarketingChance, pageindex, 50, function (data) {
            if (data.Error) {
                alert(data.ErrorMessage);
            }
            $scope.hasMoreRecords = data.hasMore;

            $scope.visittimes = data.Items || [];
            console.log($scope.visittimes);
        });
    };

    $scope.visitToVisit = function (ev) {
        ev.stopPropagation();
        ev.preventDefault();
        angular.loadSection('sales-visit-create');
        $rootScope.SaleOperationFrom = 'visit';
        $rootScope.$broadcast('EventVisitCreate');
    }

    $scope.$on('EventUpdateVisitDetail', function (event, from) {
        $scope.loadVisitDetailList(-1);
        //加载机会数据
    });

    $scope.loadVisitDetailList(-1);

    $scope.VisitNewNum = '初';
    var $ratePanle,$addressPanle,$prisePanle;



    $scope.visitFormReset = function () {
        $scope.EditVisit = angular.copy(emptyVisit);
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
	                $scope.VisitNewNum = $scope.parseVisitNum(($scope.chance_visits.length || 0) + 1);
	            }
	        }).
            error(function (data, status, headers, config) {
                $scope.chance_visits = {};
            });
	    }
	};
	
	$scope.addVisitLocation = function (ev) {
	    $scope.EditVisit.NewAddress = $scope.EditVisit.Address;
	    if ($addressPanle) {
	        $addressPanle.coolpopover('toggle');
	    }
	    else {
	        $addressPanle = $(ev.target);
	        $addressPanle.coolpopover({ content: $("#addressInputPanle"), placement: 'bottom', html: true, trigger: 'manual' });
	        $addressPanle.coolpopover('show');
	    }
        /*
		$("#salesVisitMapIframe").attr({src:'partials/others/locationselectmap.html?lat='+$scope.visit_lat+'&lng='+$scope.visit_lng})
		$('#visitLocationSelectModal').modal('show');
        */
	};

	$scope.newAddressSubmit = function () {
	    if ($scope.SalesVisitAddressInputFrom.$valid) {
	        $scope.showerror1 = false;
	        $addressPanle.coolpopover('hide');
	        $scope.EditVisit.Address = $scope.EditVisit.NewAddress;
	    }
	    else {
	        $scope.showerror1 = true;
	    }
	}
	$scope.newAddressCancel = function () {
	    $addressPanle.coolpopover('hide');
	}

	$scope.addVisitPrise = function (ev) {
	    $scope.EditVisit.NewAmount = $scope.EditVisit.Amount;
	    if ($prisePanle) {
	        $prisePanle.coolpopover('toggle');
	    }
	    else {
	        $prisePanle = $(ev.target);
	        $prisePanle.coolpopover({ content: $("#priceInputPanle"), placement: 'bottom', html: true, trigger: 'manual' });
	        $prisePanle.coolpopover('show');
	    }
	};
	$scope.newAmountSubmit = function () {
	    if ($scope.SalesVisitPriceInputFrom.$valid) {
	        $scope.showerror2 = false;
	        $prisePanle.coolpopover('hide');
	        $scope.EditVisit.Amount = $scope.EditVisit.NewAmount;
	    }
	    else {
	        $scope.showerror2 = true;
	    }
	}
	$scope.newAmountCancel = function () {
	    $prisePanle.coolpopover('hide');
	}

	$scope.markAddressOnMap = function () {
	    var address = $scope.parseAddress(this.visit.Address);
	    $("#salesVisitMapIframe").attr({ src: 'partials/others/locationselectmap.html?lookonly=true&lat=' + address.lat + '&lng=' + address.lng })
	    $('#visitLocationSelectModal').modal('show');
	};
	
    /*
	utilities.registeriframelistener("visitlocation",function(){
	    $('#visitLocationSelectModal').modal('hide');
	    var lat = arguments[0], lng = arguments[1], address = arguments[2];
	    $scope.$apply(function () {
	        $scope.visit_lat = lat;
	        $scope.visit_lng = lng;
	        $scope.EditVisit.Address = address;
	    });
	});*/

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

	$scope.parseVisitIcon = function (type) {
	    type = type + "";
	    switch (type)
	    {
	        case "1":
	            return "visit_fs_phone";
	        case "2":
	            return "visit_fs_mt";
	        case "3":
	            return "visit_fs_zx";
	        default:
	            return "visit_fs_mt";
	    }
	};

	$scope.addChanceVisit = function () {
	    if ($scope.SalesAddChanceVisitFrom.$valid) {
	        //console.log($scope.EditVisit)
	        $scope.showerror = false;
	        $http.post(
                $scope.EditVisit.IdmarketingVisit ? $sitecore.urls["salesEditChanceVisits"] : $sitecore.urls["salesAddChanceVisits"], {
	            cid: chance.IdmarketingChance,
	            vid: $scope.EditVisit.IdmarketingVisit,
	            visitType: $scope.EditVisit.VisitType,
	            remark: $scope.EditVisit.Remark,
	            amount: $scope.EditVisit.Amount || 0,
	            address: $scope.combineAdderess()
	        }).success(function (data) {
	            //console.log(data);
	            if (data.Error) {
	                alert(data.ErrorMessage);
	            }
	            else {
	                if ($scope.EditVisit.IdmarketingVisit)
	                {
	                    angular.extend($scope.EditVisit_SourceObject, $scope.EditVisit);
	                    $scope.EditVisit = angular.copy(emptyVisit);
	                    $scope.addNewVisit();
	                }
                    else
	                {
	                    var addedvisit = angular.copy($scope.EditVisit);
	                    addedvisit.Address = $scope.combineAdderess();
	                    addedvisit.IdmarketingVisit = data.Id;
	                    $scope.chance_visits.unshift(addedvisit);
	                    $scope.addvisitpanleshow = false;
	                    $scope.visitFormReset();
	                    chance.LastVisitTime = new Date();
	                    chance.VisitNum = (chance.VisitNum || 0) + 1;
	                    $scope.addNewVisit();
	                }
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


	$scope.changeRateShow = function (ev) {
	    if ($ratePanle) {
	        $ratePanle.coolpopover('toggle');
	    }
	    else {
	        $ratePanle = $(ev.target);
	        $ratePanle.coolpopover({ content: $("#salesRateEditePanle"), placement: 'bottom', html: true, trigger: 'manual' });
	        $ratePanle.coolpopover('show');
	    }
	}
	$scope.changeRateCancel = function () {
	    $ratePanle.coolpopover('hide');
	};
	$scope.changeRateSubmit = function () {
	    if ($scope.chance.Rate == $scope.NewRate)
	    {
	        $scope.changeRateCancel();
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
	                $scope.changeRateCancel();
	            }
	        }).
            error(function (data, status, headers, config) {
                //$scope.product = {};
            });
	    }
	};

	$scope.parseVisitNum = function (index) {
	    index = Math.abs(index);
	    return (index == 1 ? '初' : '第' + $scope.parseNumberToChinese(index));
	};

	$scope.addNewVisit = function (ev) {
	    $scope.VisitNewNum = $scope.parseVisitNum(($scope.chance_visits.length || 0) + 1);
	    $("#visitEditBox").siblings('.li-boxs').show();
	    $('#newVisitEditContainer').append($("#visitEditBox"));
	    $scope.visitFormReset();
	    $scope.addvisitpanleshow = true;
	};

	$scope.addNewVisitCancel = function () {
	    $("#visitEditBox").siblings('.li-boxs').show();
	    $scope.addvisitpanleshow = false;
	};

	$scope.editExistVisit = function (ev) {
	    $scope.VisitNewNum = $scope.parseVisitNum(this.$index - $scope.chance_visits.length);
	    $scope.EditVisit = angular.copy(this.visit);
	    //console.log($scope.EditVisit)
	    $scope.EditVisit_SourceObject = this.visit;
	    $("#visitEditBox").siblings('.li-boxs').show();
	    $(ev.target).parents('.li-boxs').hide().after($("#visitEditBox"));
	    $scope.addvisitpanleshow = true;
	};
};
function SalesContractDetailCtrl($scope, $routeParams, $http, $location) {
    $("#contractDetailBox").hide();
    $scope.tabIndex = 1;
    var contract;
    $scope.$on('EventContractDetail', function (event,from) {
        $("#contractDetailBox").show();
        $("#contractDetailBox").animate({ width: "500px" }, 400);
        contract = from.contract;
        if (!$scope.contract || contract.ContractNo != $scope.contract.ContractNo) {
            $http.post($sitecore.urls["salesGetConractByContractNo"], { contractNo: contract.ContractNo }).success(function (data) {
                console.log(data);
                $scope.contract = data.Data;
            }).
            error(function (data, status, headers, config) {
                $scope.contract = {};
            });
        }
	});
	
    $scope.hideContractDetail = function () {
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
    var chance;
    console.log('SalesChanceEditCtrl')
    console.log($element)
    var customSource = [];
    var selectCustomId = 0;

    $('#salesChanceContactNameInput').change(function () {
        var name = $(this).val();
        console.log('matchname' + name);
        //var contactInfo = {Phone:'',Tel:'',Email:''};
        var matched = false;
        for (var i = 0; i < customSource.length; i++)
        {
            var custom = customSource[i];
            if (name == custom.DisplayName)
            {
                matched = true;
                selectCustomId = custom.CustomerEntId || custom.CustomerPrivateId;
                if ($.trim(name) != name)
                {
                    var realname = $.trim(name);
                    $(this).val(realname);
                }
                var contactInfo = {
                    Phone: custom.ContactMobile || custom.Mobile,
                    Tel: custom.ContactPhone || custom.Phone,
                    Email: custom.ContactEmail || custom.Email,
                    Qq: custom.ContactQq || custom.Qq
                };
                $scope.$apply(function () {
                    angular.extend($scope.EditChance, contactInfo);
                });
            }
        }
        selectCustomId = matched ? selectCustomId : 0;
    });

    $scope.$on('EventAddChance', function (event, fromscope) {
        console.log("EventAddChance");
        console.log(fromscope);
        console.log($scope);
        from = fromscope;
        chance = fromscope.chance || {};
        $scope.EditChance = chance.IdmarketingChance ? angular.copy(chance) : { ChanceType: 1, CustomerType: 1 };
        $('#addChanceModal').modal('show');
    });

    $scope.customerTypeChange = function () {
        customSource = [];
        selectCustomId = 0;
        $scope.EditChance.ContactName = '';
    };

    $scope.contactNameChange = function () {
        if (!$scope.EditChance.ContactName || !$scope.EditChance.ContactName.length)
            return;
        $http.post($scope.EditChance.CustomerType == 1 ? $sitecore.urls["salesSearchCustomerEntByName"] : $sitecore.urls["salesSearchCustomerPrivateByName"], {
            name: $scope.EditChance.ContactName
        }).success(function (data) {
            console.log(data)
            var datasource = [];
            customSource = [];
            $scope.searchCustomers = data.Data;

            var displayNameParse = function (item) {
                if (item.DisplayNameParsed)
                    return;
                if ($scope.EditChance.CustomerType == 1) {
                    item.DisplayName = item.EntName;
                }
                else {
                    item.DisplayName = item.Name;
                }
                item.DisplayNameParsed = true;
            };
            for (var i = 0; i < data.Data.length; i++)
            {
                var item = data.Data[i];
                var samecount = 1;
                displayNameParse(item);
                for (var j = i + 1; j < data.Data.length; j++) {
                    var sitem = data.Data[j];
                    displayNameParse(sitem);
                    if (item.DisplayName == sitem.DisplayName) {
                        samecount++;
                        sitem.DisplayName = sitem.DisplayName + (new Array(samecount).join(' '));
                    }
                }
                customSource.push(item);
                datasource.push(item.DisplayName);
            }
            
            var typeahead = $('#salesChanceContactNameInput').data('typeahead');
            if (typeahead) {
                typeahead.source = datasource;
            }
            else {
                $('#salesChanceContactNameInput').typeahead({ source: datasource });
            }
        }).
        error(function (data, status, headers, config) {

        });

    };

    $scope.SalesAddChanceSubmit = function () {
        console.log($scope.EditChance)
        $scope.EditChance.ContactName = $('#salesChanceContactNameInput').val();
        if ($scope.SalesAddChanceForm.$valid) {
            $scope.showerror = false;

            $http.post(chance.IdmarketingChance ? $sitecore.urls["salesChanceEdit"] : $sitecore.urls["salesAddChance"], {
                cid: chance.IdmarketingChance || 0,
                chanceType: $scope.EditChance.ChanceType,
                customerType: $scope.EditChance.CustomerType,
                username: $scope.EditChance.ContactName,
                chanceDetail: $scope.EditChance.Remark,
                tel: $scope.EditChance.Tel,
                phone: $scope.EditChance.Phone,
                email: $scope.EditChance.Email,
                qq: $scope.EditChance.Qq,
                customerId: $scope.EditChance.UserId || selectCustomId
            }).success(function (data) {
                console.log(data);
                if (data.Error) {
                    alert(data.ErrorMessage);
                }
                else {
                    if (from && angular.isArray($scope.chances)) {
                        $scope.EditChance.IdmarketingChance = data.Id;
                        $scope.EditChance.AddTime = new Date();
                        if (chance.IdmarketingChance) {
                            angular.forEach($scope.chances, function (value) {
                                if (value.IdmarketingChance == $scope.EditChance.IdmarketingChance)
                                    angular.extend(value, $scope.EditChance);
                            });
                        }
                        else {
                            from.chances.push(angular.copy($scope.EditChance));
                        }
                    }
                    $('#addChanceModal').modal('hide');
                }
            }).
            error(function (data, status, headers, config) {
                //$scope.product = {};
            }).lock({ selector: '#addChanceModal' });
        }
        else {
            $scope.showerror = true;
        }
    };
}

function SalesContractEditCtrl($scope, $routeParams, $http, $location, $filter) {
    var from;
    var chance;
    $scope.$on('EventAddContract', function (event, fromscope) {
        console.log("EventAddContract");
        console.log(fromscope);
        from = fromscope;
        chance = from.cvisit || { IdmarketingChance: 0 };
        //$scope.chance = chance;
        $scope.EditContract = {
            HowtopayListCount: 3,
            Howtopay: 0,
            ContractNo: $filter('date')(new Date(), 'yyyyMMddHHmmss'),
            attachments: []
        };
        $scope.EditContract.chanceId = chance.IdmarketingChance;
        $scope.salesContractEditPage = 1;
        $scope.ContractPayChanced();
        $('#salesContractEditModal').modal('show');
        $("#salesContractEditPageOne .form_datetime").datetimepicker({
            minView: 2,
            language: 'zh-CN',
            format: "yyyy/mm/dd",
            autoclose: true,
            todayBtn: true,
            pickerPosition: "top-left"
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
                saveSource: 1,
                fileType: 3
            },
            'swf': 'js/uploadify/uploadify.swf',
            'uploader': $sitecore.urls["UploadFile"],
            'height': 70,
            'width': 190,
            'queueID': 'salesContractFileQueue',
            onUploadSuccess: function (file, data, response) {
                var data = $.parseJSON(data);
                if (data.Error) {
                    alert(data.ErrorMessage);
                }
                else {
                    $.post($sitecore.urls["AddPic"], { sourcePath: data.Data.PicUrl, picType: 3, description: file.name }, function (picdata) {
                        if (picdata.Error) {
                            alert(picdata.ErrorMessage);
                        }
                        else {
                            $scope.$apply(function () {
                                $scope.EditContract.attachments.push(picdata.Data);
                            });
                        }
                    });
                }
            }
        });
    });

    $scope.DeleteFile = function () {
        var file = this.file;
        for (var i = 0; i < $scope.EditContract.attachments.length; i++)
        {
            if (file.PicId == $scope.EditContract.attachments[i].PicId)
            {
                $scope.EditContract.attachments.splice(i, 1);
                return;
            }
        }
    }

    $scope.ShowDatePicker = function (ev) {
        var item = this.payItem;
        $(ev.target).datetimepicker({
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
                angular.forEach($scope.EditContract.HowtopayList, function (value, key) {
                    if (value.$index == item.$index)
                        value.PayTime = ev.target.value;

                });
            });
        });
        $(ev.target).datetimepicker('update');
        $(ev.target).datetimepicker('show');
    };

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
            var files = [];
            angular.forEach($scope.EditContract.HowtopayList, function (value, key) {
                if (value.enable == 'display')
                    paylist.push({ InstalmentsNo: key, Amount: value.Amount, PayTime: value.PayTime, ReceivedTime: value.PayTime, Isreceived: 1, ContractNo: $scope.EditContract.ContractNo });
                
            });
            angular.forEach($scope.EditContract.attachments, function (value, key) {
                files.push(value.PicId);

            });

            if (paylist.length == 0)
                paylist.push({ InstalmentsNo: 1, Amount: $scope.EditContract.Amount, PayTime: $scope.EditContract.StartTime, ReceivedTime: $scope.EditContract.StartTime, Isreceived: 1, ContractNo: $scope.EditContract.ContractNo });
  
            $http.post($scope.EditContract.ContractInfoId ? $sitecore.urls["salesAddConract"] : $sitecore.urls["salesAddConract"], {
                ChanceId: chance.IdmarketingChance,
                ContractNo: $scope.EditContract.ContractNo,
                CName: $scope.EditContract.CName,
                CustomerName: $scope.EditContract.CustomerName,
                StartTime: $scope.EditContract.StartTime,
                EndTime: $scope.EditContract.EndTime,
                ContractTime: $scope.EditContract.ContractTime,
                Amount: $scope.EditContract.Amount,
                HowToPay: $scope.EditContract.Howtopay,
                HowtopayList: paylist,
                attachments:files
            }).success(function (data) {
                console.log(data);
                if (data.Error) {
                    alert(data.ErrorMessage);
                }
                else {
                    $scope.EditContract.ContractInfoId = data.Id;
                    if (from.contracts) {
                        from.contracts.push(angular.copy($scope.EditContract));
                    }
                    else if ($scope.contracts) {
                        from.contracts.push(angular.copy($scope.EditContract));
                    }
                    $('#salesContractEditModal').modal('hide');
                }
                $scope.product = data;
            }).
            error(function (data, status, headers, config) {
                $scope.product = {};
            }).lock({ selector: '#salesContractEditModal' });
        }
    };

}
