$(document).ready(function () {

    loadData();
    GetCategoriesByGeneralCategory();
    GetSubCategoryByCategory();
    GetProductModelBySubCategory();



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



            url: "/AssetPurchaseEntry/GetCategoriesByGeneralCategory/" + id,
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



            url: "/AssetPurchaseEntry/GetSubCategoryByCategory/" + id,
            type: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",




            success: function (data) {
                // get json data list
                $.each(data, function (key, category) {

                    var optionText = "<option value='" + category.Id + "'>" + category.Name + "</option>";
                    $('#SubcategoryId').append(optionText);
                });


            }


        });

    });

}
function GetProductModelBySubCategory() {

    $("#SubcategoryId").change(function () {
        var id = $('#SubcategoryId').val();



        $.ajax({



            url: "/AssetPurchaseEntry/GetProductModelBySubCategory/" + id,
            type: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",




            success: function (data) {
                // get json data list
                $.each(data, function (key, category) {

                    var optionText = "<option value='" + category.Id + "'>" + category.Name + "</option>";
                    $('#ProductModelId').append(optionText);
                });


            }


        });

    });

}

function Search() {


    var search_input = $('#search_input').val();

    var jsonData = { "AssetPurchaseEntry.PurchaseDate": search_input };


    $('#mydatatable').DataTable({



        ajax: {
            data: JSON.stringify(jsonData),
            contentType: "application/json; charset=utf-8",
            url: "/AssetPurchaseEntry/Search",
            type: "POST",

            dataSrc: ""

        },

        columns: [
            {
                data: "PurchaseDate"
            },
            {
                data: "Vendor"
            },
            {
                data: "ProductModelId"
            },
            {
                data: "Price"
            },
            {
                data: "Qty"
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

            url: '/AssetPurchaseEntry/GetByAjax',
            dataSrc: ""

        },

        columns: [
            {
                data: "PurchaseDate"
            },
            {
                data: "Vendor"
            },
            {
                data: "ProductModelId"
            },
            {
                data: "Price"
            },
            {
                data: "Qty"
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
    $('#AssetPurchaseEntry_Name').css('border-color', 'lightgrey');

    $('#AssetPurchaseEntry_Code').css('border-color', 'lightgrey');
    $('#AssetPurchaseEntry_Description').css('border-color', 'lightgrey');
    $.ajax({
        url: "/AssetPurchaseEntry/GetbyID/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            $('#GeneralCategoryId').val(result.GeneralCategoryId);
            $('#CategoryId').val(result.CategoryId);
            $('#SubcategoryId').val(result.SubcategoryId);
            $('#ProductModelId').val(result.ProductModelId);
           
            $('#AssetPurchaseEntry_Id').val(result.Id);
            $('#AssetPurchaseEntry_PurchaseDate').val(result.PurchaseDate);
            $('#AssetPurchaseEntry_Vendor').val(result.Vendor);
            

            $('#AssetPurchaseEntry_WarrantyPeriod').val(result.WarrantyPeriod);
            $('#AssetPurchaseEntry_Price').val(result.Price); 
            $('#AssetPurchaseEntry_Qty').val(result.Qty);


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
        "AssetPurchaseEntry.CategoryId": $('#CategoryId').val(),
        "AssetPurchaseEntry.subcategoryId": $('#subcategoryId').val(),
        "AssetPurchaseEntry.ProductModelId": $('#ProductModelId').val(),

        "AssetPurchaseEntry.Id": $('#AssetPurchaseEntry_Id').val(),
        "AssetPurchaseEntry.PurchaseDate": $('#AssetPurchaseEntry_PurchaseDate').val(),

        "AssetPurchaseEntry.Vendor": $('#AssetPurchaseEntry_Vendor').val(),
        "AssetPurchaseEntry.WarrantyPeriod": $('#AssetPurchaseEntry_WarrantyPeriod').val(),
        "AssetPurchaseEntry.Price": $('#AssetPurchaseEntry_Price').val(),
        "AssetPurchaseEntry.Qty": $('#AssetPurchaseEntry_Qty').val()
    };
    $.ajax({
        url: "/AssetPurchaseEntry/Create",
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
        "AssetPurchaseEntry.CategoryId": $('#CategoryId').val(),
        "AssetPurchaseEntry.subcategoryId": $('#subcategoryId').val(),
        "AssetPurchaseEntry.ProductModelId": $('#ProductModelId').val(),

        "AssetPurchaseEntry.Id": $('#AssetPurchaseEntry_Id').val(),
        "AssetPurchaseEntry.PurchaseDate": $('#AssetPurchaseEntry_PurchaseDate').val(),

        "AssetPurchaseEntry.Vendor": $('#AssetPurchaseEntry_Vendor').val(),
        "AssetPurchaseEntry.WarrantyPeriod": $('#AssetPurchaseEntry_WarrantyPeriod').val(),
        "AssetPurchaseEntry.Price": $('#AssetPurchaseEntry_Price').val(),
        "AssetPurchaseEntry.Qty": $('#AssetPurchaseEntry_Qty').val()
    };

    $.ajax({
        url: "/AssetPurchaseEntry/Edit",
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
            url: "/AssetPurchaseEntry/Delete/" + id,
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
    if ($('#SubcategoryId').val().trim() == "") {
        $('#SubcategoryId').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#SubcategoryId').css('border-color', 'lightgrey');
    }
    if ($('#ProductModelId').val().trim() == "") {
        $('#ProductModelId').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ProductModelId').css('border-color', 'lightgrey');
    }
    if ($('#AssetPurchaseEntry_PurchaseDate').val().trim() == "") {
        $('#AssetPurchaseEntry_PurchaseDate').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#AssetPurchaseEntry_PurchaseDate').css('border-color', 'lightgrey');
    }

    if ($('#AssetPurchaseEntry_Vendor').val().trim() == "") {
        $('#AssetPurchaseEntry_Vendor').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#AssetPurchaseEntry_Vendor').css('border-color', 'lightgrey');
    }
    if ($('#AssetPurchaseEntry_WarrantyPeriod').val().trim() == "") {
        $('#AssetPurchaseEntry_WarrantyPeriod').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#AssetPurchaseEntry_WarrantyPeriod').css('border-color', 'lightgrey');
    }
    if ($('#AssetPurchaseEntry_Price').val().trim() == "") {
        $('#AssetPurchaseEntry_Price').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#AssetPurchaseEntry_Price').css('border-color', 'lightgrey');
    }
    if ($('#AssetPurchaseEntry_Qty').val().trim() == "") {
        $('#AssetPurchaseEntry_Qty').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#AssetPurchaseEntry_Qty').css('border-color', 'lightgrey');
    }
    return isValid;
}