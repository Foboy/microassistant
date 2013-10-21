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