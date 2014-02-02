/*
    create by cray(285025600@qq.com)
    create time 2013/12/16
*/
(function (lng, undefined) {
    //修正屏幕宽度大于768px,aside异常
    //全部设备都当做Phone处理
    lng.Constants.DEVICE.TABLET = lng.Constants.DEVICE.TV = lng.Constants.DEVICE.PHONE;

    //自定义aside宽度
    lng.Aside.Width = 80;
    lng.Aside.MaxWidth = Math.round(lng.Aside.Width * 1.1);

    var createTextNode = document.createTextNode;
    document.createTextNode = function () {
        var args = [];
        if (arguments.length) {
            args = Array.prototype.slice.call(arguments);
            for (var i = 0; i < args.length; i++) {
                var arg = args[i];
                if (typeof arg == 'string' && arg.indexOf('__customKFShow__') >= 0) {
                    while (arg.indexOf("translateX(262px)") >= 0)
                        arg = arg.replace("translateX(262px)", "translateX(" + lng.Aside.MaxWidth + "px)");
                    while (arg.indexOf("translateX(256px)") >= 0)
                        arg = arg.replace("translateX(256px)", "translateX(" + lng.Aside.Width + "px)");
                    args[i] = arg;
                }
            }
        }
        return createTextNode.apply(this, args);
    };

    var customAsideAnimation;

    var createCustomAsideAnimation = function (animationName, maxTranslateX, translateX) {
        var isMoz = lng.Core.environment().browser.match(/mozilla|firefox/gi) ? true : false;
        var _kfPrefix = isMoz ? '' : '-webkit-';
        var _customAsideAnimation = customAsideAnimation || document.createElement('style');
        customAsideAnimation = _customAsideAnimation;
        _customAsideAnimation.type = 'text/css';
        rule = " \
        body > aside{width: #{TranslateX}px;} \
        body > section.aside{#{_kfPrefix}transform:translateX(#{TranslateX}px);} \
        body > section.aside-right{#{_kfPrefix}transform:translateX(-#{TranslateX}px);} \
        @#{_kfPrefix}keyframes #{animationName}LeftShow { \
            0%   { #{_kfPrefix}transform: translateX(0); } \
            60%  { #{_kfPrefix}transform: translateX(#{MaxTranslateX}px);  } \
            100% { #{_kfPrefix}transform: translateX(#{TranslateX}px);  } \
        } \
        @#{_kfPrefix}keyframes #{animationName}LeftHide { \
            0%   { #{_kfPrefix}transform: translateX(#{TranslateX}px); } \
            100% { #{_kfPrefix}transform: translateX(0);      } \
        } \
        @#{_kfPrefix}keyframes #{animationName}RightShow { \
            0%   { #{_kfPrefix}transform: translateX(0); } \
            60%  { #{_kfPrefix}transform: translateX(-#{MaxTranslateX}px);  } \
            100% { #{_kfPrefix}transform: translateX(-#{TranslateX}px);  } \
        } \
        @#{_kfPrefix}keyframes #{animationName}RightHide { \
            0%   { #{_kfPrefix}transform: translateX(-#{TranslateX}px); } \
            100% { #{_kfPrefix}transform: translateX(0);      } \
        } \
        ";
        while (rule.indexOf("#{_kfPrefix}") >= 0) 
            rule = rule.replace("#{_kfPrefix}", _kfPrefix);
        while (rule.indexOf("#{animationName}") >= 0)
            rule = rule.replace("#{animationName}", animationName);
        while (rule.indexOf("#{MaxTranslateX}") >= 0)
            rule = rule.replace("#{MaxTranslateX}", maxTranslateX);
        while (rule.indexOf("#{TranslateX}") >= 0)
            rule = rule.replace("#{TranslateX}", translateX);
        if (_customAsideAnimation.firstChild)
            _customAsideAnimation.removeChild(_customAsideAnimation.firstChild);
        _customAsideAnimation.appendChild(document.createTextNode(rule));
        document.getElementsByTagName("head")[0].appendChild(_customAsideAnimation);
        return _customAsideAnimation;
    };
    lng.ready(function () {
        createCustomAsideAnimation("sectionAside", lng.Aside.MaxWidth, lng.Aside.Width);
    });

})(Lungo);
//用quo代替jquery
var $ = $ || $$;
