
//https://www.w3schools.com/jsref/jsref_getmonth.asp
var month = new Array();
month[0] = "January";
month[1] = "February";
month[2] = "March";
month[3] = "April";
month[4] = "May";
month[5] = "June";
month[6] = "July";
month[7] = "August";
month[8] = "September";
month[9] = "October";
month[10] = "November";
month[11] = "December";




/**
 * https://stackoverflow.com/a/9047794/4336330
 * Returns the week number for this date.  dowOffset is the day of week the week
 * "starts" on for your locale - it can be from 0 to 6. If dowOffset is 1 (Monday),
 * the week returned is the ISO 8601 week number.
 * @param int dowOffset
 * @return int
 */
Date.prototype.getWeek = function (dowOffset) {
    /*getWeek() was developed by Nick Baicoianu at MeanFreePath: http://www.meanfreepath.com */

    dowOffset = typeof (dowOffset) == 'int' ? dowOffset : 0; //default dowOffset to zero
    var newYear = new Date(this.getFullYear(), 0, 1);
    var day = newYear.getDay() - dowOffset; //the day of week the year begins on   
    day = (day >= 0 ? day : day + 7);
    var daynum = Math.floor((this.getTime() - newYear.getTime() -
        (this.getTimezoneOffset() - newYear.getTimezoneOffset()) * 60000) / 86400000) + 1;
    var weeknum;
    /*if the year starts before the middle of a week*/
    if (day < 4) {
        weeknum = Math.floor((daynum + day - 1) / 7) + 1;
        if (weeknum > 52) {
            nYear = new Date(this.getFullYear() + 1, 0, 1);
            nday = nYear.getDay() - dowOffset;
            nday = nday >= 0 ? nday : nday + 7;
            /*if the next year starts before the middle of
              the week, it is week #1 of that year*/
            weeknum = nday < 4 ? 1 : 53;
        }
    }
    else {
        weeknum = Math.floor((daynum + day - 1) / 7);
    }
    return weeknum;
};


/**
 * //https://gist.github.com/Abhinav1217/5038863
 *  Add a getWeek() method in Javascript inbuilt Date object.
 * This function is the colsest I could find which is ISO-8601 compatible. This is what php's `Date->format('w')` uses.
 * ISO-8601 means.
 *    Week starts from Monday.
 *    Week 1 is the week with first thurday of the year or the week which has 4th jan in it.
 * @param  {[Date]}   Prototype binding with Date Object. 
 * @return {[Int]}    Integer from 1 - 53 which denotes the week of the year.
 */

//Date.prototype.getWeek = function () {

//    // Create a copy of this date object  
//    var target = new Date(this.valueOf());

//    // ISO week date weeks start on monday, so correct the day number  
//    var dayNr = (this.getDay() + 6) % 7;

//    // Set the target to the thursday of this week so the  
//    // target date is in the right year  
//    target.setDate(target.getDate() - dayNr + 3);

//    // ISO 8601 states that week 1 is the week with january 4th in it  
//    var jan4 = new Date(target.getFullYear(), 0, 4);

//    // Number of days between target date and january 4th  
//    var dayDiff = (target - jan4) / 86400000;

//    if (new Date(target.getFullYear(), 0, 1).getDay() < 5) {
//        // Calculate week number: Week 1 (january 4th) plus the    
//        // number of weeks between target date and january 4th    
//        return 1 + Math.ceil(dayDiff / 7);
//    }
//    else {  // jan 4th is on the next week (so next week is week 1)
//        return Math.ceil(dayDiff / 7);
//    }
//};

/**
 * Get the date and days within a week from week number.
 * eg: date range for 8th week in 2013 is 17th Feb to 23rd Feb. This 
 * code snippet will give you.
 *
 * It is not my code completely, Bit of modification from something
 * i found on net. Cant find it anymore so keeping a backup.
 * 
 * @param  {[Integer]} weekNo [From week 1 to Week 52/53 based on the system date setting]
 * @return {[Date]}        [description]
 */
function getDateRangeOfWeek(weekNo) {
    var d1 = new Date();
    var currentyear = d1.getFullYear();
    numOfdaysPastSinceLastMonday = eval(d1.getDay() - 1);
    //numOfdaysPastSinceLastMonday = eval(d1.getDay());
    d1.setDate(d1.getDate() - numOfdaysPastSinceLastMonday);
    var weekNoToday = d1.getWeek();
    var weeksInTheFuture = eval(weekNo - weekNoToday);
    d1.setDate(d1.getDate() + eval(7 * weeksInTheFuture));
    var rangeIsFrom

    if (weekNo == 1) {
        if (d1.getFullYear() < currentyear) {
            rangeIsFrom = 1 + "/" + 1 + "/" + currentyear;
        }
    }
    else {
        rangeIsFrom = d1.getDate() + "/" + eval(d1.getMonth() + 1) + "/" + d1.getFullYear();
    }


    d1.setDate(d1.getDate() + 6);
    var rangeIsTo = d1.getDate() + "/" + eval(d1.getMonth() + 1) + "/" + d1.getFullYear();

    return { week: weekNo, start: rangeIsFrom, end: rangeIsTo, startMDY: moment(rangeIsFrom, 'DD/MM/YYYY').format("M/D/YYYY"), endMDY: moment(rangeIsTo, 'DD/MM/YYYY').format("M/D/YYYY") }
};
/**
 * 
 * @param {any} date
 */
function GetWeekDateRangeListUpto(date) {
    var weekno = date.getWeek()

    var list = []

    for (i = 1; i <= weekno; i++) {
        list.push(getDateRangeOfWeek(i))
    }

    return list;

}
/**
 * 
 * @param {any} date
 */
//https://stackoverflow.com/a/13572682/4336330
function GetMonthDateRangeListUpto(date) {

    var curdate = date, y = curdate.getFullYear(), m = curdate.getMonth();

    var list = []

    for (i = 0; i <= m; i++) {

        let firstDay = new Date(y, i, 1);
        let lastDay = new Date(y, i + 1, 0);

        // console.log(i,firstDay,lastDay)
        let start = firstDay.getDate() + "/" + eval(firstDay.getMonth() + 1) + "/" + firstDay.getFullYear();
        let end = lastDay.getDate() + "/" + eval(lastDay.getMonth() + 1) + "/" + lastDay.getFullYear();

        list.push({
            monthnumber: i,
            monthname: month[i],
            start: start,
            end: end,
            startMDY: moment(start, 'DD/MM/YYYY').format("M/D/YYYY"),
            endMDY: moment(end, 'DD/MM/YYYY').format("M/D/YYYY"),
        });

    }

    return list;

}
/**
 * 
 * @param {any} monthno
 * @param {any} year
 */
function GetMonthDateRange(monthno, year) {

    var curdate = new Date(), y = (typeof year == typeof undefined) ? curdate.getFullYear() : year, m = (typeof monthno == typeof undefined) ? curdate.getMonth() : (monthno - 1);

    //var list = []

    // for (i = 0; i <= m; i++) {

    let firstDay = new Date(y, m, 1);
    let lastDay = new Date(y, m + 1, 0);

    // console.log(i,firstDay,lastDay)
    let start = firstDay.getDate() + "/" + eval(firstDay.getMonth() + 1) + "/" + firstDay.getFullYear();
    let end = lastDay.getDate() + "/" + eval(lastDay.getMonth() + 1) + "/" + lastDay.getFullYear();

    return {
        monthnumber: m,
        monthname: month[m],
        start: start,
        end: end,
        startMDY: moment(start, 'DD/MM/YYYY').format("M/D/YYYY"),
        endMDY: moment(end, 'DD/MM/YYYY').format("M/D/YYYY"),
    };

    //}

    // return list;

}
/**
 * 
 * @param {any} date
 */
function GetQuarterhDateRangeListUpto(date) {

    var today = date;
    var quarter = Math.floor((today.getMonth() + 3) / 3);

    var list = []
    for (q = 1; q <= quarter; q++) {

        var start = new Date(today.getFullYear(), (q - 1) * 3, 1)

        var end = new Date(start)
        end.setMonth(end.getMonth() + +3);
        end.setDate(end.getDate() - 1)


        list.push({
            quarternumber: q,
            quartername: 'Q' + q,
            start: start.getDate() + "/" + eval(start.getMonth() + 1) + "/" + start.getFullYear(),
            end: end.getDate() + "/" + eval(end.getMonth() + 1) + "/" + end.getFullYear(),
            startMDY: moment(start, 'DD/MM/YYYY').format("M/D/YYYY"),
            endMDY: moment(end, 'DD/MM/YYYY').format("M/D/YYYY"),
        });
    }

    return list;
}
/**
 * 
 * @param {any} quarterno
 */
function GetQuarterhDateRange(quarterno) {

    var today = new Date();
    var quarter = quarterno;

    var list = []
    //for (q = 1; q <= quarter; q++) {

    var start = new Date(today.getFullYear(), (quarter - 1) * 3, 1)

    var end = new Date(start)
    end.setMonth(end.getMonth() + +3);
    end.setDate(end.getDate() - 1)


    return {
        quarternumber: quarter,
        quartername: 'Q' + quarter,
        start: start.getDate() + "/" + eval(start.getMonth() + 1) + "/" + start.getFullYear(),
        end: end.getDate() + "/" + eval(end.getMonth() + 1) + "/" + end.getFullYear(),
        startMDY: moment(start, 'DD/MM/YYYY').format("M/D/YYYY"),
        endMDY: moment(end, 'DD/MM/YYYY').format("M/D/YYYY"),
    };
    //}


}


/**
 * https://stackoverflow.com/a/3426956/4336330
 * @param {any} str
 */
function stringToColor(str) { // java String#hashCode
    var hash = 0;
    for (var i = 0; i < str.length; i++) {
        hash = str.charCodeAt(i) + ((hash << 5) - hash);
    }

    var c = (hash & 0x00FFFFFF)
        .toString(16)
        .toUpperCase();

    return "#" + "00000".substring(0, 6 - c.length) + c;

}
/**
 * 
 * @param {any} num
 */
function intToColor(num) { 
    let word = numberToWord(num)

    let colorcode = stringToColor(word)
    return colorcode

}
/**
 * 
 * @param {any} num
 */
function numberToWord(num) {
    var a = ['', 'one ', 'two ', 'three ', 'four ', 'five ', 'six ', 'seven ', 'eight ', 'nine ', 'ten ', 'eleven ', 'twelve ', 'thirteen ', 'fourteen ', 'fifteen ', 'sixteen ', 'seventeen ', 'eighteen ', 'nineteen '];
    var b = ['', '', 'twenty', 'thirty', 'forty', 'fifty', 'sixty', 'seventy', 'eighty', 'ninety'];

    if ((num = num.toString()).length > 9) return 'overflow';
    n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
    if (!n) return; var str = '';
    str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'crore ' : '';
    str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'lakh ' : '';
    str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'thousand ' : '';
    str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'hundred ' : '';
    str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) /*+ 'only '*/ : '';
    return str;

}










