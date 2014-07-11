/*
    create by cray(285025600@qq.com)
    create time 2013/12/16
*/
(function (lng, angular, undefined) {
    var NG_FLAG = "angular-element";
    var load = lng.Resource.load;
    var init = lng.Boot.Data.init;
    var loadedList = [];
    var initedList = [];
    lng.Boot.Data.init = function () {
        var id = angular.lowercase(arguments[0]);
        for (var i = 0; i < initedList.length; i++) {
            if (id == initedList[i]) {
                return;
            }
        }
        initedList.push(id);
        init.apply(this, arguments);
    };
    lng.Resource.load = function () {
        var src = angular.lowercase(arguments[0]);
        for (var i = 0; i < loadedList.length; i++)
        {
            if (src == loadedList[i])
            {
                return;
            }
        }
        loadedList.push(src);
        load.apply(this, arguments);
        lng.ready(function () {
            lng.dom('section').each(function () {
                var el = lng.dom(this);
                if (el.attr(NG_FLAG)) {
                    el.find("article").each(function () {
                        var article = lng.dom(this);

                        if (!article.attr(NG_FLAG)) {
                            console.log('compile article' + article.attr('id'));
                            angular.element(document).injector().invoke(function ($compile) {
                                var scope = angular.element(article[0]).scope();
                                $compile(article[0])(scope);
                            });
                        }
                        article.attr(NG_FLAG, true);
                    });
                }
                else {
                    console.log('compile section' + el.attr('id'));
                    console.log(el);
                    angular.element(document).injector().invoke(function ($compile) {
                        var scope = angular.element(el[0]).scope();
                        $compile(el[0])(scope);
                    });
                    el.find("article").each(function () {
                        var article = lng.dom(this);
                        article.attr(NG_FLAG, true);
                    });
                    el.attr(NG_FLAG, true);

                }
            });
        });
    };

    var _alert = alert;
    alert = function (msg, type, options) {
        //type: warn,error,success,none
        var options = options || {};
        options.closeTimeout = options.closeTimeout || 3000;
        var icon;
        switch (type) {
            case "s":
            case "succ":
            case "success":
                icon = "check";
                break;
            case "w":
            case "warn":
                icon = "cancel";
                break;
            case "e":
            case "error":
                icon = "cancel";
                break;
            default:
                icon = "cancel";
                break;
        }

        lng.Notification.show(
            icon,                //Icon
            msg,              //Title
            options.closeTimeout / 1000,                      //Seconds
            options.onClosed       //Callback function
        );
    };

    angular.section = function () {
        Lungo.Router.section.apply(null, arguments);
    };
    angular.showLoading = function () {
        Lungo.Notification.show();
    };
    angular.hideLoading = function () {
        Lungo.Notification.hide();
    };
    angular.showMessage = function () {
        alert.apply(null, arguments);
    }; 
    angular.environment = lng.Core.environment();
})(Lungo, angular);