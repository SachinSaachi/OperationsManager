
$(function () {
    $('[data-toggle="tooltip"]').tooltip()
    $('body').tooltip({ selector: '[data-toggle="tooltip"]' });
    $("#tblIssueLog").DataTable({
        dom: 'Bfrtip',
        "bInfo": false,
        "paging": false
    });
    BindStatus();
    BindGroups();
    SetInputDate("#txtToDate", new Date())
    SetInputDate("#txtFromDate", new Date().setMonth(new Date().getMonth()-1))
});
function SetInputDate(_id,inputDate) {

    var _dat = document.querySelector(_id);
    var hoy = new Date(inputDate),
        d = hoy.getDate(),
        m = hoy.getMonth() + 1,
        y = hoy.getFullYear(),
        data;

    if (d < 10) {
        d = "0" + d;
    };
    if (m < 10) {
        m = "0" + m;
    };

    data = y + "-" + m + "-" + d;
    console.log(data);
    _dat.value = data;
};
function ConvertDateFormat(inputDate)
{
    let objectDate = new Date(inputDate);
    let day = objectDate.getDate();
    console.log(day); // 23

    let month = objectDate.getMonth();
    console.log(month + 1); // 8

    let year = objectDate.getFullYear();
    console.log(year); // 2022
    return month + "/" + day + "/" + year;
}


$(document).on('click', '#btnsubmit', function () {
    
    FilterIssue();
});
function BindStatus() {

    $.ajax({
        url: 'LogIssue/BindStatus',
        type: "Get",
        success: function (data) {
            console.log(data);
            for (var i = 0; i < data.length; i++) {
                var opt = new Option(data[i].name, data[i].id);
                $('#ddlSatus').append(opt);

            }

        }

    });
}
$(document).on('change', 'input', function () {
    // Does some stuff and logs the event to the console
});
function BindGroups() {

    $.ajax({
        url: 'LogIssue/GetGroup',
        type: "Get",
        success: function (data) {
            console.log(data);
            for (var i = 0; i < data.length; i++) {
                var opt = new Option(data[i].groupName, data[i].groupID);
                $('#ddlgroup').append(opt);
            }

        }

    });
}
function FilterIssue()
{
    var carttable = $('#tblIssueLog').DataTable();
    carttable.destroy();
    var FromDate = $("#txtFromDate").val();
    var ToDate = $("#txtToDate").val();
    var TicketType = $("#ddlTicktType").val();
    var Status = $("#ddlSatus").val();
    var Group = $("#ddlgroup").val();
    var ComplaintIds = $("#ComplainIds").val();

    $.ajax({
        type: 'Post',
        url: 'LogIssue/GetLogSheet',
        data: { "FromDate": FromDate, "ToDate": ToDate, "TicketType": TicketType, "Status": Status, "Group": Group, "ComplaintIds": ComplaintIds},

        success: function (res) {
            //$("#tblIssueLog").css('height', '500px')
            
            console.log(res); var carttable = $('#tblIssueLog').DataTable();
            carttable.destroy();
            $("#tblIssueLog").DataTable({
                data: res,
                dom: 'Bfrtip',
                "bInfo": false,
                "paging": false,
                fixedHeader: true,
                buttons: [
                    {
                        extend: 'excel',
                        text: 'Export to Excel',
                        title: 'Development Task List',

                    }
                ],
                "ordering": false,
                columns: [

                    { "data": "complaint_id" },
                    { "data": "companyName" },
                    { "data": "departmentName" },
                    { "data": "locationName" },
                    { "data": "userType" },
                    { "data": "severity_id" },
                    { "data": "status" },
                    { "data": "problem_details" },
                    {
                        "render": function (data, type, full, meta) {
                            var text = "";
                            if (full.shortproblem.length > 0) {
                                text = typeof full.shortproblem === 'string' && full.shortproblem.length > 25 ? full.shortproblem.substring(0, 25) + '...' : full.shortproblem;
                            }
                            return '<p data-toggle="tooltip" data-placement="top" title="' + full.shortproblem + '" >' + text + '</p>';
                            return text;
                        }
                    },
                    { "data": "loggedin_time" },
                    { "data": "expctdclsrDatetime" },
                    { "data": "resolution_time" },
                    { "data": "engineer_name" },
                    { "data": "cce_name" },
                    {
                        "render": function (data, type, full, meta) {
                            var text = "";
                            if (full.engineer_Remarks.length > 0) {
                                text = typeof full.engineer_Remarks === 'string' && full.engineer_Remarks.length > 25 ? full.engineer_Remarks.substring(0, 25) + '...' : full.engineer_Remarks;
                            }
                            return '<p data-toggle="tooltip" data-placement="top" title="' + full.engineer_Remarks + '" >' + text + '</p>';
                         
                        }
                    },
                    { "data": "resolvedBy" }
                    //{ "data": "BreachMinute" },//{ "data": "ChildComplaint_id" },   /// Need to funcational
                  

                ]


            });
            $("<p style='float: left;'> Total Records : <strong>" + res.length + "</strong></p>").insertAfter("#tblIssueLog_filter label");
        },
        error: function () {
            alert("no saved");
        }
    });
   

}