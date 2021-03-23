(function () {

    // 加载JQ
    // ==============================
    var script = document.createElement("script")
    script.type = "text/javascript";
    script.onload = function () {
        $.noConflict();

        jQuery("#orderSubmit").click();
    }
    //script.src = "http://libs.baidu.com/jquery/2.0.0/jquery.min.js";
    script.src = "https://lib.baomitu.com/jquery/1.12.4/jquery.min.js";
    document.getElementsByTagName("head")[0].appendChild(script);
    // ==============================


})()