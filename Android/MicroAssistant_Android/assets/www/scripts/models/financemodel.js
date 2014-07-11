angular.module('models.finance', ['$serverModels']);
angular.module('models.finance').factory('financemodel', ['$serverModels', function ($serverModels) {

    var financemodel = $serverModels();

    financemodel.getlist = function (steps, pageindex, pagesize, scb, ecb) {
        var url;
        switch (steps) {
            case 'receivable':
                url = $sitecore.urls["receivablesfinanceList"];
                break;
            case 'payable':
                url = $sitecore.urls["payablesfinanceList"];
                break;
        }

        return financemodel.querylist({
            url: url,
            data: { pageIndex: pageindex, pageSize: pagesize },
            lock: true,
            refresh: pageindex < 0,
            getMore: pageindex > 0,
            cacheKey: 'finance_' + steps,
            pagesize: pagesize,
            scb: scb,
            ecb: ecb
        });
    };

    return financemodel;
}]);