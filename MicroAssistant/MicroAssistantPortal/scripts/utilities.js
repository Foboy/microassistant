var utilities = utilities || {};

var docWrite = document.write;
document.write = function (text){
	var scripttest = /src="([\s\S]*?)"/i;
	if(scripttest.test(text))
	{
		var url = text.match( /src="([\s\S]*?)"/i )[1];
		utilities.loadscript(url);
	}
};
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