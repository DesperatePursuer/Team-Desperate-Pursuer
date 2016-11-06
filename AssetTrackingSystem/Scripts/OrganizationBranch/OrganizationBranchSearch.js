$(document).ready(function () {

    loadData();



});


function loadData() {



    $('#tblOrganizationBranchSearch').DataTable({




        ajax: {

            url: '/OrganizationBranch/GetByAjax',
            dataSrc: ""

        },

        columns: [
             {
                 data: "OrganizationId"
             },
            {
                data: "Name"
            },
            {
                data: "ShortName"
            },
           
            {
                render: function (data, type, organization) {
                    return "<div class='btn-group'><a href='#' class='btn btn-primary' onclick='return getbyID(" + organization.Id + ")'><i class='icon_plus_alt2'></i></a><a href='#' class='btn btn-danger' onclick='Delete(" + organization.Id + ")'><i class='icon_close_alt2'></i></a></div>"
                }
            }
        ]

        


    });


}



//Add Data Function   
function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var organization = {

        "OrganizationBranch.Id": $('#OrganizationBranch_Id').val(),

        "OrganizationBranch.OrganizationId": $('#OrganizationBranch_OrganizationId').val(),

        "OrganizationBranch.Name": $('#OrganizationBranch_Name').val(),
       
        "OrganizationBranch.ShortName": $('#OrganizationBranch_ShortName').val()
    };
    $.ajax({
        url: "/OrganizationBranch/Create",
        data: JSON.stringify(organization),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.status) {
                clearTextBox();
                $('#message').addClass('alert alert-success').html("<strong>" + result.message + "</strong>").show(200).delay(2500).hide(200);
                $("#tblOrganizationBranchSearch").dataTable().fnDestroy();
                loadData();


            }


        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}



//Function for getting the Data Based upon Organization
function getbyID(id) {
    $('#OrganizationBranch_OrganizationId').css('border-color', 'lightgrey');
    $('#OrganizationBranch_Name').css('border-color', 'lightgrey');
    $('#OrganizationBranch_ShortName').css('border-color', 'lightgrey');
    
    $.ajax({
        url: "/OrganizationBranch/GetbyID/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#OrganizationBranch_Id').val(result.Id);
            $('#OrganizationBranch_OrganizationId').val(result.OrganizationId);
           
            $('#OrganizationBranch_Name').val(result.Name);
            $('#OrganizationBranch_ShortName').val(result.ShortName);
           


            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}



//Function for clearing the textboxes
function clearTextBox() {
    $('#organization_form')[0].reset();
    $('#btnUpdate').hide();
    $('#btnAdd').show();
}


function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }

    var organization = {


        "OrganizationBranch.Id": $('#OrganizationBranch_Id').val(),

        "OrganizationBranch.OrganizationId": $('#OrganizationBranch_OrganizationId').val(),

        "OrganizationBranch.Name": $('#OrganizationBranch_Name').val(),

        "OrganizationBranch.ShortName": $('#OrganizationBranch_ShortName').val()
    };

    $.ajax({
        url: "/OrganizationBranch/Edit",
        data: JSON.stringify(organization),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            if (result.status) {


                $('#message').addClass('alert alert-success').html("<strong>" + result.message + "</strong>").show(200).delay(2500).hide(200);
                $("#tblOrganizationBranchSearch").dataTable().fnDestroy();
                loadData();



            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });


}

function Delete(id) {

    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/OrganizationBranch/Delete/" + id,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                $("#tblOrganizationBranchSearch").dataTable().fnDestroy();
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }

}


//Valdidation using jquery  
function validate() {
    var isValid = true;
    if ($('#OrganizationBranch_OrganizationId').val().trim() == "") {
        $('#OrganizationBranch_OrganizationId').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#OrganizationBranch_OrganizationId').css('border-color', 'lightgrey');
    }
    if ($('#OrganizationBranch_Name').val().trim() == "") {
        $('#OrganizationBranch_Name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#OrganizationBranch_Name').css('border-color', 'lightgrey');
    }
    if ($('#OrganizationBranch_ShortName').val().trim() == "") {
        $('#OrganizationBranch_ShortName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#OrganizationBranch_ShortName').css('border-color', 'lightgrey');
    }

    return isValid;
}