document.addEventListener('DOMContentLoaded', function () {

    document.getElementById('btnAddUserRole').addEventListener('click', function (event) {
        let isValid = true;
        const name = document.getElementById('ddlAddUserRoleId_Add');

        // Clear previous invalid states
        [name].forEach(field => {
            field.classList.remove('is-invalid');
        });

        // Validate title
        if (!name.value) {
            name.classList.add('is-invalid');
            isValid = false;
        }
        // If form is invalid, prevent submission
        if (!isValid) {
            event.preventDefault();
        }
    });
    document.getElementById('btnUpdateUser').addEventListener('click', function (event) {
        let isValid = true;
        const name = document.getElementById('txtUserName_Update');
        const email = document.getElementById('txtUserEmailName_Update');
        const no = document.getElementById('txtPhoneNumber_Update');
        const pass = document.getElementById('txtPassword_Update');
        const dep = document.getElementById('ddlDepartment_Update');

        // Clear previous invalid states
        [name,email,no,pass,dep].forEach(field => {
            field.classList.remove('is-invalid');
        });

        // Validate title
        if (!name.value) {
            name.classList.add('is-invalid');
            isValid = false;
        } if (!email.value) {
            email.classList.add('is-invalid');
            isValid = false;
        } if (!no.value) {
            no.classList.add('is-invalid');
            isValid = false;
        } if (!pass.value) {
            pass.classList.add('is-invalid');
            isValid = false;
        } if (!dep.value) {
            dep.classList.add('is-invalid');
            isValid = false;
        }
        // If form is invalid, prevent submission
        if (!isValid) {
            event.preventDefault();
        }
    });
});
function addRoleModal(id) {
    $("#modal-RoleModal").modal("show");
    $("#userId").val(id);

}
function deleteRoleModal(id) {
    $("#modal-deleteRoleModal").modal("show");
    $("#deleteUserId").val(id);

}

function updateUserModal(id, name, email, phone, password, image, department) {
    $("#modal-updateUserModal").modal("show");
    $("#updateUserId").val(id);
    $("#txtUserName_Update").val(name);
    $("#txtUserEmailName_Update").val(email);
    $("#txtPhoneNumber_Update").val(phone);
    $("#txtPassword_Update").val(password);
    $("#txtImage_Update").val(image);
    $("#ddlDepartment_Update").val(department);
}