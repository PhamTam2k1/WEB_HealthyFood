
var trang = 1;
$(document).ready(function () {
    LoadDsSanPham(trang);

});
// su kien click li cua phan trang

$('#PhanTrang').on("click", "li", function (e) {
    e.preventDefault();
    trang = $(this).text();
    // alert();
    LoadDsSanPham(trang);
})
$(document).on('click', "button[name='view']", function () {
    let idSanPham = $(this).closest('tr').attr('id').trim();

    // alert(idSanPham);
    $.ajax({
        url: '/Admin/SanPham/chiTietSP',
        type: 'get',
        data: {
            id: idSanPham
        },
        success: function (data) {
            data.da.NgaySX = new Date(parseInt(data.da.NgaySX.replace("/Date(", "").replace(")/", ""), 10));
            let x = new Date(data.da.NgaySX);
            let ngaysx = x.getDate() + '/' + (x.getMonth() + 1) + '/' + x.getFullYear();



            $('#tblChiTiet').empty();


            if (data.code == 200) {
                $('#modalChiTietSP').modal();
                let tr = '<tr>';

                tr += '<td>' + data.da.MaSP + '</td>';
                tr += '<td>' + data.da.TenSP + '</td>';
                tr += '<td>' + data.da.MaLoai + '</td>';
                tr += '<td>' + data.da.Gia + '</td>';
                tr += '<td>' + data.da.KhuyenMai + '</td>';
                tr += '<td>' + ngaysx + '</td>';
                tr += '<td>' + data.da.HSD + '</td>';
                tr += '<td>' + data.da.KG + '</td>';
                tr += '<td>' + data.da.Mota + '</td>';
                tr += '<td>' + data.da.SoLuong + '</td>';
                tr += '<td>' + data.da.Active + '</td>';
                tr += '</tr>';

                $('#tblChiTiet').append(tr);


            }
        }
    });
});

let ma;
$(document).on('click', "button[name='Delete']", function () {

    $('#modalXoa').modal();
    ma = $(this).closest('tr').attr('id');


});
$('#btnDelete').click(function () {
    // let maSP = $('#maSP').val().trim();
    $.ajax({
        url: '/Admin/SanPham/xoaSanPham',
        type: 'post',
        data: {
            id: ma
        },
        success: function (data) {

            if (data.code == 200) {

                LoadDsSanPhamCbXoa();
                $('#modalXoa').modal('hide');
            } else {

                $('#modalXoa').modal('hide');
            }

        }


    })
});

function LoadDsSanPhamCbXoa() {
    $.ajax({
        url: '/Admin/SanPham/dsSanPhamCbXoa',
        type: 'get',
        success: function (data) {
            // console.log(data);

            $('#tblSanPhamCbXoa').empty();
            $.each(data.dsSP, function (k, v) {
                let tr = '<tr id="' + v.MaSP + '">';

                tr += '<td>' + v.TenSP + '</td>';
                tr += '<td>' + v.Gia + '</td>';
                tr += '<td>' + v.KhuyenMai + '</td>';
                tr += '<td>' + v.SoLuong + '</td>';

                tr += '<td>';

                tr += '<a  class="btn btn-xs btn-success" href= ' + "/Admin/SanPham/Retrash/" + v.MaSP + '>  <i class="fa-solid fa-arrow-rotate-left"></i> </a >&nbsp';
                // tr += '<button class = "btn btn-xs btn-info" id="btnEdit" " name = "update"><i class="fa-solid fa-pen-to-square"></i></button>&nbsp';
                tr += '<button class = "btn btn-xs btn-info"  name = "Delete"><i class="fa-solid fa-trash-can"></i></button>';



                tr += '</td>';
                tr += '</tr>';
                $('#tblSanPhamCbXoa').append(tr);

            });

        }

    });
}
function LoadDsSanPham(trang) {
    $.ajax({
        url: '/Admin/SanPham/dsSanPham',
        type: 'get',
        data: {
            trang: trang
        },
        success: function (data) {
            // console.log(data);

            $('#tblSanPham').empty();
            $.each(data.dsSP, function (k, v) {
                let tr = '<tr id="' + v.MaSP + '">';
                //tr += '<td>';
                //tr += '<img style="height:100px;width:100px;" src= ' + "/Public/images/SanPham/" + v.Anh + '>';
                //tr += '</td>';
                /* tr += '<td>' + v.MaSP + '</td>';*/
                tr += '<td>' + v.TenSP + '</td>';
                tr += '<td>' + v.Gia + '</td>';
                tr += '<td>' + v.KhuyenMai + '</td>';
                tr += '<td>' + v.SoLuong + '</td>';
                tr += '<td>';
                if (v.Active == 1)

                    tr += '<a  class="btn btn-xs btn-danger" href= ' + "/Admin/SanPham/Status/" + v.MaSP + '>   <i class="fa-solid fa-toggle-on"></i> </a >&nbsp';
                else
                    tr += '<a  class="btn btn-xs btn-success" href= ' + "/Admin/SanPham/Status/" + v.MaSP + '>  <i class="fa-solid fa-toggle-off"></i> </a >&nbsp';
                tr += '</td>';
                tr += '<td>';
                tr += '<button class = "btn btn-xs btn-info" " name = "view"><i class="fa-solid fa-info"></i></button>&nbsp';
                tr += '<a  class="btn btn-xs btn-success" href= ' + "/Admin/SanPham/Edit/" + v.MaSP + '>  <i class="fa-solid fa-pen-to-square" ></i> </a >&nbsp';
                // tr += '<button class = "btn btn-xs btn-info" id="btnEdit" " name = "update"><i class="fa-solid fa-pen-to-square"></i></button>&nbsp';
                //tr += '<button class = "btn btn-xs btn-danger" " name = "delete"><i class="fa-solid fa-trash-can"></i></button>';
                tr += '<a  class="btn btn-xs btn-danger" href= ' + "/Admin/SanPham/DelTrash/" + v.MaSP + '>  <i class="fa-solid fa-trash-can"></i></i> </a >&nbsp';


                tr += '</td>';
                tr += '</tr>';
                $('#tblSanPham').append(tr);

            });
            // Phan trang
            if (data.soTrang > 1) {
                $('#PhanTrang').empty();
                for (let i = 1; i <= data.soTrang; i++) {
                    let li = '<li class="page-item"><a class="page-link" href="#">' + i + '</a></li>';
                    $('#PhanTrang').append(li);
                }
            }
        }

    });
}


