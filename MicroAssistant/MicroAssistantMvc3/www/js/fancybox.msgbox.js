(function ($) {
    $.showMsg = function (msg, type, options) {
        //type: warn,error,success,none
        var options = $.extend({
            closeTimeout: 3000,
            onClosed: function () { }
        }, options);
        var msgboxspan = $("#fancybox_msgbox_span");
        if (!msgboxspan.length) {
            $(document.body).append("<div style='display:none;'>\
                         <div class='msgbox' id='fancybox_msgbox'>\
                             <span class='msgbg msgbox_layer'>\
                               <span class='msgbg gtl_ico_succ' id='fancybox_msgbox_icon'></span><span id='fancybox_msgbox_span'>保存成功</span><span class='msgbg gtl_end'></span>\
                             </span>\
                         </div>\
                     </div>");
            msgboxspan = $("#fancybox_msgbox_span");
        }
        var msgicon = $("#fancybox_msgbox_icon");
        msgicon.removeClass();
        msgicon.addClass("msgbg");
        switch (type) {
            case "s":
            case "succ":
            case "success":
                msgicon.addClass("gtl_ico_succ");
                break;
            case "w":
            case "warn":
                msgicon.addClass("gtl_ico_warn");
                break;
            case "e":
            case "error":
                msgicon.addClass("gtl_ico_error");
                break;
            default:
                msgicon.addClass("gtl_ico_error");
                break;
        }
        msgboxspan.html(msg);
        $.fancybox.open($('#fancybox_msgbox'), {
            'closeBtn': false,
            tpl: {
                wrap: '<div class="fancybox-wrap" tabIndex="-1"><div class="fancybox-outer"><div class="fancybox-inner"></div></div></div>'
            },
            helpers: {
                overlay: null
            }
        })
        if (options.closeTimeout > 0) {
            setTimeout(function () {
                $.fancybox.close();
                if (typeof options.onClosed == "function") {
                    options.onClosed();
                }
            }, options.closeTimeout);
        }
    };
    $.showLoading = function () {
        $.fancybox.showLoading();
    };
    $.pagePreLoading = function (url, loaded) {
        var loader = $('<iframe style="display:none"></iframe>');
        var loadingwrap = $('<div class="pagepreloadingbox" style="width:220px;"><p>正在跳转...</p><br /><div style="width:200px;text-align:center;margin:20px auto;" class="perLoading"><span style="font-size:80px;float: none;margin-left: none;" class="loadingspan">0</span>%</div><div style="height:3px;background:#1ABC9C;width:0%;margin-top:10px;" class="loadingProcess"></div></div>');
        $(document.body).append(loader);
        $(document.body).append(loadingwrap);
        var loadingSpan = loadingwrap.find(".loadingspan");
        var loadingProcess = loadingwrap.find(".loadingProcess");

        var timeOut = 100;
        var percent = 0;

        loader.load(function () {
            timeOut = 10;
        });

        var process = function () {
            loadingSpan.html(percent);
            loadingProcess.css({ width: percent + '%' });
            if (timeOut >= 100) {
                timeOut += 100;
            }
            percent++;
            if (percent < 100)
                setTimeout(function () { process() }, timeOut);
            else
            {
                if (typeof loaded == 'function')
                    loaded();
                //$.fancybox.close();
            }
        };
        loader.attr("src", url);
        $.fancybox.open(loadingwrap, {
            'closeBtn': false,
            helpers: {
                overlay: null
            }
        })
        process();
    }
    $.showConfirmMsg = function (msg, callback, options) {
        //callback: 参数为bool类型
        options = $.extend({
            onClosed: function () { }
        }, options);
        var link = $("a#fancyboxlink");
        var msgboxspan = $("#fancybox_confirmmsgbox_span");
        if (!msgboxspan.length) {
            $(document.body).append("<div style='display:none;'>\
                         <div class='fancybox-popup' id='fancybox_confirmmsgbox'>\
	<p class='textDes' id='fancybox_confirmmsgbox_span'>成长录</p>\
    <div class='popupBtn'>\
        <a class='button-green' id='confirm_true' href='javascript:void(0);' >确定</a>\
        <a class='button-green' id='confirm_false' href='javascript:void(0);'>取消</a>\
    </div>\
</div>\
                     </div>");
            msgboxspan = $("#fancybox_confirmmsgbox_span");
        }
      
        $("#confirm_true").bind("click", function () {
                  callback(true);
                  $.fancybox.close();

            });
        $("#confirm_false").bind("click", function () {
                 callback(false);
                 $.fancybox.close();
            });

        if (!link.length) {
            link = $("<a id='fancyboxlink'></a>");
            $(document.body).append(link);
        }
        link.fancybox({
            'showCloseButton': false,
            'overlayShow': false,
            'transitionOut': true,
            'hideOnOverlayClick': true,
            'hideOnContentClick': true
        });
        msgboxspan.html(msg);
        link.attr("href", "#fancybox_confirmmsgbox").click();
      
    };

    $.fn.modal = function (flag, options) {
        var options = $.extend({ 'closeBtn': false, padding: 0, margin: 0 }, options);
        if (flag == 'show') {
            $(this).removeClass('modal').removeClass('fade');
            $(this).find('.modal-body').css({ padding: 0, margin: 0,'overflow-x':'hidden' });
            $(this).find('.close').unbind('click');
            $(this).find('.close').click(function () {
                $.fancybox.close();
            });
            $.fancybox.open($(this), options);
        }
        else {
            $.fancybox.close();
        }
    }

})(jQuery);

var oldAlert = alert;
alert = window.alert = jQuery.showMsg;
