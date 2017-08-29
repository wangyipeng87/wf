var lang = {
    "sProcessing": "处理中...",
    "sLengthMenu": "每页 _MENU_ 项",
    "sZeroRecords": "没有匹配结果",
    "sInfo": "当前显示第 _START_ 至 _END_ 项，共 _TOTAL_ 项。",
    "sInfoEmpty": "当前显示第 0 至 0 项，共 0 项",
    "sInfoFiltered": "(由 _MAX_ 项结果过滤)",
    "sInfoPostFix": "",
    "sSearch": "搜索:",
    "sUrl": "",
    "sEmptyTable": "表中数据为空",
    "sLoadingRecords": "载入中...",
    "sInfoThousands": ",",
    "oPaginate": {
        "sFirst": "首页",
        "sPrevious": "上页",
        "sNext": "下页",
        "sLast": "末页",
        "sJump": "跳转"
    },
    "oAria": {
        "sSortAscending": ": 以升序排列此列",
        "sSortDescending": ": 以降序排列此列"
    }
};

$.htmlEncode = function (str) {
    var div = document.createElement("div");
    var text = document.createTextNode(str);
    div.appendChild(text);
    return div.innerHTML;
}
$.htmlDecode = function (str) {
    var div = document.createElement("div");
    div.innerHTML = str;
    return div.innerHTML;
}
// 对Date的扩展，将 Date 转化为指定格式的String
// 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符，
// 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字)
// 例子：
// (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423
// (new Date()).Format("yyyy-M-d h:m:s.S")      ==> 2006-7-2 8:9:4.18
//Date.prototype.Format = function (fmt) { //author: meizz
//    var o = {
//        "M+": this.getMonth() + 1, //月份
//        "d+": this.getDate(), //日
//        "h+": this.getHours(), //小时
//        "m+": this.getMinutes(), //分
//        "s+": this.getSeconds(), //秒
//        "q+": Math.floor((this.getMonth() + 3) / 3), //季度
//        "S": this.getMilliseconds() //毫秒
//    };
//    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
//    for (var k in o)
//        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
//    return fmt;
//}

function ChangeDateFormat(cellval) {
    return cellval.substring(0, 10);;
}
$.isempty = function (str) {
    if (str == null || str == undefined || str == "" || $.trim(str) == "") {
        return true;
    }
    return false;
}
var layerloadindex
function startloading() {
    layerloadindex = layer.load();
}
function closeloading() {
    layer.close(layerloadindex);
}
/*求两条线段的交点
用法：segmentsIntr({x:0,y:0},{x:100,y:100},{x:20,y:20},{x:150,y:50})
返回值 ：flase 为没有交点 {x,y}为有交点
*/
$.segmentsIntr=function  (a, b, c, d) {

    /** 1 解线性方程组, 求线段交点. **/
    // 如果分母为0 则平行或共线, 不相交  
    var denominator = (b.y - a.y) * (d.x - c.x) - (a.x - b.x) * (c.y - d.y);
    if (denominator == 0) {
        return false;
    }

    // 线段所在直线的交点坐标 (x , y)      
    var x = ((b.x - a.x) * (d.x - c.x) * (c.y - a.y)
                + (b.y - a.y) * (d.x - c.x) * a.x
                - (d.y - c.y) * (b.x - a.x) * c.x) / denominator;
    var y = -((b.y - a.y) * (d.y - c.y) * (c.x - a.x)
                + (b.x - a.x) * (d.y - c.y) * a.y
                - (d.x - c.x) * (b.y - a.y) * c.y) / denominator;

    /** 2 判断交点是否在两条线段上 **/
    if (
        // 交点在线段1上  
        (x - a.x) * (x - b.x) <= 0 && (y - a.y) * (y - b.y) <= 0
        // 且交点也在线段2上  
         && (x - c.x) * (x - d.x) <= 0 && (y - c.y) * (y - d.y) <= 0
        ) {

        // 返回交点p  
        return {
            x: x,
            y: y
        }
    }
    //否则不相交  
    return false

}