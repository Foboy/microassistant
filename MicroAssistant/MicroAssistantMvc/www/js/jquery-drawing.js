(function ($) {
    function f_getrandombyrange(imin, imax) {
        return Math.floor(Math.random() * (1 + imax - imin) + imin);
    }
    $.fn.KolaLineDraw = function (action) {
        var options = $.extend({}, $.fn.KolaLineDraw.defaults, action);
        var thisobj = $(this);
        $(this).css("padding", "0px");
        function f_draw() {
            var bcreate = false;
            if (thisobj.children().length == 0) {
                bcreate = true;
            } else {
                if (options.EraseOld == true) {
                    if ($.browser.msie) {
                        thisobj.find("group").remove();
                    } else {
                        thisobj.find("canvas").remove();
                    }

                    bcreate = true;
                }
            }
            var id = "draw_" + f_getrandombyrange(100000, 999999);
            if ($.browser.msie) {
                //ie use VML
                if (thisobj.find("group").length == 0) {
                    thisobj.append($("<v:group id='" + id + "' style='position:relative;width:" + options.Width + "px;height:" + options.Height + "px;' CoordOrig='0,0' CoordSize = '" + options.Width + "," + options.Height + "'></v:group>"));
                } else {
                    id = thisobj.find("group").first().attr("id");
                }
                var vobj = document_createElement_x_x("<v:line style='position:relative' from='" + options.StartPointLeft + "," + options.StartPointTop + "' to='" + options.EndPointLeft + "," + options.EndPointTop + "' strokecolor='" + options.LineColor + "' strokeweight='" + options.LineWidth + "px'></v:line>");
                document.getElementById(id).insertBefore(vobj);
            } else {
                //firefox chrome use canvas
                if (thisobj.find("canvas").length == 0) {
                    thisobj.append($("<canvas id='" + id + "' width='" + options.Width + "' height='" + options.Height + "'></canvas>"));
                } else {
                    id = thisobj.find("canvas").attr("id");
                }
                var canvas = document.getElementById(id);
                var ctx = canvas.getContext("2d");
                try {
                    ctx.strokeStyle = options.LineColor;
                    ctx.lineWidth = options.LineWidth;
                    ctx.beginPath();
                    ctx.moveTo(options.StartPointLeft, options.StartPointTop);
                    ctx.lineTo(options.EndPointLeft, options.EndPointTop);
                } catch (Error) {
                    alert(Error);
                } finally {
                    ctx.closePath();
                    ctx.stroke();
                }
            }
            return id;
        }
        return f_draw();
    };

    $.fn.KolaClearDraw = function () {
        if ($.browser.msie) {
            $(this).find("group").remove();
        } else {
            $(this).find("canvas").remove();
        }
    }

    $.fn.KolaRectDraw = function (action) {
        var options = $.extend({}, $.fn.KolaRectDraw.defaults, action);
        var thisobj = $(this);
        $(this).css("padding", "0px");
        function f_draw() {
            var bcreate = false;
            if (thisobj.children().length == 0) {
                bcreate = true;
            } else {
                if (options.EraseOld == true) {
                    if ($.browser.msie) {
                        thisobj.find("group").remove();
                    } else {
                        thisobj.find("canvas").remove();
                    }

                    bcreate = true;
                }
            }
            var id = "draw_" + f_getrandombyrange(100000, 999999);
            if ($.browser.msie) {
                //ie use VML
                if (thisobj.find("group").length == 0) {
                    thisobj.append($("<v:group id='" + id + "' style='position:relative;width:" + options.Width + "px;height:" + options.Height + "px;' CoordOrig='0,0' CoordSize = '" + options.Width + "," + options.Height + "'></v:group>"));
                } else {
                    id = thisobj.find("group").first().attr("id");
                }
                var pos = new Array();
                var co1 = { x: options.Left, y: options.Top };
                pos.push(co1);
                var co2 = { x: options.Left, y: Number(options.Top) + Number(options.RectHeight) };
                pos.push(co2);
                var co3 = { x: Number(options.Left) + Number(options.RectWidth), y: Number(options.Top) + Number(options.RectHeight) };
                pos.push(co3);
                var co4 = { x: Number(options.Left) + Number(options.RectWidth), y: options.Top };
                pos.push(co4);

                var points = "";
                for (var i = 0; i < pos.length; i++) {
                    if (i == 0) {
                        points = pos[i].x + "," + pos[i].y;
                    } else {
                        points += " " + pos[i].x + "," + pos[i].y;
                    }
                }
                var vobj = document_createElement("<v:PolyLine style='position:relative' Points='" + points + " ' strokecolor='" + options.LineColor + "' strokeweight='" + options.LineWidth + "px'  fillColor='" + options.FillColor + "'></v:PolyLine>");
                document.getElementById(id).insertBefore(vobj);
            } else {
                //firefox chrome use canvas
                if (thisobj.find("canvas").length == 0) {
                    thisobj.append($("<canvas id='" + id + "' width='" + options.Width + "' height='" + options.Height + "'></canvas>"));
                } else {
                    id = thisobj.find("canvas").attr("id");
                }
                var canvas = document.getElementById(id);
                var ctx = canvas.getContext("2d");
                try {
                    ctx.strokeStyle = options.LineColor;
                    ctx.fillStyle = options.FillColor;
                    ctx.lineWidth = options.LineWidth;
                    ctx.beginPath();
                    ctx.fillRect(options.Left, options.Top, options.RectWidth, options.RectHeight);
                } catch (Error) {
                    alert(Error);
                } finally {
                    ctx.closePath();
                    ctx.fill();
                }
            }
            return id;
        }
        return f_draw();
    };

    // Rect Draw Default Properties and Events
    $.fn.KolaRectDraw.defaults = {
        Width: 640,  // canvas width default 640 px
        Height: 640, // canvas height default 500 px
        LineWidth: 1, // 线宽度 必须为正数 1表示1个宽度单位
        LineColor: "#FFFFFF", //线颜色
        Left: 0, //rect left postion default 0px
        Top: 0, //rect top postion default 0px
        RectWidth: 640, //rect width default 500px
        RectHeight: 640, //rect height default 500px
        EraseOld: false, // judge erase old image before draw new
        FillColor: "#99CCFF" //Rect fill color
    };

    // Line Draw Default Properties and Events
    $.fn.KolaLineDraw.defaults = {
        Width: 640,  // canvas width default 640 px
        Height: 640, // canvas height default 500 px
        LineWidth: 1, // 线宽度 必须为正数 1表示1个宽度单位
        LineColor: "#000000", //线颜色
        StartPointLeft: 0, //start point left postion default 0px
        StartPointTop: 0, //start point top postion default 0px
        EndPointLeft: 640, //end point left postion default 500px
        EndPointTop: 640, //end point top postion default 500px
        EraseOld: false // judge erase old image before draw new
    };

})(jQuery);