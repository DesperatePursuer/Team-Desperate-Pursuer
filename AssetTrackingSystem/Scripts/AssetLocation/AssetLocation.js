$(document).ready(function () {

    loadData();



    //Search Contact
    $("#search_input").on('keyup', function (e) {
        e.preventDefault();
        $("#mydatatable").dataTable().fnDestroy();
        Search();
    });


    $("#OrganizationId").change(function () {
        $('#organizationBranchId').val = "";
        var id = $('#OrganizationId').val();



        $.ajax({



            url: "/AssetLocation/GetOrganizationBranchByOrganization/" + id,
            type: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",




            success: function (data) {
                // get json data list
                $.each(data, function (key, organizationBranch) {

                    var optionText = "<option value='" + organizationBranch.Id + "'>" + organizationBranch.Name + "</option>";
                    $('#organizationBranchId').append(optionText);
                });


            }


        });

    });




});


function Search() {


    var search_input = $('#search_input').val();

    var jsonData = { "AssetLocation.Name": search_input };


    $('#mydatatable').DataTable({



        ajax: {
            data: JSON.stringify(jsonData),
            contentType: "application/json; charset=utf-8",
            url: "/AssetLocation/Search",
            type: "POST",

            dataSrc: ""

        },

        columns: [
            {
                data: "Name"
            },
            {
                data: "ShortName"
            },
            {
                data: "OrganizationBranchId"
            },
            {
                render: function (data, type, organization) {
                    return "<div class='btn-group'><a href='#' class='btn btn-primary' onclick='return getbyID(" + organization.Id + ")'><i class='icon_plus_alt2'></i></a><a href='#' class='btn btn-danger' onclick='Delete(" + organization.Id + ")'><i class='icon_close_alt2'></i></a></div>"
                }
            }
        ]


    });



}






//Load Data function 2nd wayyyyyyyy  


function loadData() {



    $('#mydatatable').DataTable({




        ajax: {

            url: '/AssetLocation/GetByAjax',
            dataSrc: ""

        },

        columns: [
            {
                data: "Name"
            },
            {
                data: "ShortName"
            },
            {
                data: "OrganizationBranchId"
            },
            {
                render: function (data, type, organization) {
                    return "<div class='btn-group'><a href='#' class='btn btn-primary' onclick='return getbyID(" + organization.Id + ")'><i class='icon_plus_alt2'></i></a><a href='#' class='btn btn-danger' onclick='Delete(" + organization.Id + ")'><i class='icon_close_alt2'></i></a></div>"
                }
            }
        ]


    });


}


//Function for getting the Data Based upon Class
function getbyID(id) {
    $('#AssetLocation_Name').css('border-color', 'lightgrey');

    $('#AssetLocation_Code').css('border-color', 'lightgrey');
    $('#AssetLocation_Description').css('border-color', 'lightgrey');
    $.ajax({
        url: "/AssetLocation/GetbyID/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#OrganizationId').val(result.OrganizationBranch.OrganizationId);
            $('#organizationBranchId').val(result.OrganizationBranchId);
            $('#AssetLocation_Id').val(result.Id);
            $('#AssetLocation_Name').val(result.Name);

            $('#AssetLocation_ShortName').val(result.ShortName);
      


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



//Add Data Function   
function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var obj = {

       
        "OrganizationId": $('#OrganizationId').val(),
        "AssetLocation.OrganizationBranchId": $('#organizationBranchId').val(),
        "AssetLocation.Id": $('#AssetLocation_Id').val(),
        "AssetLocation.Name": $('#AssetLocation_Name').val(),

        "AssetLocation.ShortName": $('#AssetLocation_ShortName').val()
       
    };
    $.ajax({
        url: "/AssetLocation/Create",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.status) {
                clearTextBox();
                $('#message').addClass('alert alert-success').html("<strong>" + result.message + "</strong>").show(200).delay(2500).hide(200);
                $("#mydatatable").dataTable().fnDestroy();
                loadData();


            }


        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }

    var obj = {

        "OrganizationId": $('#OrganizationId').val(),
        "AssetLocation.OrganizationBranchId": $('#organizationBranchId').val(),
        "AssetLocation.Id": $('#AssetLocation_Id').val(),
        "AssetLocation.Name": $('#AssetLocation_Name').val(),

        "AssetLocation.ShortName": $('#AssetLocation_ShortName').val()
    };

    $.ajax({
        url: "/AssetLocation/Edit",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            if (result.status) {


                $('#message').addClass('alert alert-success').html("<strong>" + result.message + "</strong>").show(200).delay(2500).hide(200);
                $("#mydatatable").dataTable().fnDestroy();
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
            url: "/AssetLocation/Delete/" + id,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                $("#mydatatable").dataTable().fnDestroy();
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
    if ($('#OrganizationId').val().trim() == "") {
        $('#OrganizationId').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#OrganizationId').css('border-color', 'lightgrey');
    }
    if ($('#AssetLocation_Name').val().trim() == "") {
        $('#AssetLocation_Name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#AssetLocation_Name').css('border-color', 'lightgrey');
    }

    if ($('#AssetLocation_ShortName').val().trim() == "") {
        $('#AssetLocation_ShortName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#AssetLocation_ShortName').css('border-color', 'lightgrey');
    }
    if ($('#organizationBranchId').val().trim() == "") {
        $('#organizationBranchId').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#organizationBranchId').css('border-color', 'lightgrey');
    }
    return isValid;
}