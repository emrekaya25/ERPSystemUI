function updateCompanyModal(companyId, id, name) {
    $("#modal-updateDepartmentModal").modal("show");
    $("#txtCompanyId_Update").val(companyId);
    $("#Id").val(id);
    $("#txtDepartmentName_Update").val(name);

}
function addDepartmentModal(companyId) {
    $("#modal-DepartmentModal").modal("show");
    $("#txtCompanyId_Update").val(companyId);

}
document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('btnAddDepartment').addEventListener('click', function (event) {
        let isValid = true;
        const title = document.getElementById('txtDepartmentName_Add');

        // Clear previous invalid states
        [title].forEach(field => {
            field.classList.remove('is-invalid');
        });

        // Validate title
        if (!title.value) {
            title.classList.add('is-invalid');
            isValid = false;
        }

        // If form is invalid, prevent submission
        if (!isValid) {
            event.preventDefault();
        }
    });
    document.getElementById('btnUpdateDepartment').addEventListener('click', function (event) {
        let isValid2 = true;
        const title2 = document.getElementById('txtDepartmentName_Update');

        // Clear previous invalid states
        [title2].forEach(field => {
            field.classList.remove('is-invalid');
        });

        // Validate title
        if (!title2.value) {
            title2.classList.add('is-invalid');
            isValid2 = false;
        }

        // If form is invalid, prevent submission
        if (!isValid2) {
            event.preventDefault();
        }
    });
});