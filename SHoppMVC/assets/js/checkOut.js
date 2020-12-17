$(function () {
    //$('select[name=shippingName]').change(function () {
    //    var selectIndex = $(this).children('option:selected').val();
    //    //获取小计的价格
    //    var price = $("span[class=Price]").text();
    //    if (selectIndex == 2) {
    //        $(".shipFee").text("12");
    //        var resu = parseInt(price) + 12;
    //        $("span[class=total_Price]").text(resu);
    //        $("input[name=totalPrice]").attr("value", resu);
    //    }
    //    else
    //    {
    //        $(".shipFee").text("0");
    //        $("span[class=total_Price]").text(price);
    //        $("input[name=totalPrice]").attr("value", price);
    //    }
      
    //})
    //计算总费用
    //获取小计的价格
    var price = $("span[class=Price]").text();
    var Fee = $(".shipFee").text();
    var resu = parseInt(price) +parseInt(Fee);
    $("input[name=totalPrice]").attr("value", resu);

    $(".Baddress_Flag:first").css("opacity", "1");
    var moren=$(".Baddress_Flag:first").attr("id");
    //默认选中的地址ID
    //给一个框赋值选中地址的主键值
    $("input[name=addressId]").attr("value", moren);
 
    $("button[name=btnAddress]").click(function () {
        var selectAddress = $(this).attr("id");//获取选中按钮的id值(即主键)
        $("div[class=Baddress_Flag][id='" + selectAddress + "']").css("opacity", "1");
        $("div[class=Baddress_Flag][id!='" + selectAddress + "']").css("opacity", "0");
        //给一个框赋值选中地址的主键值
        $("input[name=addressId]").attr("value", selectAddress);
     
    })
   

})
