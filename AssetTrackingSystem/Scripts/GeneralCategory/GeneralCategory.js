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

    var jsonData = { "GeneralCategory.Name": search_input };
   

    $('#mydatatable').DataTable({



            ajax: {
                data: JSON.stringify(jsonData),
                contentType: "application/json; charset=utf-8",
                url: "/GeneralCategory/Search",
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

            url: '/GeneralCategory/GetByAjax',
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
    $('#GeneralCategory_Name').css('border-color', 'lightgrey');
    
    $('#GeneralCategory_Code').css('border-color', 'lightgrey');
    $('#GeneralCategory_Description').css('border-color', 'lightgrey');
    $.ajax({
        url: "/GeneralCategory/GetbyID/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#GeneralCategory_Id').val(result.Id);
            $('#GeneralCategory_Name').val(result.Name);
            
            $('#GeneralCategory_Code').val(result.Code);
            $('#GeneralCategory_Description').val(result.Description);


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

        "GeneralCategory.Id": $('#GeneralCategory_Id').val(),
        "GeneralCategory.Name": $('#GeneralCategory_Name').val(),
       
        "GeneralCategory.Code": $('#GeneralCategory_Code').val(),
        "GeneralCategory.Description": $('#GeneralCategory_Description').val()
    };
    $.ajax({
        url: "/GeneralCategory/Create",
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

        "GeneralCategory.Id": $('#GeneralCategory_Id').val(),
        "GeneralCategory.Name": $('#GeneralCategory_Name').val(),

        "GeneralCategory.Code": $('#GeneralCategory_Code').val(),
        "GeneralCategory.Description": $('#GeneralCategory_Description').val()
    };

    $.ajax({
        url: "/GeneralCategory/Edit",
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
            url: "/GeneralCategory/Delete/" + id,
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
    if ($('#GeneralCategory_Name').val().trim() == "") {
        $('#GeneralCategory_Name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#GeneralCategory_Name').css('border-color', 'lightgrey');
    }

    if ($('#GeneralCategory_Code').val().trim() == "") {
        $('#GeneralCategory_Code').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#GeneralCategory_Code').css('border-color', 'lightgrey');
    }
    if ($('#GeneralCategory_Description').val().trim() == "") {
        $('#GeneralCategory_Description').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#GeneralCategory_Description').css('border-color', 'lightgrey');
    }
    return isValid;
}