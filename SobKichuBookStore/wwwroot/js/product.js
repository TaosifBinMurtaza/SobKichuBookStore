var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "name" },
            { "data": "isbn" },
            { "data": "price" },
            { "data": "author" },
            {"data":"category.name"},
           
            {
                "data": "id",
                "render": function (data) {
                    return `
                        
                        <a href="/Admin/Product/Upsert?id=${data}"
                        class="btn btn-primary">  Edit</a>
                        <a href="/Admin/Product/Delete?id=${data}"
                        class="btn btn-danger"> Delete</a>
					</div>
                        `
                }
             
            }
        ]
    });
}