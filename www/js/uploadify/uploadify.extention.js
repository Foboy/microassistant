
/*
创 建 人     ： 陈 锐
创 建 时 间  ： 2013.09.21
功 能 说 明  ： 图片上传控件

*/
(function ($) {
    $.fn.cuploadify = function (options) {
        var me = $(this);
		var uid = me.attr('id') +"_cu";
        var mwidth = options.width || me.width();
        var mheight = options.height || me.height();
        var timer;
        me.css({"position":"relative","overflow":"hidden"});
        var wraper = $("<div style='position:absolute;left:0;top:0;z-index:99'></div>");
        me.append(wraper);

        var file = $("<input type='file' style='width:" + mwidth + "px;height:" + mheight + "px;cursor:pointer;' id='"+uid+"' />");
        wraper.append(file);
		
		options = $.extend({
            height: mheight,
            width: mwidth
        }, options);
		
		file.uploadify(options);
		
		me.find(".swfupload").css({left:0,top:0});
		me.find(".uploadify-button").remove();
    }
})(jQuery);