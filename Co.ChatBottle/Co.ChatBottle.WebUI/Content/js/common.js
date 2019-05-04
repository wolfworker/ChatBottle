
//js date 拓展方法，转换自定义的时间格式
Date.prototype.Format = function (fmt) { //author: brucezhang
    var o = {
        "M+": this.getMonth() + 1, //月份
        "d+": this.getDate(), //日
        "h+": this.getHours(), //小时
        "m+": this.getMinutes(), //分
        "s+": this.getSeconds(), //秒
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度
        "S": this.getMilliseconds() //毫秒
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

//yyyy/mm/dd这种格式转化成日期对像可以用new Date(str);在转换成指定格式
//yyyy-mm-dd这种格式的字符串转化成日期对象可以用new Date(Date.parse(str.replace(/-/g,"/")));
//yyyy-mm-dd T HH:mm:ss，这种的可以先转换成yyyy-mm-dd HH:mm:ss这种，再进行转换
String.prototype.FormatToDate = function (fmt) {
    try {
        var dateStr = this;
        var dateTemp = new Date();
        if (dateStr.indexOf("T") != -1) {
            dateStr = dateStr.replace(/T/g, " ");
        }
        if (dateStr.indexOf("-") != -1) {
            dateStr = dateStr.replace(/-/g, "/");
        }

        if (dateStr.indexOf("/") != -1) {
            var timeArr = dateStr.split(" ");
            if (timeArr != undefined && timeArr.length > 0) {
                var dateArr = timeArr[0].split("/");
                if (dateArr != undefined && dateArr.length > 2) {
                    dateStr = dateArr[1] + "/" + dateArr[2] + "/" + dateArr[0];
                }

                if (timeArr.length > 1) {
                    if (timeArr[1] != undefined && timeArr[1] != "") {
                        var timestr = timeArr[1].split(".");
                        dateStr = dateStr + " " + timestr[0];
                    }
                }
            }
            dateTemp = new Date(dateStr);
        }
        else {
            return "";
        }
        return dateTemp.Format(fmt);
    } catch (e) { return ""; }
}