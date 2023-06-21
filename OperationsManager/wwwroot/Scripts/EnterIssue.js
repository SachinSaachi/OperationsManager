
        var counter;
$(document).ready(function () {
    $("#ddlCircleList").hide();
    $("#lblCircle").hide();
    $("#txtlocation").hide();
            $('.deparment').prop('checked', true);
            if ($(".deparment").prop('checked') == true) {
                $('#trBrand').show();
                $('#trEmail').show();
                $('#TrEmpCode').hide();
                $('#txtEmpCode').text="";
            }
    counter = 0;
    
});


function CategoryType()
{
   var lDepartment = $("#ddlDepartmentlist option:selected").val();
    var CatogryType = $("#ddlCatogryTypeList option:selected").val();
    var CatogryTypetext = $("#ddlCatogryTypeList option:selected").text();

    if (lDepartment == undefined || lDepartment == "") {
        alert('Please select Department first.');
        $("option:selected").prop("selected", false)
        return false;
    }
    if (CatogryTypetext == "Request") { $("#dvAffected").hide(); } else { $("#dvAffected").show(); }
    var ddlCustomers = $("#ddlCategory");
        $.ajax({
            type: "POST",
            url: "/EnterIssue/GetCategoryMaster",
            data: { "TypeId": CatogryType, "DeparmentId": lDepartment },
            success: function (response) {
                var arr = $.parseJSON(response);
                $.each(arr, function (data, value) {

                   ddlCustomers.append($("<option></option>").val(value.CategoryId).html(value.CategoryName));
               })
            }
                });
}
function GetSubCategory() {
    var Category = $("#ddlCategory option:selected").val();
    if (Category == undefined || Category == "") {
        alert('Please select Category first.');
        $("option:selected").prop("selected", false)
        return false;
    }
    var ddlSubCategory = $("#ddlSubCategory");
    ddlSubCategory.empty().append('<option selected="selected" value="" disabled = "disabled">--Select Sub Category--</option>');

    $.ajax({
        type: "POST",
        url: "/EnterIssue/GetSubCategoryList",
        data: { "Category": Category},
        success: function (response) {
            var arr = $.parseJSON(response);
            $.each(arr, function (data, value) {
                ddlSubCategory.append($("<option></option>").val(value.SubCategoryId).html(value.SubCategoryName));
                if (value.SubCategoryName == "Miscellaneous") {
                    $('#ddlSubCategory option:contains("Miscellaneous")').prop('selected', true);
                    $('#ddlSubCategory').prop('disabled', true);
                    GetProblem();
                }
                else if (value.SubCategoryName == "Others") {
                    $('#ddlSubCategory option:contains("Others")').prop('selected', true);
                   $('#ddlSubCategory').prop('disabled', true);
                    GetProblem();
                }
                else {
                    $('#ddlSubCategory').prop('disabled', false);
                }
            })
        }
    });
}

function GetProblem() {
    var Category = $("#ddlCategory option:selected").val();
    var SubCategory = $("#ddlSubCategory option:selected").val();
    if (Category == undefined || Category == "") {
        alert('Please select Category first.');
        $("option:selected").prop("selected", false)
        return false;
    }
    if (SubCategory == undefined || SubCategory == "") {
        alert('Please select SubCategory first.');
        $("option:selected").prop("selected", false)
        return false;
    }
    var ddlProblem = $("#ddlProblem");
    ddlProblem.empty().append('<option selected="selected" value="" disabled = "disabled">--Select Problem--</option>');

    $.ajax({
        type: "POST",
        url: "/EnterIssue/GetProblemList",
        data: { "Category": Category, "SubCategory": SubCategory },
        success: function (response) {
            var arr = $.parseJSON(response);
            $.each(arr, function (data, value) {

                ddlProblem.append($("<option></option>").val(value.ProblemID).html(value.ProblemName));
            })
        }
    });
}
        
   

function brandClick() {
    if ($(".Branch").prop('checked') == true) {
        $('#trBrand').show();
        $('#trEmail').show();
        $('#TrEmpCode').hide();
        $('#txtEmpCode').text = "";
    }
      
    }
    
   

function ddllocationChange() {
    if ($("#ddllocationlist").val() == "4") {
        $("#ddlCircleList").show();
        $("#txtlocation").hide();
        $("#lblCircle").show();
        $("#ddlCircleList").append("required")
    }
    else if ($("#ddllocationlist").val() == "5") {
        $("#ddlCircleList").hide();
        $("#txtlocation").show();
        $("#lblCircle").show();
        $("#txtlocation").append("required")

    }
    else {
        $("#ddlCircleList").hide();
        $("#txtlocation").hide();
        $("#ddlCircleList").val(-1);
        $("#lblCircle").hide();
    }
}


