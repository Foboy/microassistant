angular.module('models.client', ['$serverModels']);
angular.module('models.client').factory('clientmodel', ['$serverModels', function ($serverModels) {

    var clientmodel = $serverModels();

    clientmodel.getlist = function (steps, pageindex, pagesize, scb, ecb) {
        var url;
        switch (steps) {
            case 'enterprise':
                url = $sitecore.urls["SearchCustomerEntByOwnerId"];
                break;
            case 'personal':
                url = $sitecore.urls["SearchCustomerPrivByOwnerId"];
                break;
        }

        return clientmodel.querylist({
            url: url,
            data: { pageIndex: pageindex, pageSize: pagesize },
            lock: true,
            refresh: pageindex < 0,
            getMore: pageindex > 0,
            cacheKey: 'client_' + steps,
            pagesize: pagesize,
            scb: scb,
            ecb: ecb
        });
    };

    return clientmodel;
}]);