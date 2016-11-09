$(document).ready(function () {

    loadData();

    

    //Search Contact
    $("#Organization_code_search").on('keyup', function (e) {
        e.preventDefault();
        SearchOrganization();
    });







});


function SearchOrganization() {

    var organization_seach_input = $('#Organization_code_search').val();

    var jsonData = { "Oraganization.Code": organization_seach_input };


    $.ajax({
        url: "/Organization/Search",
        type: "POST",

        data: JSON.stringify(jsonData),
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            if (data.length > 0) {
                var $data = $('<table ></table>').addClass('table table-striped table-advance table-hover');
                var header = "<thead><tr><th><i class='icon_profile'></i> Name</th><th><i class='icon_info_alt'></i> Short Name</th><th><i class='icon_document'></i> Code</th><th><i class='icon_pin_alt'></i> AssetLocation</th><th><i class='icon_cogs'></i> Action</th></tr></thead>";
                $data.append(header);

                $.each(data, function (i, row) {
                    var $row = $('<tr/>');
                    $row.append($('<td/>').html(row.Name));
                    $row.append($('<td/>').html(row.ShortName));
                    $row.append($('<td/>').html(row.Code));
                    $row.append($('<td/>').html(row.Location));
                    $row.append($('<td/>').html("<div class='btn-group'><a href='#' class='btn btn-primary' onclick='return getbyID(" + row.Id + ")'><i class='icon_plus_alt2'></i></a><a href='#' class='btn btn-danger' onclick='return Delete(" + row.Id + ")'><i class='icon_close_alt2'></i></a></div>"));
                    $data.append($row);
                });

                $('#update_panel').html($data);
            }
            else {
                var $noData = $('<div/>').html('No Data Found!');
                $('#update_panel').html($noData);
            }

        }

    });

}

function LoadOrganization() {
    $('#update_panel').html('Loading Data........');

    $.ajax({
        url: '/Organization/GetByAjax',
        type: 'GET',
        dataType: 'json',
        success: function (d) {
            if (d.length > 0) {
                var $data = $('<table id="tblOrganizationHome"></table>').addClass('table table-striped table-advance table-hover');
                var header = "<thead><tr><th><i class='icon_profile'></i> Name</th><th><i class='icon_info_alt'></i> Short Name</th><th><i class='icon_document'></i> Code</th><th><i class='icon_pin_alt'></i> AssetLocation</th><th><i class='icon_cogs'></i> Action</th></tr></thead>";
                $data.append(header);

                $.each(d, function (i, row) {
                    var $row = $('<tr/>');
                    $row.append($('<td/>').html(row.Name));
                    $row.append($('<td/>').html(row.ShortName));
                    $row.append($('<td/>').html(row.Code));
                    $row.append($('<td/>').html(row.Location));
                    $row.append($('<td/>').html("<div class='btn-group'><a href='#' class='btn btn-primary' onclick='return getbyID(" + row.Id + ")'><i class='icon_plus_alt2'></i></a><a href='#' class='btn btn-danger' onclick='Delete(" + row.Id + ")'><i class='icon_close_alt2'></i></a></div>"));
                    $data.append($row);
                });

                $('#update_panel').html($data);
            }
            else {
                var $noData = $('<div/>').html('No Data Found!');
                $('#update_panel').html($noData);
            }
        },
        error: function () {
            alert('Error! Please try again.');
        }
    });

}




//Load Data function 2nd wayyyyyyyy  


function loadData() {



    $('#tblOrganizationHome').DataTable({


 
        
        ajax: {
            
            url: '/Organization/GetByAjax',
            dataSrc: ""

        },

        columns: [
            {
                data:"Name"
            },
            {
                data: "ShortName"
            },
            {
                data: "Code"
            },
            {
                data: "AssetLocation"
            },
            {
               render: function(data, type, organization) {
                   return "<div class='btn-group'><a href='#' class='btn btn-primary' onclick='return getbyID(" + organization.Id + ")'><i class='icon_plus_alt2'></i></a><a href='#' class='btn btn-danger' onclick='Delete(" + organization.Id + ")'><i class='icon_close_alt2'></i></a></div>"
               }
            }
        ]


    });

   
}


//Function for getting the Data Based upon Organization
function getbyID(id) {
    $('#Oraganization_Name').css('border-color', 'lightgrey');
    $('#Oraganization_ShortName').css('border-color', 'lightgrey');
    $('#Oraganization_Code').css('border-color', 'lightgrey');
    $('#Oraganization_Location').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Organization/GetbyID/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#Oraganization_Id').val(result.Id);
            $('#Oraganization_Name').val(result.Name);
            $('#Oraganization_ShortName').val(result.ShortName);
            $('#Oraganization_Code').val(result.Code);
            $('#Oraganization_Location').val(result.Location);


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
    var organization = {

        "Oraganization.Id": $('#Oraganization_Id').val(),
        "Oraganization.Name": $('#Oraganization_Name').val(),
        "Oraganization.ShortName": $('#Oraganization_ShortName').val(),
        "Oraganization.Code": $('#Oraganization_Code').val(),
        "Oraganization.Location": $('#Oraganization_Location').val()
    };
    $.ajax({
        url: "/Organization/Create",
        data: JSON.stringify(organization),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.status) {
                clearTextBox();
                $('#message').addClass('alert alert-success').html("<strong>" + result.message + "</strong>").show(200).delay(2500).hide(200);
                $("#tblOrganizationHome").dataTable().fnDestroy();
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

    var organization = {

        "Oraganization.Id": $('#Oraganization_Id').val(),
        "Oraganization.Name": $('#Oraganization_Name').val(),
        "Oraganization.ShortName": $('#Oraganization_ShortName').val(),
        "Oraganization.Code": $('#Oraganization_Code').val(),
        "Oraganization.Location": $('#Oraganization_Location').val()
    };

    $.ajax({
        url: "/Organization/Edit",
        data: JSON.stringify(organization),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            if (result.status) {


                $('#message').addClass('alert alert-success').html("<strong>" + result.message + "</strong>").show(200).delay(2500).hide(200);
                $("#tblOrganizationHome").dataTable().fnDestroy();
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
            url: "/Organization/Delete/" + id,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                $("#tblOrganizationHome").dataTable().fnDestroy();
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
    if ($('#Oraganization_Name').val().trim() == "") {
        $('#Oraganization_Name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Organization_Name').css('border-color', 'lightgrey');
    }
    if ($('#Oraganization_ShortName').val().trim() == "") {
        $('#Oraganization_ShortName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Oraganization_ShortName').css('border-color', 'lightgrey');
    }
    if ($('#Oraganization_Code').val().trim() == "") {
        $('#Oraganization_Code').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Oraganization_Code').css('border-color', 'lightgrey');
    }
    if ($('#Oraganization_Location').val().trim() == "") {
        $('#Oraganization_Location').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Oraganization_Location').css('border-color', 'lightgrey');
    }
    return isValid;
}