angular.module('models.user', ['$serverModels']);
angular.module('models.user').factory('usermodel', ['$serverModels', '$dataCache', function ($serverModels, $dataCache) {

    var usermodel = $serverModels();
    var currentUserKey = 'currentUserKey';
    var cache = $dataCache.getUserCache();

    function removeUserCache() {
        cache.removeAll();
    }

    usermodel.logout = function () {
        removeUserCache();
    }

    usermodel.login = function (email, psw, scb, ecb) {
        removeUserCache();
        return usermodel.query({
            url: $sitecore.urls["userLogin"],
            data: { account: email, pwd: psw },
            lock: true,
            scb: scb,
            ecb: ecb
        });
    };

    usermodel.register = function (name, email, psw, code, scb, ecb) {
        removeUserCache();
        return usermodel.query({
            url: $sitecore.urls["userRegister"],
            data: { username: name, account: email, pwd: psw, entCode: code },
            lock: true,
            scb: scb,
            ecb: ecb
        });
    }

    usermodel.entRegister = function (name, email, psw, scb, ecb) {
        removeUserCache();
        return usermodel.query({
            url: $sitecore.urls["userCurrentUser"],
            data: { entName: name, account: email, pwd: psw },
            lock: true,
            scb: scb,
            ecb: ecb
        });
    }

    usermodel.loadCurrentUser = function (scb, ecb) {
        var currentUser = cache.get(currentUserKey);
        if (currentUser)
        {
            typeof scb == 'function' && scb.apply(null, [currentUser]);
            return;
        }
        return usermodel.query({
            url: $sitecore.urls["userCurrentUser"],
            data: {},
            lock: false,
            scb: function (data) {
                cache.put(currentUserKey, data);
                typeof scb == 'function' && scb.apply(this, arguments);
            },
            ecb:ecb
        });
    };

    return usermodel;
}]);