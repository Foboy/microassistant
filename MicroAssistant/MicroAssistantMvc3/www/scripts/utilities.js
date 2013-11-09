var utilities = utilities || {};

utilities.getquerystring = function(para) {
	var reg = new RegExp("(^|&|\\?)" + para + "=([^&]*)(&|$)"), r;
	if (r = window.location.href.match(reg)) return unescape(r[2]); return null;
};

var docWrite = document.write;
utilities.hookwrite = function(){
	document.write = function (text){
		var scripttest = /src="([\s\S]*?)"/i;
		if(scripttest.test(text))
		{
			var url = text.match( /src="([\s\S]*?)"/i )[1];
			utilities.loadscript(url);
		}
	};
}
utilities.loadscript = function(url, loadedCallback){
	var script = document.createElement( 'script' ), head = document.head || document.getElementsByTagName( 'head' )[0] || document.documentElement;
	
	script.type = 'text/javascript';
	script.src = url;
	//script.onerror = 
	script.onload = 
	//script.onreadystatechange = 
	function( e ){
		e = e || window.event;
		console.log(e);
		/*
		if( !script.readyState || /loaded|complete/.test(script.readyState) || e === 'error'){
			head.removeChild( script );
			head = 			
			script = 
			script.onload = null;
		}*/
		if(typeof loadedCallback == "function")
		{
			loadedCallback();
		}
	}
	
	// 加载script
	head.insertBefore( script, head.firstChild );
		
};

utilities.registeriframelistener = function( key, callback){
	window.top['iframeevent_'+key] = callback;
};

utilities.fireiframelistener = function(){
	var args = Array.prototype.slice.call(arguments);
	var key = args.shift();
	var callback = window.top['iframeevent_'+key];
	if(typeof callback == 'function')
	{
		callback.apply(null,args);
	}
};

utilities.mvcParamMatch = (function () {
    var MvcParameterAdaptive = {};
    //验证是否为数组  
    MvcParameterAdaptive.isArray = Function.isArray || function (o) {
        return typeof o === "object" &&
                Object.prototype.toString.call(o) === "[object Array]";
    };

    //将数组转换为对象  
    MvcParameterAdaptive.convertArrayToObject = function (/*数组名*/arrName, /*待转换的数组*/array, /*转换后存放的对象，不用输入*/saveOjb) {
        var obj = saveOjb || {};

        function func(name, arr) {
            for (var i in arr) {
                if (!MvcParameterAdaptive.isArray(arr[i]) && typeof arr[i] === "object") {
                    for (var j in arr[i]) {
                        if (MvcParameterAdaptive.isArray(arr[i][j])) {
                            func(name + "[" + i + "]." + j, arr[i][j]);
                        } else if (typeof arr[i][j] === "object") {
                            MvcParameterAdaptive.convertObject(name + "[" + i + "]." + j + ".", arr[i][j], obj);
                        } else {
                            obj[name + "[" + i + "]." + j] = arr[i][j];
                        }
                    }
                } else {
                    obj[name + "[" + i + "]"] = arr[i];
                }
            }
        }

        func(arrName, array);

        return obj;
    };

    //转换对象  
    MvcParameterAdaptive.convertObject = function (/*对象名*/objName,/*待转换的对象*/turnObj, /*转换后存放的对象，不用输入*/saveOjb) {
        var obj = saveOjb || {};

        function func(name, tobj) {
            for (var i in tobj) {
                if (MvcParameterAdaptive.isArray(tobj[i])) {
                    MvcParameterAdaptive.convertArrayToObject(i, tobj[i], obj);
                } else if (typeof tobj[i] === "object") {
                    func(name + i + ".", tobj[i]);
                } else {
                    obj[name + i] = tobj[i];
                }
            }
        }

        func(objName, turnObj);
        return obj;
    };

    return function (json, arrName) {
        arrName = arrName || "";
        if (typeof json !== "object") throw new Error("请传入json对象");
        if (MvcParameterAdaptive.isArray(json) && !arrName) throw new Error("请指定数组名，对应Action中数组参数名称！");

        if (MvcParameterAdaptive.isArray(json)) {
            return MvcParameterAdaptive.convertArrayToObject(arrName, json);
        }
        return MvcParameterAdaptive.convertObject("", json);
    };
})();