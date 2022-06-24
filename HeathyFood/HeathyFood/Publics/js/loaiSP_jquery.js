
$(document).ready(function () {
    LoadDsLoaiSanPham();
});
$('#btnSubmitAdd').click(function () {

    let tentl = $('#tentl').val().trim();
    let check = 1;
   
    if (tentl == "") {
        document.getElementById("tentl_err").innerHTML = "Bạn chưa nhập tên thể loại"; check = 0;
    }
    else {
        document.getElementById("tentl_err").innerHTML = "";
    }
    if (check == 1)
        $.ajax({
            url: '/Admin/LoaiSanPham/addLoaiSanPham',
            type: 'post',
            data: {
               
                tentl: tentl
            },
            success: function (data) {
                if (data.code == 200) {
                 
                    $('#tentl').val('');
                    LoadDsLoaiSanPham();


                } else {
                    $('#modalHienThiLoi').modal();
                }

            }
        });
});
$(document).on('click', "button[name='update']", function () {
    $('#modaltitle').text("Sửa thể loại");
    $('.matl').show();
    $('#matl').prop('readonly', true);
    $('#btnSubmitAdd').hide();
    $('#btnSubmitEdit').show();
    let idLoaiSanPham = $(this).closest('tr').attr('id').trim();
    //  alert(idLoaiSanPham);

    $.ajax({
        url: '/Admin/LoaiSanPham/chiTietTL',
        type: 'get',
        data: {
            id: idLoaiSanPham
        },
        success: function (data) {

            if (data.code == 200) {



                $('#matl').val(data.da.MaLoai);
                $('#matl').prop('readonly', true);
                $('#tentl').val(data.da.TenLoai);
                //$('#tendoan1').prop('readonly', false);
                $('#modalLoaiSanPham').modal();


            }


        }
    });
});
$(document).on('click', "button[name='view']", function () {
    let idLoaiSanPham = $(this).closest('tr').attr('id').trim();
    //alert(idDoAn);
    $.ajax({
        url: '/Admin/LoaiSanPham/chiTietTL',
        type: 'get',
        data: {
            id: idLoaiSanPham
        },
        success: function (data) {
            $('#tblChiTiet').empty();
            if (data.code == 200) {
                $('#modalChiTietTL').modal();
                let tr = '<tr>';
                tr += '<td>' + data.da.MaLoai + '</td>';
                tr += '<td>' + data.da.TenLoai + '</td>';


                tr += '</tr>';

                $('#tblChiTiet').append(tr);


            }
        }
    });
});
$('#btnSubmitEdit').click(function () {
    let matl = $('#matl').val().trim();
    let tentl = $('#tentl').val().trim();
    //alert("edit");
    $.ajax({
        url: '/Admin/LoaiSanPham/editLoaiSanPham',
        type: 'post',
        data: {
            matl: matl,
            tentl: tentl

        },
        success: function (data) {
            if (data.code == 200) {
                LoadDsLoaiSanPham();
                $('#matl').val('');
                $('#tentl').val('');
                $('#modalLoaiSanPham').modal('hide');


            }



        }
    });
});
$('#btnAdd').click(function () {
    $('#modaltitle').text("Thêm mới thể loại");

    $('.matl').hide();
    $('#tentl').val('');
    $('#btnSubmitEdit').hide();
    $('#btnSubmitAdd').show();
    
    $('#modalLoaiSanPham').modal();

});

let ma;
$(document).on('click', "button[name='delete']", function () {

    $('#modalXoa').modal();
    ma = $(this).closest('tr').attr('id').trim();


});
$('#btnDelete').click(function () {
    // let matl = $('#matl').val().trim();
    $.ajax({
        url: '/Admin/LoaiSanPham/xoaLoaiSanPham',
        type: 'post',
        data: {
            id: ma
        },
        success: function (data) {
            if (data.code == 200) {

                LoadDsLoaiSanPham();
                $('#modalXoa').modal('hide');
            } else {
                $('#modalHienThiLoiKhiXoa').modal();
                $('#modalXoa').modal('hide');
            }

        }


    })
});
function LoadDsLoaiSanPham() {
    $.ajax({
        url: '/Admin/LoaiSanPham/dsLoaiSanPham',
        type: 'get',
        success: function (data) {
            /*  console.log(data);*/
            //if (data.code == 200) {
            $('#tblLoaiSanPham').empty();
            $.each(data.dsTL, function (k, v) {
                let tr = '<tr id="' + v.MaLoai + '">'
                tr += '<td>' + v.MaLoai + '</td>';
                tr += '<td>' + v.TenLoai + '</td>';

                tr += '<td>';
                tr += '<button class = "btn btn-xs btn-info" " name = "view"><i class="fa-solid fa-info"></i></button>&nbsp';
                tr += '<button class = "btn btn-xs btn-success" id="btnEdit" " name = "update"><i class="fa-solid fa-pen-to-square"></i></button>&nbsp';
                tr += '<button class = "btn btn-xs btn-danger" " name = "delete"><i class="fa-solid fa-trash-can"></i></button>';
                tr += '</td>';
                tr += '</tr>';
                $('#tblLoaiSanPham').append(tr);

            });
            //}
        }

    });
}

