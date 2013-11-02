var $sitecore = $sitecore || {};

$sitecore.urls = $sitecore.urls || {};
$sitecore.urls.base = "";
$sitecore.urls.add = function(name,url){
	$sitecore.urls[name] = $sitecore.urls.base + url +"?timestamp="+ new Date().getTime();
};
$sitecore.urls.add("userLogin", "/UserManagement/User/Login"); 
$sitecore.urls.add("userRegister", "/UserManagement/User/UserRegister"); 
$sitecore.urls.add("enterpriseRegister", "/UserManagement/User/EntRegister");

$sitecore.urls.add("productCat", "/ProductManagement/Production/SearchProductTypeListByEntID");
$sitecore.urls.add("productAddCat", "/ProductManagement/Production/AddProductionType"); 
$sitecore.urls.add("productList", "/ProductManagement/Production/SearchProductionSByType");
$sitecore.urls.add("productDetail", "/ProductManagement/Production/GetProductInfoByPID");
$sitecore.urls.add("productAdd", "/ProductManagement/Production/AddProduction");
$sitecore.urls.add("productUpdate", "/ProductManagement/Production/UpateProduction");
$sitecore.urls.add("productStoresList", "/ProductManagement/Production/SearchProductonDetailList"); 
$sitecore.urls.add("productAddStores", "/ProductManagement/Production/AddProductonDetail");

$sitecore.urls.add("salesAddChance", "/MarketingManagement/Marketing/AddMarketingChance"); 
$sitecore.urls.add("salesChanceList", "/MarketingManagement/Marketing/SearchMarketingList"); 
$sitecore.urls.add("salesChanceVisitsList", "/MarketingManagement/Marketing/GetVisitInfo");
$sitecore.urls.add("salesAddChanceVisits", "/MarketingManagement/Marketing/ToVisit"); 
$sitecore.urls.add("salesRateChange", "/MarketingManagement/Marketing/EditCustomerRate");
$sitecore.urls.add("salesChanceVisitList", "/MarketingManagement/Marketing/SearchVisitInfoList");

$sitecore.urls.add("receivablesfinanceList", "/FinancialManagement/Financial/SearchReceivables");//������ҵID��ȡӦ�տ��б� ���� Ӧ�տ��б�
$sitecore.urls.add("payablesfinanceList", "/FinancialManagement/Financial/SearchPayablesByEID");//������ҵID��ȡӦ�����б� ���� Ӧ�����б�
$sitecore.urls.add("receivablesDetail", "/FinancialManagement/Financial/GetHowToPayByEID");//Ӧ�տ�����
$sitecore.urls.add("makeSureTimesReceivable", "/FinancialManagement/Financial/ConfirmReceived");//ȷ���տ�
$sitecore.urls.add("makeSurePay", "/FinancialManagement/Financial/ConfirmPay");//ȷ�ϸ���



$sitecore.urls.add("SearchCustomerEntByOwnerId", "/CustomerManagement/Customer/SearchCustomerEntByOwnerId");//ͨ��������ԱID��ȡ���۵���ҵ�ͻ�
$sitecore.urls.add("SearchCustomerPrivByOwnerId", "/CustomerManagement/Customer/SearchCustomerPrivByOwnerId");//�����û�ID��ѯ���˿ͻ���token������ ���˿ͻ��б����������䣬������ҵ�����ڵأ���ϵ��ʽ��
$sitecore.urls.add("AddOrUpdateEnterPriseClient", "/CustomerManagement/Customer/AddEntCustomer");//�����޸���ҵ�ͻ���Ϣ
$sitecore.urls.add("AddOrUpdatePersonalClient", "/CustomerManagement/Customer/AddPrivateCustomer");//�����޸ĸ��˿ͻ���Ϣ



