//////////////////////全选操作//////////////////////
function GetAllCheckbox(parentName) {
    var ch = document.getElementsByTagName("input");
    for (var i = 0; i < ch.length; i++) {
        if (parentName.checked) {
            if (ch[i].type == "checkbox") {
                ch[i].checked = true;
                //价格循环遍历累加

            }
        }
        else {
            if (ch[i].type == "checkbox") {
                ch[i].checked = false;
            }
        }
    }
}



$(function () {
    $("input[id='chkall']").on("click", function () {
        var ch = $("input[name='chpproducts']");
        var sumprice = 0;
        var sumpostage = 0;
        var p = 0;
        var s = 0;
        for (var j = 0; j < ch.length; j++) {

            if (ch[j].checked) {
                id = $(ch[j]).attr('id');
                p = $("#price_" + id + "").text();
                s = $("#goodsPsotage_" + id + "").text();
                //价格循环遍历累加
                sumprice += parseInt(p);//获取价格
                sumpostage += parseInt(s);//获取邮费

            }
        }
        //设置价格

        $(".end_Total").text(sumprice);
        $(".end_postage").text(sumpostage);
        $(".end_totalPos").text(sumprice);

    })

    $("input[name='chpproducts']").on("click", function () {
        var ch = $("input[name='chpproducts']");
        var id = 0;
        var selectedNum = 0;

        for (var i = 0; i < ch.length; i++) {
            if (ch[i].checked) {
                selectedNum += 1;
            }
        }
        var price;
        var postage;

        if (selectedNum == 1) {
            //获取id为当前的
            var idd = $(this).attr('id');
            price = $("#price_" + idd + "").text();//获取价格
            postage = $("#goodsPsotage_" + idd + "").text();//获取邮费

            //设置价格
            $(".end_Total").text(price);
            $(".end_postage").text(postage);
            $(".end_totalPos").text(price);

        }
        else {
            var sumprice = 0;
            var sumpostage = 0;
            var p = 0;
            var s = 0;
            for (var j = 0; j < ch.length; j++) {

                if (ch[j].checked) {
                    id = $(ch[j]).attr('id');
                    p = $("#price_" + id + "").text();
                    s = $("#goodsPsotage_" + id + "").text();
                    //价格循环遍历累加
                    sumprice += parseInt(p);//获取价格
                    sumpostage += parseInt(s);//获取邮费

                }
            }
            //设置价格

            $(".end_Total").text(sumprice);
            $(".end_postage").text(sumpostage);
            $(".end_totalPos").text(sumprice);

        }


    })

})