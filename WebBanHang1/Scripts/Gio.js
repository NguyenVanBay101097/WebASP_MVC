var cart = {
    hanhdong: function () {
        cart.tieptucmua();
        cart.capnhat();
        cart.DeleteAll();
        cart.Delete();
    },
    tieptucmua: function () {
        $("#btncontinue").off('click').on('click', function () {
            window.location.href = "/Sanpham";
        });
    },
    capnhat: function () {
        $("#btnupdate").on("click", function () {
            var ds = $(".txtsoluong");
            var list = [];
            $.each(ds, function (i, item) {
                list.push({

                  
                        SoLuong: $(item).val(),
                        ProductID:$(item).data("id")
                    
                });
            });
			$.ajax({
				url: "/Gio/Update",
				data: { cartModel: JSON.stringify(list) },
				dataType: "json",
				type: "POST",
				success: function (res) {
					
					 $('#tongtien').text(res.tongtien);
					
					
					alert("cập nhật giỏ hàng thành công");

				},
				error: function () { alert('tb');}
            });
        });
    },
    DeleteAll: function () {
        $("#btndelete").on("click", function () {
            $.ajax({
                url: '/Gio/DeleteAll',
                dataType: 'Json',
                type: 'POST',
                success: function (res) {
                    if (res.status==true) {
                        window.location.href = "/Danhsachsanpham";
                    }
                }
            });
        });
    },

    Delete: function () {
		$(".XoaSP").on("click", function (e) {
			e.preventDefault();
			e.stopPropagation();
			e.stopImmediatePropagation();
			$.ajax({
				url: '/Gio/Delete',
				dataType: 'Json',
				data: { cartModel: $(this).data("id") },
				type: 'POST',
			success: function (res) {
					var a = $('#tongtien').text(res.tongtien);
				},
			});
			$(this).closest('tr').remove();

        });
      
    }
}
cart.hanhdong();