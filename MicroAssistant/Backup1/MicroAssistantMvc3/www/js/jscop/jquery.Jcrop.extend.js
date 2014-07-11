/*
 *create by cray 
 *2013/11/07
*/
(function ($) {
    $.extend($.fn, {
        photoCutter: function (option, callback) {
            $(this).each(function () {
                var settings = jQuery.extend({
                    id: jQuery(this).attr("id"),
                    ctype: "veh",
                    photoid: "#target1",
                    previewid: "#preview",
                    bigbtnid: "#bigbtn",
                    smallbtnid: "#smallbtn",
                    picbigbtnid: "#picbigbtn",
                    picsmallbtnid: "#picsmallbtn",
                    allsliderid: "#allslide",
                    picsliderid: "#picslide",
                    cutbtnid: "#cutphotobtn",
                    savebtnid: "#savephotobtn",
                    src: "",
                    photoWidth: 150,
                    photoHeight: 100,
                    JcropOptionFunc: null,
                    defaultsrc: "/cray/images/white.gif",
                    cutPhoto: function (url, cutdata, savesize) { },
                    savePhoto: function (url, cutdata, savesize) { }
                }, option);
                constructCutter(this, settings);
            });
            function constructCutter(form, settings) {

                settings.cutdata = { x: 0, y: 0, w: 0, h: 0 };

                var $form = $(form);
                var $photo = $(form).find(settings.photoid);
                var $cutbtn = $(settings.cutbtnid);
                var $savebtn = $(settings.savebtnid);
                var $photoHolder = $photo.parent();
                var $preview = $(settings.previewid);
                var $previewHolder = $preview.parent();

                var $bigBtn = $(settings.bigbtnid);
                var $smallBtn = $(settings.smallbtnid);
                var $picBigBtn = $(settings.picbigbtnid);
                var $picSmallBtn = $(settings.picsmallbtnid);
                var $allslide = $(settings.allsliderid);
                var $picslide = $(settings.picsliderid);

                function destroy() {
                    if ($photo.data("Jcrop")) {
                        $photo.data("Jcrop").destroy();
                    }
                    else {
                        return;
                    }
                    $bigBtn.unbind("click");
                    $smallBtn.unbind("click");
                    $picBigBtn.unbind("click");
                    $picSmallBtn.unbind("click");
                    $preview.unbind("click");
                    $cutbtn.unbind("click");
                    $savebtn.unbind("click");
                    $photo.attr("src", settings.defaultsrc);
                    $preview.attr("src", settings.defaultsrc);
                }
                destroy();


                var widthScale = settings.photoWidth / $form.width();
                var heightScale = settings.photoHeight / $form.height();
                var joption;
                if (typeof settings.JcropOptionFunc == "function") {
                    joption = settings.JcropOptionFunc(settings.ctype);
                }
                else {
                    joption = GetOption(settings.ctype);
                }
                var boundx = 0, boundy = 0;
                var japi;

                $("#" + settings.id + "_hidHoler").remove();
                var $hidHoler = $('<div id="' + settings.id + '_hidHoler" style="display:none;"></div>');
                $(document.body).append($hidHoler);
                var $popPreviewHolder = $('<div id="' + settings.id + '_hidPreview" style="overflow:hidden;border:solid 1px #000;"></div>');
                $hidHoler.append($popPreviewHolder);
                $popPreviewHolder.css({ width: joption.fullview[0] + "px", height: joption.fullview[1] + "px" });

                var $popPreview = $("<img />");
                $popPreviewHolder.append($popPreview);


                $photo.attr("src", settings.src);
                $preview.attr("src", settings.src);
                $popPreview.attr("src", settings.src);
                $previewHolder.css({ width: (joption.presize[0] + 2) + "px", height: (joption.presize[1] + 2) + "px" });
                if (widthScale > 1 || heightScale > 1) {
                    if (widthScale > heightScale) {
                        $photo.css({ width: ($form.width() - 4) + "px", height: parseInt(settings.photoHeight / widthScale) + "px" });
                    } else {
                        $photo.css({ height: ($form.height() - 4) + "px", width: parseInt(settings.photoWidth / heightScale) + "px" });
                    }
                }
                else {
                    $photo.css({ width: "auto", height: "auto" });
                }

                $photo.Jcrop({
                    allowSelect: false,
                    onChange: updatePreview,
                    onSelect: updatePreview,
                    setSelect: joption.select,
                    maxSize: joption.minsize,
                    minSize: joption.maxsize,
                    aspectRatio: joption.aspectRatio
                }, function () {
                    // Use the API to get the real image size
                    var bounds = this.getBounds();
                    boundx = bounds[0];
                    boundy = bounds[1];
                    // Store the API in the jcrop_api variable
                    //jcrop_api = this;
                    japi = inited.call(this);
                });

                function updatePreview(c) {
                    var scale;
                    var swidth = settings.photoWidth;
                    var sheight = settings.photoHeight;
                    var presize = joption.presize;
                    var fullview = joption.fullview;
                    if (typeof japi === "object") {
                        scale = japi.scale;
                    }
                    else {
                        scale = $photo.width() / settings.photoWidth;
                    }
                    if (parseInt(c.w) > 0) {
                        c.w = Math.round(c.w / scale);
                        c.h = Math.round(c.h / scale);
                        c.x = Math.round(c.x / scale);
                        c.y = Math.round(c.y / scale);

                        var rx = presize[0] / c.w;
                        var ry = presize[1] / c.h;

                        $preview.css({
                            width: Math.round(rx * swidth) + 'px',
                            height: Math.round(ry * sheight) + 'px',
                            marginLeft: '-' + Math.round(rx * c.x) + 'px',
                            marginTop: '-' + Math.round(ry * c.y) + 'px'
                        });

                        rx = fullview[0] / c.w;
                        ry = fullview[1] / c.h;
                        $popPreview.css({
                            width: Math.round(rx * swidth) + 'px',
                            height: Math.round(ry * sheight) + 'px',
                            marginLeft: '-' + Math.round(rx * c.x) + 'px',
                            marginTop: '-' + Math.round(ry * c.y) + 'px'
                        });
                    }
                    settings.cutdata.x = c.x;
                    settings.cutdata.y = c.y;
                    settings.cutdata.w = c.w;
                    settings.cutdata.h = c.h;
                };

                function inited() {

                    this.sourceWidth = settings.photoWidth;
                    this.sourceHeight = settings.photoHeight;
                    this.scale = $photo.width() / this.sourceWidth;
                    var src = $photo.attr("src");
                    var zoomspeed = 0.1;
                    var me = this;
                    var sss = settings;
                    var oldScale = me.scale;


                    //计算当前缩放比例
                    var computeScale = function () {
                        var bounds = me.getWidgetSize();
                        me.scale = bounds[0] / me.sourceWidth;
                        oldScale = me.scale;
                    };

                    //重设图片大小，选区大小
                    var resizeAll = function () {
                        me.scale = (Math.round(me.scale * 100) / 100);
                        var select = me.tellSelect();
                        select.x = select.x / oldScale;
                        select.y = select.y / oldScale;
                        select.x2 = select.x2 / oldScale;
                        select.y2 = select.y2 / oldScale;
                        select.x = Math.round(select.x * me.scale);
                        select.y = Math.round(select.y * me.scale);
                        select.x2 = Math.round(select.x2 * me.scale);
                        select.y2 = Math.round(select.y2 * me.scale);
                        var newWidth = Math.round(me.sourceWidth * me.scale);
                        var newHeight = Math.round(me.sourceHeight * me.scale);
                        me.setImageAndSize(src, newWidth, newHeight, function () { });
                        me.setSelect([select.x, select.y, select.x2, select.y2]);
                        showScale(newWidth, newHeight);
                    };

                    //重设图片大小
                    var resizePicture = function () {
                        me.scale = (Math.round(me.scale * 100) / 100);
                        var select = me.tellSelect();
                        var newWidth = Math.round(me.sourceWidth * me.scale);
                        var newHeight = Math.round(me.sourceHeight * me.scale);
                        if (select.x2 > newWidth) {
                            var overWidth = select.x2 - newWidth;
                            if (parseInt(select.x, 10) >= overWidth) {
                                select.x = select.x - overWidth;
                                select.x2 = select.x2 - overWidth;
                            }
                            else {
                                me.scale = oldScale;
                                showScale(newWidth, newHeight);
                                return;
                            }
                        }
                        if (select.y2 > newHeight) {
                            var overHeight = select.y2 - newHeight;
                            if (parseInt(select.y, 10) >= overHeight) {
                                select.y = select.y - overHeight;
                                select.y2 = select.y2 - overHeight;
                            }
                            else {
                                me.scale = oldScale;
                                showScale(newWidth, newHeight);
                                return;
                            }
                        }
                        me.setImageAndSize(src, newWidth, newHeight, function () { });
                        me.setSelect([select.x, select.y, select.x2, select.y2]);
                        showScale(newWidth, newHeight);
                    };

                    this.setScale = function (slideValue) {
                        computeScale();
                        var scale = 1;
                        scale += slideValue / 100;
                        me.scale = scale;
                        resizeAll();
                    };

                    this.setPicScale = function (slideValue) {
                        computeScale();
                        var scale = 1;
                        scale += slideValue / 100;
                        me.scale = scale;
                        resizePicture();
                    };

                    var showScale = function (newWidth, newHeight) {
                        $allslide.slider("value", (me.scale * 100 - 100));
                        $picslide.slider("value", (me.scale * 100 - 100));
                        $photoHolder.css({ "width": newWidth + "px", "height": newHeight + "px" });
                        if (newHeight * 1 < $form.height() * 1) {
                            var margintop = Math.round(($form.height() - newHeight) / 2);
                            $photoHolder.css("margin", margintop + "px auto");
                        }
                    };

                    var Biger = function () {
                        computeScale();
                        me.scale += zoomspeed;
                        if (me.scale >= 2) {
                            return;
                        }
                        resizeAll();
                    };
                    var Smaller = function () {
                        computeScale();
                        if (me.scale > zoomspeed) {
                            me.scale -= zoomspeed;
                        }
                        if (me.scale <= 0) {
                            return;
                        }
                        resizeAll();
                    };

                    var PicBiger = function () {
                        computeScale();
                        me.scale += zoomspeed;
                        if (me.scale >= 2) {
                            return;
                        }
                        resizePicture();
                    };
                    var PicSmaller = function () {
                        computeScale();
                        if (me.scale > zoomspeed) {
                            me.scale -= zoomspeed;
                        }
                        if (me.scale <= 0) {
                            return;
                        }
                        resizePicture();
                    };
                    var ShowPreview = function () {
                        var $templink = $('<a href="#' + settings.id + '_hidPreview"></a>');
                        $(document.body).append($templink);
                        $templink.facebox();
                        $templink.trigger("click");
                        $templink.remove();
                    }
                    //
                    $allslide.slider({
                        value: 0,
                        min: -90,
                        max: 90,
                        step: 10,
                        slide: function (event, ui) {
                            return false;
                        }
                    });
                    $picslide.slider({
                        value: 0,
                        min: -90,
                        max: 90,
                        step: 10,
                        slide: function (event, ui) {
                            return false;
                        }
                    });
                    //移除绑定
                    $bigBtn.unbind("click");
                    $smallBtn.unbind("click");
                    $picBigBtn.unbind("click");
                    $picSmallBtn.unbind("click");
                    $preview.unbind("click");
                    $cutbtn.unbind("click");
                    $savebtn.unbind("click");

                    //绑定事件
                    $bigBtn.bind("click", Biger);
                    $smallBtn.bind("click", Smaller);
                    $picBigBtn.bind("click", PicBiger);
                    $picSmallBtn.bind("click", PicSmaller);
                    $preview.bind("click", ShowPreview);
                    $cutbtn.bind("click", function () {
                        if (typeof settings.cutPhoto == "function") {
                            settings.cutPhoto(settings.src, settings.cutdata, joption.savesize);
                        }
                    });
                    $savebtn.bind("click", function () {
                        if (typeof settings.savePhoto == "function") {
                            settings.savePhoto(settings.src, settings.cutdata, joption.savesize);
                        }
                    });
                    showScale($photo.width(), $photo.height());
                    if (typeof callback == "function") {
                        callback.call(this);
                    }
                    return this;
                }

                var pcapi = { destroy: destroy };
                $(form).data("photoCutter", pcapi);
                return pcapi;
            }
            function GetOption(ctype) {
                var aspectRatio = 1212 / 827;
                var select = [0, 0, 1212, 827];
                var minsize = [0, 0];
                var maxsize = [0, 0];
                var presize = [303, 207];
                var fullview = [1212, 827];
                var savesize = [1212, 827];
                switch (ctype) {
                    case "veh":
                        aspectRatio = 1212 / 827;
                        select = [0, 0, 1212, 827];
                        minsize = [0, 0];
                        maxsize = [0, 0];
                        presize = [303, 207];
                        fullview = [1212, 827];
                        savesize = [1212, 827];
                        break;
                    case "drv":
                        aspectRatio = 358 / 441;
                        select = [0, 0, 358, 441];
                        minsize = [0, 0];
                        maxsize = [0, 0];
                        presize = [286, 353];
                        fullview = [358, 441];
                        savesize = [358, 441];
                        break;
                }
                return {
                    aspectRatio: aspectRatio,
                    select: select,
                    minsize: minsize,
                    maxsize: maxsize,
                    presize: presize,
                    fullview: fullview,
                    savesize: savesize
                };
            }
        },
        PhotoCutter_Destroy: function () {
            if (typeof $(this).data("photoCutter") == "object") {
                return $(this).data("photoCutter").destroy();
            }
            else {
                return null;
            }
        }
    });
})(jQuery);