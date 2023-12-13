$(document).ready(function () {
    ShowEmployeeData();
});

function ShowEmployeeData() {
    $.ajax({
        url: '/Ajax/EmployeeList',
        type: 'Get',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8;',
        success: function (result,status,xhr) {
            var object = '';
            $.each(result, function (index, item) {

                object += '<tr>';
                object += '<td>' + item.id + '</td>';
                object += '<td>' + item.name + '</td>';
                object += '<td>' + item.city + '</td>';
                object += '<td>' + item.state + '</td>';
                object += '<td>' + item.salary + '</td>';
                object += '<td><a href="#" class="btn btn-primary" onclick="Edit(' + item.id + ')">Edit</a>|<a href="#" class="btn btn-danger" onclick="Delete(' + item.id + ')">Delete</a></td>';
                object += '</tr>';
            });
            $('#table-data').html(object);
        },
        error: function () {
            alert("Data can't fetch");
        }
    });
};

$('#btnAddEmployee').click(function () {
    ClearTextBox();
    $('#EmployeeModal').modal('show');  
    $('#empId').hide();
    $('#AddEmp').css('display', 'block');
    $('#btnUpdate').css('display', 'none');
    $('#ModalHeading').text('Add Employee');
});

function AddEmployee() {
   // debugger
   // var form = $('#AddEmp').closest('form');

    var formData = {
        Name: $('#Name').val(),
        City: $('#City').val(),
        State: $('#State').val(),
        Salary: $('#Salary').val()
    };
    var url = '/Ajax/AddEmployee';
    $.ajax({
        data: formData,
        url: url,
        type: 'POST',
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
      
        success: function (response) {
            if (response.success) { 
                alert(response.message);
                ClearTextBox();
                ShowEmployeeData();       
                HideModalPopUp();          
              
               // window.location.href = '/Ajax/Index';
            }
            else
            {
                alert(response.message);
            }
        },
        error: function () {
            alert('An error occurred during the request.');
        }
    });
}
function HideModalPopUp() {
    $('#EmployeeModal').modal('hide');
};
function ClearTextBox() {
    $('#Name').val('');
    $('#State').val('');
    $('#City').val('');
    $('#Salary').val('');
    $('#EmployeeId').val('');
};

function Edit(id) {
   // debugger
    $.ajax({
        url: '/Ajax/Edit?id=' + id,
        type: 'GET',
        dataType: 'json',
        success: function (response, textStatus, xhr) {
          
            if (xhr.status === 200) {
                $('#EmployeeModal').modal('show');
                $('#EmployeeId').val(response.data.id);
                $('#Name').val(response.data.name);
                $('#State').val(response.data.state);
                $('#City').val(response.data.city);
                $('#Salary').val(response.data.salary);
                $('#AddEmp').css('display', 'none');
                $('#btnUpdate').css('display', 'block');
                $('#ModalHeading').text('Update Employee');

                //$('#AddEmp').hide();
                //$('#btnUpdate').show();

            }
            else if (xhr.status === 404) {
                
                console.log('Employee not found');
            }
            else
            {                
                console.log('Unexpected status code:', xhr.status);
            }
        },
        error: function () {
            alert("Error fetching data");
        }
    });
}

function UpdateEmployee() {
   // debugger
    var formData = {
        Id: $('#EmployeeId').val(),
        Name: $('#Name').val(),
        City: $('#City').val(),
        State: $('#State').val(),
        Salary: $('#Salary').val()
    }
    $.ajax({

        url: '/Ajax/Update',
        type: 'Post',
        dataType: 'json',
        data: formData, 
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success: function (response) {
            if (response.success)
            {
                alert(response.message);
                ClearTextBox();
                ShowEmployeeData();              
                HideModalPopUp();
               // window.location.href = '/Ajax/Index';
            }
            else {
                alert(response.message);
            }
        },
        error: function () {
            alert('An error occurred during the request.');
        }
    });
}

var deleteId;
function Delete(id) {
   
    // Fetch data based on ID
    $.ajax({
        url: '/Ajax/Edit?id=' + id,
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            // Display the fetched data in the modal body
            $('#deleteConfirmationModalBody').html(
                'Are you sure you want to delete this record?<br>' +
                'Employee ID: ' + response.data.id + '<br>' +
                'Name: ' + response.data.name + '<br>' +
                'City: ' + response.data.city + '<br>' +
                'State: ' + response.data.state + '<br>' +
                'Salary: ' + response.data.salary
            );
            deleteId = id;
            $('#deleteConfirmationModal').modal('show');
        },
        error: function () {
            alert("Error fetching data for deletion.");
        }
    });
}
function DeleteConfirmed() {
   
    $('#deleteConfirmationModal').modal('hide');
    $.ajax({
        url: '/Ajax/Delete?id=' + deleteId,
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            if (response.success) {
                alert(response.message);
                ShowEmployeeData();
            } else {
                alert(response.message);
            }
        },
        error: function () {
            alert("Data couldn't be deleted.");
        }
    });
}



   

