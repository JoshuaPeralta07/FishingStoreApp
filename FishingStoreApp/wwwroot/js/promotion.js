var dataTable;
$(function () {
    loadDataTable();
})

function loadDataTable() {
    $('#tblData').DataTable({
        scrollX: true,
        autoWidth: false,
        "ajax": { url: '/Admin/Promotion/GetAll' },
        "columns": [
            { data: 'promoName', "width": "15%" },
            { data: 'discount', "width": "20%" },
            { data: 'expiryDate', "width": "20%" },
            { data: 'isActive', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/Admin/Promotion/Upsert?id=${data}" class="btn btn-primary mx-1" style="border-radius:10px"><i class="bi bi-pencil-square"></i> Edit</a>
                    <a onClick=Delete('/admin/promotion/delete/${data}') class="btn btn-danger mx-1" style="border-radius:10px"><i class="bi bi-trash3-fill"></i> Delete</a>
                    </div>`
                },
                "width": "15%"
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
