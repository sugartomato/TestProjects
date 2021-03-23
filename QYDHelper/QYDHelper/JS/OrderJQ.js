(function () {
    var account = "$account$";
    var password = "$password$";

    // 加载JQ
    // ==============================
    var script = document.createElement("script")
    script.type = "text/javascript";
    script.onload = function () {
        $.noConflict();
        console.log("jQuery加载完成！");
        console.log("当前地址：" + window.location.href);
        if (window.location.href.indexOf("http://m.quyundong.com/court/book?bid=22887") != -1 || window.location.href.indexOf("https://m.quyundong.com/court/book?bid=22887") != -1) {

            // 判断是否开放场地
            //console.log(jQuery("body").text());
            var str1 = "场馆只开放一周内的场地预定哦";
            var str2 = jQuery("body").text();
            console.log(str2.search(str1));
            if (str2.search(str1) != -1) {
                console.log("x1");
                return;
            }

            // 执行下单
            //$goodsid$
            var goodsid = "";
            goodsid += jQuery(".book-list:eq(0)").find("ul:eq(2)").find("li:eq(-3)").attr("goodsid") + ","
            goodsid += jQuery(".book-list:eq(0)").find("ul:eq(2)").find("li:eq(-4)").attr("goodsid")

            console.log("下单")

            //var targetUrl = "https://m.quyundong.com/order/Confirm?price[]=120&hour[]=9&course_name[]=1号场&real_time[]=9:00-10:00&price[]=70&hour[]=10&course_name[]=1号场&real_time[]=10:00-11:00&allcourse_name=1号场,2号场,3号场,4号场,5号场,6号场,7号场,8号场,&goods_ids=369156193,369156194&book_date=1601308800&court_name=四得公园羽毛球馆&category_name=羽毛球&bid=22887&cid=1&order_type=0&relay=0"
            //window.location.href = targetUrl;
            jQuery(".J_goodsIds").val(goodsid);
            jQuery(".J_payConfirm").submit();
            return goodsid;
        }

        if (window.location.href.indexOf("https://m.quyundong.com/order/Confirm") != -1 || window.location.href.indexOf("http://m.quyundong.com/order/Confirm") != -1) {
            window.setInterval(checkorder, 10000);

            console.log("提交订单页面处理");
            jQuery("#goodsAmount").val("140")
            //jQuery("#J_payHash").val("fbc0acd7d5058972bc37cbf9ec099a3a")

            jQuery("#orderSubmit").click();
        }

        if (window.location.href.indexOf("https://m.quyundong.com/order/pay") != -1 || window.location.href.indexOf("http://m.quyundong.com/order/pay") != -1) {
            console.log("x2");
        }

        // 登录界面
        if (window.location.href.indexOf("m.quyundong.com/login") != 1) {
            // login-tel J_tel
            // J_pwd
            // J_submit
            jQuery(".login-tel").val(account)
            jQuery(".J_pwd").val(password)
            jQuery(".J_submit").click();
        }
    }
    //script.src = "http://libs.baidu.com/jquery/2.0.0/jquery.min.js";
    script.src = "https://lib.baomitu.com/jquery/1.12.4/jquery.min.js";
    document.getElementsByTagName("head")[0].appendChild(script);
    // ==============================


    function checkorder() {
        // 如果是有提示支付，执行取消订单点击。否则，直接执行提交
        if (jQuery("#book-Cancel").length == 1 && !jQuery("#book-Cancel").is(":hidden")) {
            console.log("取消支付，重新下单！");
            jQuery("#book-Cancel").click();
        }
    }


})();
//# sourceMappingURL=Order.js.map