angular.module('$serverModels', []).factory('$serverModels', ['$pagination', '$dataCache', '$sitecore', '$http', '$q', function ($pagination, $dataCache, $sitecore, $http, $q) {

    function ServerModelsFactory() {

        var thenFactoryMethod = function (httpPromise, successcb, errorcb, parseFn, lock) {
            var scb = successcb || angular.noop;
            var ecb = errorcb || angular.noop;
            var parseFn = parseFn || Resource.defaultParseFn;
            if (lock) {
                angular.showLoading(lock);
            }
            return httpPromise.then(function (response) {
                var result;
                if (lock) {
                    angular.hideLoading(lock);
                }
                if (response.Error) {
                    if (ecb == angular.noop) {
                        Resource.error(response.ErrorMessage);
                    }
                }
                else {
                    parseFn(response, function (data) {
                        if (angular.isArray(data)) {
                            result = [];
                            for (var i = 0; i < data.length; i++) {
                                result.push(new Resource(data[i]));
                            }
                        }
                        else {
                            result = new Resource(data);
                        }
                    });
                    scb(result, response.status, response.headers, response.config);
                }
                return result;
            }, function (response) {
                ecb(undefined, response.status, response.headers, response.config);
                return undefined;
            });
        };

        var Resource = function (data) {
            angular.extend(this, data);
        };

        Resource.defaultParseFn = function (response, parsecb) {
            parsecb(response.data);
        };

        Resource.error = function (msg) {
            angular.showMessage(msg);
        };

        Resource.query = function (config) {
            //config.scb({ Data: { PicId: 0 }, Items: [] });
            
            var httpPromise = $http.post(config.url, config.data);
            return thenFactoryMethod(httpPromise, config.scb, config.ecb, config.pfn, false);
            
        };

        Resource.querylist = function (config) {
            var cache = $dataCache.getListCache(config.cacheKey);
            var loading = cache.get('loading');
            if (loading)
                return;
            var cacheData = cache.get('data');

            config.data = config.data || {};
            config.data.pageSize = config.pagesize || $pagination.pagesize;

            if (cacheData) {
                if (config.getMore)
                {
                    config.data.pageIndex = ++cacheData.pageindex;
                }
                else if (config.refresh) {
                    cacheData.items = [];
                    cacheData.pageindex = $pagination.pageindex;

                    config.data.pageIndex = $pagination.pageindex;
                }
                else {
                    typeof config.scb == 'function' && config.scb.apply(this, [{ Items: cacheData.items, hasMore: cacheData.hasMore }]);
                    return;
                }
            }
            else {
                cacheData = { items: [], pageindex: $pagination.pageindex, hasMore: false };
                cache.put('data', cacheData);
                config.data.pageIndex = $pagination.pageindex;
            }
            var scb = config.scb;
            config.scb = function (data) {
                cache.put('loading', false);
                if (angular.isArray(data.Data.Items)) {
                    if (cacheData.items.length) {
                        var last = cacheData.items[cacheData.items.length - 1];
                        var first = data.Data.Items[0];
                        if (angular.equals(last, first))
                        {
                            cacheData.items.pop();
                        }
                    }
                    angular.forEach(data.Data.Items, function (value) {
                        value.uniqueId = 'uid_' + (Math.random() + '').replace('.', '');
                        cacheData.items.push(value);
                    });
                    if (data.Data.Items.length == config.data.pageSize) {
                        cacheData.hasMore = true;
                        data.hasMore = true;
                    }
                }
                data.Data = null;
                data.Items = cacheData.items;
                typeof scb == 'function' && scb.apply(this, arguments);
            }
            cache.put('loading', true);
            return Resource.query(config);
        };

        Resource.cache = function (key, value) {
            var global = $dataCache.getCache('globalCacheKey');
            if (typeof value != "undefined") {
                global.put(key, value);
            }
            else {
                return global.get(key);
            }
        };

        return Resource;
    }

    return ServerModelsFactory;
}]);
