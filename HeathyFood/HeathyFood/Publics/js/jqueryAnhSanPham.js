var trang = 1;
$(document).ready(function () {
    LoadDsAnh(trang);

});

// su kien click li cua phan trang

$('#PhanTrang').on("click", "li", function (e) {
    e.preventDefault();
    trang = $(this).text();
    // alert();
    LoadDsAnh(trang);
   

})

$(document).on('click', "button[name='view']", function () {
    let idAnh = $(this).closest('tr').attr('id').trim();

    // alert(idAnh);
    $.ajax({
        url: '/Admin/AnhSanPham/chiTietanh',
        type: 'get',
        data: {
            id: idAnh
        },
        dataType: "json",
        success: function (data) {
            $('#modalChiTietAnh').modal();
            $('#tblChiTiet').empty();
            $('#img').empty();

            if (data.code == 200) {

                let anh = '<img class="d-block mx-auto" style="height:300px;width:250px;" src=' + "/Publics/images/SanPham/" + data.da.TenAnh + '>';
                let tr = '<tr>';

                tr += '<td>' + data.da.MaAnh + '</td>';
                tr += '<td>' + data.da.MaSP + '</td>';
                tr += '<td>' + data.da.TenAnh + '</td>';
                tr += '<td>' + data.da.URL + '</td>';
                tr += '<td>' + data.da.isMain + '</td>';
                tr += '</tr>';

                $('#tblChiTiet').append(tr);

                $('#img').append(anh);
            }
        }
    });
});


let ma;
$(document).on('click', "button[name='delete']", function () {

    $('#modalXoa').modal();
    ma = $(this).closest('tr').attr('id');


});

function LoadDsAnh(trang) {
    $.ajax({
        url: '/Admin/AnhSanPham/dsAnh',
        type: 'get',
        data: {
            trang:trang
        },
        success: function (data) {
            // console.log(data);

            $('#tblAnh').empty();
            $.each(data.dsanh, function (k, v) {
                let tr = '<tr id="' + v.MaAnh + '">';
                tr += '<td>';
                tr += '<img style="height:100px;width:100px;" src= ' + "/Publics/images/SanPham/" + v.TenAnh + '>';
                tr += '</td>';

                tr += '<td>' + v.TenSP + '</td>';
                tr += '<td>' + v.TenAnh + '</td>';
                tr += '<td>' + v.isMain + '</td>';
                tr += '<td>';
                tr += '<button class = "btn btn-xs btn-info" " name = "view"><i class="fa-solid fa-info"></i></button>&nbsp';
                tr += '<a  class="btn btn-xs btn-success" href= ' + "/Admin/AnhSanPham/Edit/" + v.MaAnh + '>  <i class="fa-solid fa-pen-to-square" ></i> </a >&nbsp';
                // tr += '<button class = "btn btn-xs btn-info" id="btnEdit" " name = "update"><i class="fa-solid fa-pen-to-square"></i></button>&nbsp';
                tr += '<button class = "btn btn-xs btn-danger" " name = "delete"><i class="fa-solid fa-trash-can"></i></button>';


                tr += '</td>';
                tr += '</tr>';
                $('#tblAnh').append(tr);

            });
            // Phan trang
            if (data.soTrang > 1) {
                $('#PhanTrang').empty();
                for (let i = 1; i <= data.soTrang; i++) {
                    let li = '<li class="page-item"><a class="page-link" href="#">'+i+'</a></li>';
                    $('#PhanTrang').append(li);
                }
            }
        }

    });

}

function LoadDsAnhSauDel() {
    $.ajax({
        url: '/Admin/AnhSanPham/dsAnh',
        type: 'get',
        data: {
            trang: trang
        },
        success: function (data) {
            // console.log(data);

            $('#tblAnh').empty();
            $.each(data.dsanh, function (k, v) {
                let tr = '<tr id="' + v.MaAnh + '">';
                tr += '<td>';
                tr += '<img style="height:100px;width:100px;" src= ' + "/Publics/images/SanPham/" + v.TenAnh + '>';
                tr += '</td>';

                tr += '<td>' + v.TenSP + '</td>';
                tr += '<td>' + v.TenAnh + '</td>';
                tr += '<td>' + v.isMain + '</td>';
                tr += '<td>';
                tr += '<button class = "btn btn-xs btn-info" " name = "view"><i class="fa-solid fa-info"></i></button>&nbsp';
                tr += '<a  class="btn btn-xs btn-success" href= ' + "/Admin/AnhSanPham/Edit/" + v.MaAnh + '>  <i class="fa-solid fa-pen-to-square" ></i> </a >&nbsp';
                // tr += '<button class = "btn btn-xs btn-info" id="btnEdit" " name = "update"><i class="fa-solid fa-pen-to-square"></i></button>&nbsp';
                tr += '<button class = "btn btn-xs btn-danger" " name = "delete"><i class="fa-solid fa-trash-can"></i></button>';


                tr += '</td>';
                tr += '</tr>';
                $('#tblAnh').append(tr);

            });
            
        }

    });

}
$('#btnDelete').click(function () {
    // let maAnh = $('#maAnh').val().trim();
    $.ajax({
        url: '/Admin/AnhSanPham/xoaAnhSanPham',
        type: 'post',
        data: {
            id: ma
        },
        success: function (data) {

            if (data.code == 200) {

                LoadDsAnhSauDel();
                $('#modalXoa').modal('hide');
            } else {

                $('#modalXoa').modal('hide');
            }

        }


    })
});