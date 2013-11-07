function UserBossMainCtrl($scope, $http, $location) {
    $scope.salesFunnel = function () {
        var totalHeight = 326;
        var sales = [1152, 234, 532, 321, 323], total = 0;
        var colors = ['#f00', '#f0f', '#0f0', '#0ff', '#ff0'];
        var titles = ['销售机会', '初步接洽', '多次拜访', '报价', '签订合同'];
        var labelTop = [60, 110, 160, 210, 250];
        $scope.salesFunnelList = [];

        for (var i = 0; i < sales.length; i++) {
            total += sales[i];
            $scope.salesFunnelList.push({ style: { background: colors[i], height: 0 }, labelStyle:{background: colors[i]}, top: 0, labelTop: labelTop[i], title: titles[i], data: sales[i] });
        }

        var brotherHeight = 16;
        for (var i = 0; i < sales.length; i++) {
            var height = Math.floor((sales[i] / total) * totalHeight);
            $scope.salesFunnelList[i].style.height = height + 'px';
            $scope.salesFunnelList[i].top = Math.floor(brotherHeight + height / 2);
            brotherHeight += height;
        }

        
    }
    $scope.salesFunnel();

    //jQuery(document).ready(function () {
    //    for (var i = 0; i < $scope.salesFunnelList.length; i++) {
    //        $('#salesLines').KolaLineDraw({
    //            Width: 200,
    //            Height: 350,
    //            LineColor: '#666',
    //            StartPointLeft: 0,
    //            StartPointTop: $scope.salesFunnelList[i].top,
    //            EndPointLeft: 200,
    //            EndPointTop: $scope.salesFunnelList[i].labelTop
    //        });
    //    }
    //});

    $scope.histogram = function (ione, itwo, maxTotal, totalHeight, totalLabelHeight) {

        totalHeight = totalHeight - totalLabelHeight;

        var ioneHeight = Math.floor((ione / maxTotal) * totalHeight);
        var itwoHeight = Math.floor((itwo / maxTotal) * totalHeight);
        var theight = totalHeight - ioneHeight - itwoHeight + totalLabelHeight;

        var ioneStyle = { height: ioneHeight + 'px', 'line-height': ioneHeight + 'px' };
        var itwoStyle = { height: itwoHeight + 'px', 'line-height': itwoHeight + 'px' };
        var totalStyle = { height: theight + 'px' };
        var totalSpaceStyle = { height: (theight > totalLabelHeight ? theight - totalLabelHeight : 0) + 'px' };

        return {
            total: ione + itwo,
            totalStyle: totalStyle,
            totalSpaceStyle: totalSpaceStyle,
            ione: ione,
            ioneStyle: ioneStyle,
            itwo: itwo,
            itwoStyle: itwoStyle
        };
    }
   
    $scope.salesOpp = function () {
        var totalHeight = 300;
        $scope.salesOppList = [];
        var sales = [{ newc: 100, old: 300 }, { newc: 130, old: 1000 }, { newc: 200, old: 150 }];

        var maxTotal = 0;
        for (var i = 0; i < sales.length; i++)
        {
            var item = sales[i];
            if (item.newc + item.old > maxTotal)
                maxTotal = item.newc + item.old;
        }

        for (var i = 0; i < sales.length; i++)
        {
            var item = sales[i];

            $scope.salesOppList.push($scope.histogram(item.newc, item.old, maxTotal, totalHeight, 20));
        }

    };
    $scope.salesOpp();

    $scope.salesFinance = function () {
        var totalHeight = 300;
        $scope.salesFinanceList = [];
        var sales = [{ paydone: 100, notpay: 300, received: 200, notreceive: 230 },
            { paydone: 500, notpay: 200, received: 240, notreceive: 530 },
        { paydone: 140, notpay: 380, received: 80, notreceive: 330 }];

        var maxTotal = 0;
        for (var i = 0; i < sales.length; i++) {
            var item = sales[i];
            if (item.paydone + item.notpay > maxTotal)
                maxTotal = item.paydone + item.notpay;

            if (item.received + item.notreceive > maxTotal)
                maxTotal = item.received + item.notreceive;
        }

        for (var i = 0; i < sales.length; i++) {
            var item = sales[i];
            $scope.salesFinanceList.push({
                pay: $scope.histogram(item.paydone, item.notpay, maxTotal, totalHeight, 20),
                receive: $scope.histogram(item.received, item.notreceive, maxTotal, totalHeight, 20)
            });
        }

    };
    $scope.salesFinance();
}