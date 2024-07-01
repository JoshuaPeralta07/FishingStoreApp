
$(function () {
    loadDataTable();
})

function loadDataTable() {
    $('#tblData').DataTable({
        scrollX: true,
        autoWidth: false,
        "ajax": { url: '/admin/user/getall' },
        "columns": [
            { data: 'name', "width": "20%" },
            { data: 'email', "width": "15%" },
            { data: 'phoneNumber', "width": "20%" },
            { data: 'role', "width": "20%"},
            {
                data: { id: 'id', lockoutEnd: 'lockoutEnd'},
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();

                    if (lockout > today) {
                        return `
                            <div class="text-center">
                                <a onclick=LockUnlock('${data.id}') class="btn btn-danger text-white" style="cursor:pointer; width: 200px; border-radius:10px">
                                    <i class="bi bi-lock-fill"></i> Lock
                                </a>   
                            </div>
                        `
                    }
                    else {
                        return `
                             <div class="text-center">
                                <a onclick=LockUnlock('${data.id}') class="btn btn-success text-white" style="cursor:pointer; width:200px; border-radius:10px">
                                    <i class="bi bi-unlock-fill"></i> Unlock
                                </a>
                            </div>
                        `
                    }

                    
                },
                "width": "30%"
            }
        ]
    })
}

function LockUnlock(id) {
    $.ajax({
        type: "POST",
        url: '/Admin/User/LockUnlock',
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message),
                $('#tblData').DataTable().ajax.reload();
            }
        }
    });
}


