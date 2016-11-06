$(document).ready(function () {

    loadData();
    GetCategoriesByGeneralCategory();
    GetSubCategoryByCategory();



    //Search Contact
    $("#search_input").on('keyup', function (e) {
        e.preventDefault();
        $("#mydatatable").dataTable().fnDestroy();
        Search();
    });




});


function GetCategoriesByGeneralCategory() {
    
    $("#GeneralCategoryId").change(function () {
        var id = $('#GeneralCategoryId').val();



        $.ajax({



            url: "/ProductModel/GetCategoriesByGeneralCategory/" + id,
            type: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",




            success: function (data) {
                // get json data list
                $.each(data, function (key, category) {

                    var optionText = "<option value='" + category.Id + "'>" + category.Name + "</option>";
                    $('#CategoryId').append(optionText);
                });


            }


        });

    });

}

function GetSubCategoryByCategory() {

    $("#CategoryId").change(function () {
        var id = $('#CategoryId').val();



        $.ajax({



            url: "/ProductModel/GetSubCategoryByCategory/" + id,
            type: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",




            success: function (data) {
                // get json data list
                $.each(data, function (key, category) {

                    var optionText = "<option value='" + category.Id + "'>" + category.Name + "</option>";
                    $('#subcategoryId').append(optionText);
                });


            }


        });

    });

}


function Search() {


    var search_input = $('#search_input').val();

    var jsonData = { "ProductModel.Name": search_input };


    $('#mydatatable').DataTable({



        ajax: {
            data: JSON.stringify(jsonData),
            contentType: "application/json; charset=utf-8",
            url: "/ProductModel/Search",
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

            url: '/ProductModel/GetByAjax',
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
    $('#ProductModel_Name').css('border-color', 'lightgrey');

    $('#ProductModel_Code').css('border-color', 'lightgrey');
    $('#ProductModel_Description').css('border-color', 'lightgrey');
    $.ajax({
        url: "/ProductModel/GetbyID/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            $('#GeneralCategoryId').val(result.SubCategory.Category.GeneralCategoryId);
            $('#CategoryId').val(result.SubCategory.CategoryId);
            $('#subcategoryId').val(result.SubCategoryId);
            $('#ProductModel_Id').val(result.Id);
            $('#ProductModel_Name').val(result.Name);

            $('#ProductModel_Code').val(result.Code);
            $('#ProductModel_Description').val(result.Description);


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

            "GeneralCategoryId": $('#GeneralCategoryId').val(),
            "CategoryId": $('#CategoryId').val(),
            "ProductModel.subcategoryId": $('#subcategoryId').val(),

        "ProductModel.Id": $('#ProductModel_Id').val(),
        "ProductModel.Name": $('#ProductModel_Name').val(),

        "ProductModel.Code": $('#ProductModel_Code').val(),
        "ProductModel.Description": $('#ProductModel_Description').val()
    };
    $.ajax({
        url: "/ProductModel/Create",
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

        "GeneralCategoryId": $('#GeneralCategoryId').val(),
        "CategoryId": $('#CategoryId').val(),
        "ProductModel.subcategoryId": $('#subcategoryId').val(),
        "ProductModel.Id": $('#ProductModel_Id').val(),
        "ProductModel.Name": $('#ProductModel_Name').val(),

        "ProductModel.Code": $('#ProductModel_Code').val(),
        "ProductModel.Description": $('#ProductModel_Description').val()
    };

    $.ajax({
        url: "/ProductModel/Edit",
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
            url: "/ProductModel/Delete/" + id,
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



    if ($('#GeneralCategoryId').val().trim() == "") {
        $('#GeneralCategoryId').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#GeneralCategoryId').css('border-color', 'lightgrey');
    }
    if ($('#CategoryId').val().trim() == "") {
        $('#CategoryId').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#CategoryId').css('border-color', 'lightgrey');
    }
    if ($('#ProductModel_Name').val().trim() == "") {
        $('#ProductModel_Name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ProductModel_Name').css('border-color', 'lightgrey');
    }

    if ($('#ProductModel_Code').val().trim() == "") {
        $('#ProductModel_Code').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ProductModel_Code').css('border-color', 'lightgrey');
    }
    if ($('#ProductModel_Description').val().trim() == "") {
        $('#ProductModel_Description').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ProductModel_Description').css('border-color', 'lightgrey');
    }
    return isValid;
}