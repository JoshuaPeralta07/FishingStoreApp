var dataTable;
$(function () {
    loadDataTable();
})

function loadDataTable() {
    $('#tblData').DataTable({
        scrollX: true,
        autoWidth: false,
        "ajax": { url: '/admin/order/getall' },
        "columns": [
            { data: 'id', "width": "10%" },
            { data: 'name', "width": "20%" },
            { data: 'phoneNumber', "width": "15%" },
            { data: 'applicationUser.email', "width": "20%" },
            { data: 'orderStatus', "width": "20%"},
            { data: 'orderTotal', "width": "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/Admin/Order/Details?orderId=${data}" class="btn btn-primary mx-1" style="border-radius:10px"><i class="bi bi-pencil-square"></i></a>
                    </div>`
                },
                "width": "25%"
            }
        ]
    })
}


