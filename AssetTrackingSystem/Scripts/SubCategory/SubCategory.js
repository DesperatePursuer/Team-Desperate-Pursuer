$(document).ready(function () {

    loadData();



    //Search Contact
    $("#search_input").on('keyup', function (e) {
        e.preventDefault();
        $("#mydatatable").dataTable().fnDestroy();
        Search();
    });





    $("#GeneralCategoryId").change(function () {
        var id = $('#GeneralCategoryId').val();

        

        $.ajax({
            


            url: "/SubCategory/GetCategoriesByGeneralCategory/" + id,
            type: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",




            success: function (data) {
                // get json data list
                $.each(data, function (key, category) {

                    var optionText = "<option value='" + category.Id + "'>" + category.Name + "</option>";
                    $('#categoryId').append(optionText);
                });


            }


        });

    });




});


function Search() {


    var search_input = $('#search_input').val();

    var jsonData = { "SubCategory.Name": search_input };


    $('#mydatatable').DataTable({



        ajax: {
            data: JSON.stringify(jsonData),
            contentType: "application/json; charset=utf-8",
            url: "/SubCategory/Search",
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

            url: '/SubCategory/GetByAjax',
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
    $('#SubCategory_Name').css('border-color', 'lightgrey');

    $('#SubCategory_Code').css('border-color', 'lightgrey');
    $('#SubCategory_Description').css('border-color', 'lightgrey');
    $.ajax({
        url: "/SubCategory/GetbyID/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            
            $('#categoryId').val(result.CategoryId);
            $('#SubCategory_Id').val(result.Id);
            $('#SubCategory_Name').val(result.Name);

            $('#SubCategory_Code').val(result.Code);
            $('#SubCategory_Description').val(result.Description);


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
        "SubCategory.CategoryId": $('#categoryId').val(),
        "SubCategory.Id": $('#SubCategory_Id').val(),
        "SubCategory.Name": $('#SubCategory_Name').val(),

        "SubCategory.Code": $('#SubCategory_Code').val(),
        "SubCategory.Description": $('#SubCategory_Description').val()
    };
    $.ajax({
        url: "/SubCategory/Create",
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
        "SubCategory.CategoryId": $('#categoryId').val(),
        "SubCategory.Id": $('#SubCategory_Id').val(),
        "SubCategory.Name": $('#SubCategory_Name').val(),

        "SubCategory.Code": $('#SubCategory_Code').val(),
        "SubCategory.Description": $('#SubCategory_Description').val()
    };

    $.ajax({
        url: "/SubCategory/Edit",
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
            url: "/SubCategory/Delete/" + id,
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
    if ($('#categoryId').val().trim() == "") {
        $('#categoryId').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#categoryId').css('border-color', 'lightgrey');
    }
    if ($('#SubCategory_Name').val().trim() == "") {
        $('#SubCategory_Name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#SubCategory_Name').css('border-color', 'lightgrey');
    }

    if ($('#SubCategory_Code').val().trim() == "") {
        $('#SubCategory_Code').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#SubCategory_Code').css('border-color', 'lightgrey');
    }
    if ($('#SubCategory_Description').val().trim() == "") {
        $('#SubCategory_Description').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#SubCategory_Description').css('border-color', 'lightgrey');
    }
    return isValid;
}