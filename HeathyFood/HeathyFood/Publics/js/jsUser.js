
$(document).ready(function () {
    LoadDsUser();
});
$(document).on('click', "button[name='update']", function () {
    $('#modaltitleUser').text("Sửa User");
    $('#btnSubmitAdd').hide();
    $('#btnSubmitEdit').show();
    let idUser = $(this).closest('tr').attr('id').trim();

    $.ajax({
        url: '/Admin/User/chiTietUser',
        type: 'get',
        data: {
            id: idUser
        },
        success: function (data) {
            if (data.da.NgaySinh != null)
                data.da.NgaySinh = new Date(parseInt(data.da.NgaySinh.replace("/Date(", "").replace(")/", ""), 10));
            else data.da.NgaySinh = new Date('2000/01/01');

            let x = new Date(data.da.NgaySinh);
            let day = "";
            let month = "";
            if (x.getDate() < 10) day = "0" + x.getDate();
            else day = x.getDate();
            if ((x.getMonth() + 1) < 10) month = "0" + (x.getMonth() + 1);
            else month = (x.getMonth() + 1);
            let ngay = x.getFullYear() + "-" + month + "-" + day;
          


            if (data.code == 200) {

                $('#userid').val(data.da.UserID);
                $('#userid').prop('readonly', true);
                $('#firstname').val(data.da.FirstName);
                $('#diachi').val(data.da.DiaChi);
                $('#sdt').val(data.da.Sdt);
                $('#email').val(data.da.Email);
                document.getElementById("ngaysinh").value = ngay;
                //$('#ngaysinh').val(ngay);
                $('#lastname').val(data.da.LastName);
                   $('#isAdmin').val(data.da.isAdmin);
                $('#modalUser').modal();


            } else {
                alert("hi");
            }


        }
    });
});
$('#btnSubmitEdit').click(function () {
    let userid = $('#userid').val().trim();
    let firstname = $('#firstname').val().trim();
    let diachi = $('#diachi').val().trim();
    let sdt = $('#sdt').val().trim();
    let ngaysinh = $('#ngaysinh').val().trim();
    let email = $('#email').val().trim();

    let lastname = $('#lastname').val().trim();
    
    let isAdmin = $('#isAdmin').val().trim();

    //alert("edit");
    $.ajax({
        url: '/Admin/User/editUser',
        type: 'post',
        data: {
            userid: userid,
            firstname: firstname,
            lastname: lastname,
            diachi: diachi,
            sdt: sdt,
            email: email,  
            ngaysinh: ngaysinh,
            isAdmin: isAdmin

        },
        success: function (data) {
            if (data.code == 200) {
                LoadDsUser();
                $('#userid').val('');
                $('#firstname').val('');
                $('#diachi').val('');
                $('#sdt').val('');
                $('#ngaysinh').val('');
                $('#lastname').val('');
                $('#isAdmin').val('');
                $('#modalUser').modal('hide');


            }



        }
    });
});
$(document).on('click', "button[name='view']", function () {
    let idUser = $(this).closest('tr').attr('id').trim();

    // alert(idUser);
    $.ajax({
        url: '/Admin/User/chiTietUser',
        type: 'get',
        data: {
            id: idUser
        },
        dataType: "json",
        success: function (data) {
            data.da.NgaySinh = new Date(parseInt(data.da.NgaySinh.replace("/Date(", "").replace(")/", ""), 10));
            let x = new Date(data.da.NgaySinh);
            let ngay = x.getDate() + '/' + (x.getMonth() + 1) + '/' + x.getFullYear();

            $('#tblChiTiet').empty();
            gioi = data.da.GioiTinh;
            if (data.code == 200) {
                $('#modalChiTietUser').modal();
                let tr = '<tr>';
                tr += '<td>' + data.da.UserID + '</td>';
                tr += '<td>' + data.da.LastName + '</td>';
                tr += '<td>' + data.da.FirstName + '</td>';
               
                tr += '<td>' + data.da.DiaChi + '</td>';
                tr += '<td>' + data.da.Sdt + '</td>';
                tr += '<td>' + data.da.Email + '</td>';
                tr += '<td>' + ngay + '</td>';
                tr += '<td>' + data.da.isAdmin + '</td>';
                tr += '</tr>';

                $('#tblChiTiet').append(tr);


            }
        }
    });
});

$(document).on('click', "button[name='delete']", function () {

    $('#modalXoa').modal();
    ma = $(this).closest('tr').attr('id');


});
$('#btnDelete').click(function () {
    // let iduser = $('#iduser').val().trim();
    $.ajax({
        url: '/Admin/User/xoaUser',
        type: 'post',
        data: {
            id: ma
        },
        success: function (data) {

            if (data.code == 200) {

                LoadDsUser();
                $('#modalXoa').modal('hide');
            } else {
                $('#modalHienThiLoiKhiXoa').modal();
                $('#modalXoa').modal('hide');
            }

        }


    })
});
function LoadDsUser() {
    $.ajax({
        url: '/Admin/User/dsUser',
        type: 'get',
        success: function (data) {
            /*  console.log(data);*/
            $('#tblUser').empty();
            $.each(data.dsUser, function (k, v) {
                let tr = '<tr id="' + v.UserID + '">'
                
                tr += '<td>' + v.LastName + '</td>';
                tr += '<td>' + v.FirstName + '</td>';
               
                tr += '<td style="width:100px;height:100px;">' + v.DiaChi + '</td>';
                tr += '<td>' + v.Sdt + '</td>';
                tr += '<td>' + v.Email + '</td>';
                tr += '<td>' + v.isAdmin + '</td>';

                tr += '<td>';
                tr += '<button class = "btn btn-xs btn-info" " name = "view"><i class="fa-solid fa-info"></i></button>&nbsp';
                tr += '<button class = "btn btn-xs btn-info" id="btnEdit" " name = "update"><i class="fa-solid fa-pen-to-square"></i></button>&nbsp';
                tr += '<button class = "btn btn-xs btn-info" " name = "delete"><i class="fa-solid fa-trash-can"></i></button>';
                tr += '</td>';
                tr += '</tr>';
                $('#tblUser').append(tr);

            });

        }

    });
}
