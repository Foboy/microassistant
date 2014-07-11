angular.module('models.respic', ['$serverModels']);
angular.module('models.respic').factory('respicmodel', ['$serverModels', function ($serverModels) {

    var respicmodel = $serverModels();

    respicmodel.get = function (pid, scb, ecb) {
        return respicmodel.query({
            url: $sitecore.urls["GetPic"], 
            data: { picid: pid },
            lock: false,
            scb: scb,
            ecb: ecb
        });
    };

    return respicmodel;
}]);