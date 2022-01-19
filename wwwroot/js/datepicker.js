
const months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
let dateStr = `{
    "20": "a",
"21": "a",
"22": "o",
"23": "o",
"24": "a",
"25": "a"
                }`
let dates = [
{
    "date": "1",
"avail": "a"
    },
{
    "date": "2",
"avail": "b"
    },
{
    "date": "3",
"avail": "a"
    },
{
    "date": "4",
"avail": "a"
    },
]
let times = [
{
    "time": "11:00",
"avail": "a"
    },
{
    "time": "11:30",
"avail": "b"
    },
{
    "time": "12:00",
"avail": "a"
    },
{
    "time": "12:30",
"avail": "a"
    },
]
var tdy = new Date();
let this_month = tdy.getMonth();
let this_year = tdy.getFullYear();
setCal(this_month, this_year);
setDateAvail();
setDateClick();
$("#prevMonth").click(function () {
    $("#calBody").html("")
    if (this_month == 0) {
    this_year -= 1;
this_month = 11;
    }
else {
    this_month -= 1;
    }
setCal(this_month, this_year);
setDateAvail();
setDateClick();
})
$("#nextMonth").click(function () {
    $("#calBody").html("")
    if (this_month == 11) {
    this_year += 1;
this_month = 0;
    }
else {
    this_month += 1;
    }
console.log(this_month + ":" + this_year);
setCal(this_month, this_year);
setDateAvail();
setDateClick();
})
function leapYear(year) {
    if (year % 4 == 0)
return true
else
return false
}
function getDays(month, year) {
    var montharr = Array(12)
montharr[0] = 31
montharr[1] = (leapYear(year)) ? 29 : 28
montharr[2] = 31
montharr[3] = 30
montharr[4] = 31
montharr[5] = 30
montharr[6] = 31
montharr[7] = 31
montharr[8] = 30
montharr[9] = 31
montharr[10] = 30
montharr[11] = 31
return montharr[month]
}
function setCal(month, year) {
    var firstDay = new Date(year, month, 1);
    $("#month").text(months[month]);
    var monthDays = getDays(month, year);
    var dateCount = 0
    var date = 1
    for (let i = 0; i < 6; i++) {
        $("#calBody").append("<tr>");
    for (let j = 0; j < 7; j++) {
                if (firstDay.getDay() > dateCount || date > monthDays) {
        $("#calBody").append("<td></td>");
                }
    else {
        $("#calBody").append(`<td id="d${date}m${month}">${date}</td>`);
    date += 1;
                }
    dateCount += 1
            }
    $("#calBody").append("</tr>")
        }
}
function setDateAvail() {
for (var i = 0; i < dates.length; i++) {
    var obj = dates[i];
    $(`#d${obj.date}m${this_month}`).addClass(obj.avail);
}
}
function setTime(date) {
$("#timePicker").html("");
for (var i = 0; i < 5; i++) {
    $("#timePicker").append(`<div class="a" id="" data-value="0${i}:00">0${i}:00 PM</div>`);
    setTimeClick(date);
}
}
function padZero(val) {
    if (val >= 10)
        return val
    else
        return '0' + val;
}
function setDateClick() {
$("#calBody .a").each(function () {
    $(this).click(function () {
        $("#calBody .a").css("border", "1px solid #B8B8B8")
        date = `${this_year}-${padZero(this_month + 1)}-${padZero(parseInt($(this).text()))}`
        $("#dateSelect").val(date)
        $("#dateTimeSelect").val("");
        $(this).css("border", "3px solid #1977CC");
        setTime(padZero(parseInt($(this).text())));
    })
})
}
function setTimeClick(date) {
$("#timePicker .a").each(function () {
    $(this).click(function () {
        $("#timePicker").children().css("border", "1px solid #B8B8B8");
        $("#timeSelect").val($(this).text());
        datetime = `${this_year}-${padZero(this_month + 1)}-${padZero(parseInt(date))}T${$(this).data("value")}`
        $("#dateTimeSelect").val(datetime)
        $(this).css("border", "3px solid #1977CC");
    })
})
}
