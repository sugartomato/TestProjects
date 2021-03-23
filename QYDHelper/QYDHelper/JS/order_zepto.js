console.log(window.location.href);
if (window.location.href.indexOf("http://m.quyundong.com/court/book?bid=22887") != -1 || window.location.href.indexOf("https://m.quyundong.com/court/book?bid=22887") != -1) {

    // 判断是否开放场地
    //console.log(jQuery("body").text());
    var str1 = "场馆只开放一周内的场地预定哦";
    var str2 = $('body').text();
    console.log(str2.search(str1));
    if (str2.search(str1) != -1) {
        console.log("x1");
    }
    else {
        console.log("下单")

        var goodsid = "";
        // 执行下单
        //$goodsid$


        //var targetUrl = "https://m.quyundong.com/order/Confirm?price[]=120&hour[]=9&course_name[]=1号场&real_time[]=9:00-10:00&price[]=70&hour[]=10&course_name[]=1号场&real_time[]=10:00-11:00&allcourse_name=1号场,2号场,3号场,4号场,5号场,6号场,7号场,8号场,&goods_ids=369156193,369156194&book_date=1601308800&court_name=四得公园羽毛球馆&category_name=羽毛球&bid=22887&cid=1&order_type=0&relay=0"
        //window.location.href = targetUrl;
        $(".J_goodsIds").val(goodsid);
        $(".J_payConfirm").submit();
    }

    //return goodsid;
}

if (window.location.href.indexOf("https://m.quyundong.com/order/Confirm") != -1 || window.location.href.indexOf("http://m.quyundong.com/order/Confirm") != -1) {
    window.setInterval(checkorder, 10000);

    console.log("提交订单页面处理");
    //jQuery("#goodsAmount").val("140")
    //jQuery("#J_payHash").val("fbc0acd7d5058972bc37cbf9ec099a3a")

    $("#orderSubmit").click();
}

if (window.location.href.indexOf("https://m.quyundong.com/order/pay") != -1 || window.location.href.indexOf("http://m.quyundong.com/order/pay") != -1) {
    console.log("x2");
}

function checkorder() {
    // 如果是有提示支付，执行取消订单点击。否则，直接执行提交
    if ($("#book-Cancel").length == 1 && $("#book-Cancel").css('visibility') == "visible") {
        console.log("取消支付，重新下单！");
        $("#book-Cancel").click();
    }
}
