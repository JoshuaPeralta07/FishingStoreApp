var dataTable;
$(function () {
    var url = window.location.search;
    if (url.includes("rods")) {
        CategoryFilter("rods");
    }
    else {
        if (url.includes("lines")) {
            CategoryFilter("lines");
        }
        else {
            if (url.includes("lures")) {
                CategoryFilter("lures");
            }
            else {
                if (url.includes("storage")) {
                    CategoryFilter("storage");
                }
                else {
                    CategoryFilter("all");
                }
            }
        }
    }


    loadDataTable();
})

function loadDataTable() {
    $('#tblData').DataTable({
        scrollX: true,
        autoWidth: false,
        "ajax": { url: '/admin/product/getall' },
        "columns": [
            { data: 'productName', "width": "15%" },
            { data: 'description', "width": "20%" },
            { data: 'price' },
            { data: 'productCode', "width": "10%" },
            { data: 'stocks', "width": "1%" },
            { data: 'category.name', "width": "10%" },
            {
                data: 'productId',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/Admin/Product/Upsert?id=${data}" class="btn btn-primary mx-1" style="border-radius:10px"><i class="bi bi-pencil-square"></i> Edit</a>
                    <a onClick=Delete('/admin/product/delete/${data}') class="btn btn-danger mx-1" style="border-radius:10px"><i class="bi bi-trash3-fill"></i> Delete</a>
                    </div>`
                },
                "width": "25%"
            }
        ]
    })
}

function Delete(url) {
    Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#1a1a1a",
            cancelButtonColor: "#d9534f",
            confirmButtonText: "Yes, delete it!"
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: url,
                    type: 'DELETE',
                    success: function (data) {
                        $('#tblData').DataTable().ajax.reload();
                        toastr.success(data.message);
                    }
                })
            }
        });
}

function CategoryFilter(category) {
    $("#category").on('click', function () {
        $.ajax({
            url: '/admin/product/getall?category=' + category
        })
    });
}