$(document).ready(function () {

    loadData();



    //Search Contact
    $("#search_input").on('keyup', function (e) {
        e.preventDefault();
        $("#mydatatable").dataTable().fnDestroy();
        Search();
    });







});


function Search() {


    var search_input = $('#search_input').val();

    var jsonData = { "Category.Name": search_input };


    $('#mydatatable').DataTable({



        ajax: {
            data: JSON.stringify(jsonData),
            contentType: "application/json; charset=utf-8",
            url: "/Category/Search",
            type: "POST",

            dataSrc: ""

        },

        columns: [
            {
                data: "Name"
            },
            {
                data: "Code"
            },
            {
                data: "Description"
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

            url: '/Category/GetByAjax',
            dataSrc: ""

        },

        columns: [
            {
                data: "Name"
            },
            {
                data: "Code"
            },
            {
                data: "Description"
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
    $('#Category_Name').css('border-color', 'lightgrey');

    $('#Category_Code').css('border-color', 'lightgrey');
    $('#Category_Description').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Category/GetbyID/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#Category_GeneralCategoryId').val(result.GeneralCategoryId);
            $('#Category_Id').val(result.Id);
            $('#Category_Name').val(result.Name);

            $('#Category_Code').val(result.Code);
            $('#Category_Description').val(result.Description);


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

        "Category.GeneralCategoryId": $('#Category_GeneralCategoryId').val(),
        "Category.Id": $('#Category_Id').val(),
        "Category.Name": $('#Category_Name').val(),

        "Category.Code": $('#Category_Code').val(),
        "Category.Description": $('#Category_Description').val()
    };
    $.ajax({
        url: "/Category/Create",
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

        "Category.GeneralCategoryId": $('#Category_GeneralCategoryId').val(),
        "Category.Id": $('#Category_Id').val(),
        "Category.Name": $('#Category_Name').val(),

        "Category.Code": $('#Category_Code').val(),
        "Category.Description": $('#Category_Description').val()
    };

    $.ajax({
        url: "/Category/Edit",
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
            url: "/Category/Delete/" + id,
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
    if ($('#Category_GeneralCategoryId').val().trim() == "") {
        $('#Category_GeneralCategoryId').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Category_GeneralCategoryId').css('border-color', 'lightgrey');
    }
    if ($('#Category_Name').val().trim() == "") {
        $('#Category_Name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Category_Name').css('border-color', 'lightgrey');
    }

    if ($('#Category_Code').val().trim() == "") {
        $('#Category_Code').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Category_Code').css('border-color', 'lightgrey');
    }
    if ($('#Category_Description').val().trim() == "") {
        $('#Category_Description').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Category_Description').css('border-color', 'lightgrey');
    }
    return isValid;
}