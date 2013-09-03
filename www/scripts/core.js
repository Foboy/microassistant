var $sitecore = $sitecore || {};

$sitecore.urls = $sitecore.urls || {};
$sitecore.urls.base = "";
$sitecore.urls.add = function(name,url){
	$sitecore.urls[name] = $sitecore.urls.base + url;
};
$sitecore.urls.add("productCat", "scripts/product/json/productCat.json");
$sitecore.urls.add("productList", "scripts/product/json/productList.json");