

$(function () {

    //////////////绑定地区开始
    ///设置数组
    var city = [
    ["北京市", "天津市", "上海市", "重庆市"],
    ["广州市", "深圳市", "珠海市", "东莞市"],
   ["南京市", "苏州市", "南通市", "常州市"],
  ["福州市", "厦门市", "莆田市", "泉州市"]
    ];
    var district = [
       [
           ["东城区", "西城区", "宣武区"], ["和平区", "红桥区", "塘沽区"], ["杨浦区", "徐汇区"], ["万州区", "涪陵区"]
       ],
       [
          ["天河区", "海珠区", "白云区", "番禺区"], ["南山区", "宝安区", "福田区"], ["香洲区", "斗门区", "金湾区"], ["东城区", "莞城区", "万江区"]
       ],
       [
            ['玄武区', '白下区', '秦淮区', '建邺区'], ['沧浪区', '平江区', '金阊区', '虎丘区'], ['崇川区', '港闸区', '海安县', '如东县'], ['天宁区', '钟楼区', '新北区']
       ],
        [
           ['鼓楼区', '台江区', '仓山区', '马尾区'], ['思明区', '海沧区', '湖里区', '集美区'], ['城厢区', '涵江区', '荔城区', '秀屿区'], ['鲤城区', '丰泽区', '洛江区', '泉港区']
        ]
    ];
    //////////////////////////当省份下拉框改变索引的时候触发的事件
    $('select[name=province]').change(function () {




        //获得省份下拉框的对象
        var sltProvince = $('select[id=province]');
        //获得城市下拉框的对象
        var sltCity = $('select[name=city]');
        //获得市区下拉框的对象
        var sltDistrict = $('select[name=district]');

        //得到对应省份的城市数组
        var pselectedIndex = sltProvince.children('option:selected').index();

        var provinceCity = city[pselectedIndex - 1];
       
        //清空城市下拉框，仅留提示选项
        $(sltCity).children().not(":first").remove();
        $(sltDistrict).children().not(":first").remove();
        if (pselectedIndex != 0) {
        
            //将城市数组中的值填充到城市下拉框中
            for (var i = 0; i < provinceCity.length; i++) {
                var option = $("<option>").text(provinceCity[i]).val(provinceCity[i])
                sltCity.append(option);
            }
        }

    })
    $('select[name=city]').change(function () {

        //获得省份下拉框的对象
        var sltProvince = $('select[id=province]');
        //获得城市下拉框的对象
        var sltCity = $('select[name=city]');
        //获得市区下拉框的对象
        var sltDistrict = $('select[name=district]');
        //得到对应城市的市区数组
        var pselectedIndex = sltProvince.children('option:selected').index();
        var cselectedIndex = $(this).children('option:selected').index();
        //如果是初次加载的时候，不执行。
        if(cselectedIndex!=0)
        {
            var cityDistrict = district[pselectedIndex - 1][cselectedIndex - 1];
            //清空城市下拉框，仅留提示选项
            $(sltDistrict).children().not(":first").remove();

            //将城市数组中的值填充到城市下拉框中
            for (var i = 0; i < cityDistrict.length; i++) {
                var option = $("<option >").text(cityDistrict[i]).val(cityDistrict[i])
                sltDistrict.append(option);
            }
        }
      

        //获取当前选中的城市的值
        var selectCityValue = $(this).children('option:selected').val();
        //给隐藏的城市输入框赋值，为了给后台传值
        $("input[id=city]").val(selectCityValue);
      
    });
    //当地区下拉框发生改变的时候(在新增地址的时候用到)
    $('select[name=district]').change(function () {
     
        //获取当前选中的城市的值
        var selectdistrictValue = $(this).children('option:selected').val();
        //给隐藏的城市输入框赋值，为了给后台传值
        $("input[id=district]").val(selectdistrictValue);
     

    });

    ///////////////地区结束
})
