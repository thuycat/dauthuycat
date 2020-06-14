//import { isValid } from "../../vendors/iconfonts/ionicons/collection/icon/utils";

$(document).ready( function () {
     loadData();
    
});
 function  loadData() {
    $.ajax({

            url: '/Home/loadData',
            type: 'GET', 
            
            success: function (response) {
                var html = '';
                $.each(response, function (key, val) {
                    html += '<tr>';
                    html += '<td id=\"id \" scope=\"col\">' + val.id + '</td>'
                    html += '<td scope=\"col\">' + val.name + '</td>'
                    html += '<td scope=\"col\">' + val.email + '</td>'
                    html += '<td scope=\"col\">' + '<button type=\"button\" onclick=\"return getID(' + val.id + ')\"  class=\"edit\" data-toggle=\"modal\" data-target=\"#myModal1\"  ><i class=\"far fa-edit\"></i></button>' + '&nbsp' + '<button  type=\"button\" onclick=\"return getById(' + val.id + ')\"><i class=\"fas fa-trash-alt delete\"></i></button>' + '</td>'
                    html += '</tr>'
                  
                });
                $('#idTblData').html(html);

            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
    })
     
    
}

function getID(id) {
        $.ajax({
            url: 'api/UserAction/getID/'+ id,
        type: "GET",
        contentType: 'application/json; charset= uft-8',
            success: function (result) {
                $("#nameUser").text(result.id);
                $("#Edname").val(result.name);
                $("#Edemail").val(result.email);
                $("#EditMyUser").addEventListener('click', myFunction(result.id));
            },

        error: function (err) {
            alert("lỗi");
        }
    });
}
$(document).ready(function () {
    //addUser
    $("#addUser").click(function () {
        var User = {

            Name: $("#name").val(),
            Email: $("#email").val(),
            PassWord: $("#password").val()
        };
        $.ajax({
            url: 'api/UserAction/AddUser',
            type: 'POST',
            data: JSON.stringify(User),
            contentType: 'application/json; charset= utf-8',
            dataType: 'json',
            success: function (result) {
                alert("thêm thành công !");
                $('#myModal').modal('toggle');
                loadData();

            },
            error: function (err) {
                alert("lỗi");
            }
        });
    });
});
$(document).ready(function () {
    //EditUser
    $("#EditMyUser").click(function () {
        debugger
        var user = {
            id: $("#nameUser").text(),
            name: $("#Edname").val(),
            email: $("#Edemail").val(),
            password: $("#Edpassword").val()
        };
        $.ajax({
            url: 'api/UserAction/EditUser',
            type: "POST",
            data: JSON.stringify(user),
            contentType: 'application/json; charset= utf-8',
            dataType: 'json',
            success: function (result) {
                alert("cập nhật thành công");
                $('#myModal1').modal('toggle');
                loadData();
            },
            error: function (err) {
                alert("lỗi");
            }
        })
    });
});
// detete 
function getById(id) {
    alert("bạn chắc chắn xóa"),
        $.ajax({
            url: 'api/UserAction/Delete/' + id,
            type: "GET",
            contentType: 'application/json; charset= uft-8',

            success: function (result) {
                alert("xóa thành công !");
                loadData();
            },
            error: function (err) {
                alert("lỗi");
            }
        })
}
$(document).ready(function () {
    //delete User
    
});

