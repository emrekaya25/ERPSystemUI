document.addEventListener('DOMContentLoaded', function () {

    document.getElementById('btnAddCategory').addEventListener('click', function (event) {
        let isValid = true;
        const title = document.getElementById('txtCategoryName_Add');

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
    document.getElementById('btnUpdateCategoryDen').addEventListener('click', function (event) {
        let isValid2 = true;
        const title2 = document.getElementById('txtCategoryName_Update');

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
    document.getElementById('btnAddProduct').addEventListener('click', function (event) {
        let isValid3 = true;
        const name = document.getElementById('txtProductName_Add');
        const brand = document.getElementById('txtProductBrandName_Add');
        const desc = document.getElementById('txtProductDescription_Add');
        const cate = document.getElementById('ddlProductCategory_Add');

        // Clear previous invalid states
        [name,brand,desc,cate].forEach(field => {
            field.classList.remove('is-invalid');
        });

        // Validate title
        if (!name.value) {
            name.classList.add('is-invalid');
            isValid3 = false;
        }if (!brand.value) {
            brand.classList.add('is-invalid');
            isValid3 = false;
        }if (!desc.value) {
            desc.classList.add('is-invalid');
            isValid3 = false;
        }if (!cate.value) {
            cate.classList.add('is-invalid');
            isValid3 = false;
        }

        // If form is invalid, prevent submission
        if (!isValid3) {
            event.preventDefault();
        }
    });
    document.getElementById('btnUpdateProductDen').addEventListener('click', function (event) {
        let isValid4 = true;
        const name2 = document.getElementById('txtProductName_Update');
        const brand2 = document.getElementById('txtProductBrandName_Update');
        const desc2 = document.getElementById('txtProductDescription_Update');
        const cate2 = document.getElementById('ddlProductCategory_Update');

        // Clear previous invalid states
        [name2, brand2, desc2, cate2].forEach(field => {
            field.classList.remove('is-invalid');
        });

        // Validate title
        if (!name2.value) {
            name2.classList.add('is-invalid');
            isValid4 = false;
        } if (!brand2.value) {
            brand2.classList.add('is-invalid');
            isValid4 = false;
        } if (!desc2.value) {
            desc2.classList.add('is-invalid');
            isValid4 = false;
        } if (!cate2.value) {
            cate2.classList.add('is-invalid');
            isValid4 = false;
        }

        // If form is invalid, prevent submission
        if (!isValid4) {
            event.preventDefault();
        }
    });
});


function closeModal() {
    $("#modal-Categories").modal("hide");

}
function openProductDetailModal(id, name, categoryid, brandname, description, imageProductFile) {
    $("#modal-ProductDetail").modal("show");
    $("#txtProductId_Update").val(id);
    $("#txtProductName_Update").val(name);
    $("#ddlProductCategory_Update").val(categoryid);
    $("#txtProductBrandName_Update").val(brandname);
    $("#txtProductDescription_Update").val(description);
    $("#txtImageProduct_Update").val(imageProductFile);

}
function openCategoryDetailModal(id, name) {
    $("#secondaryUpdateCategoryModal").modal("show");
    $("#txtCategoryId_Update").val(id);
    $("#txtCategoryName_Update").val(name);

}
$(document).ready(function () {
    var ascending = true;
    var itemsPerPage = 10;
    var currentPage = 1;
    var items = $("#my-table tbody tr");
    var totalItems = items.length;
    var totalPages = Math.ceil(totalItems / itemsPerPage);
    var colSpanCount = $('#my-table tbody tr:first td').length;
    var notFoundMessage = `<tr id="not-found" class="border-bottom"><td class="text-danger text-center fs-5" colspan="${colSpanCount}">Üzgünüz, aradığınız veri bulunamadı.</td></tr>`;
    showPage(1);

    $("#prev-page").click(function () {
        window.scrollTo({
            top: 0,
            behavior: 'smooth'
        });
        if (currentPage > 1) {
            currentPage--;
            showPage(currentPage);
        }
    });

    $("#first-page").click(function () {
        window.scrollTo({
            top: 0,
            behavior: 'smooth'
        });
        currentPage = 1;
        showPage(currentPage);
    });

    $("#next-page").click(function () {
        window.scrollTo({
            top: 0,
            behavior: 'smooth'
        });
        if (currentPage < totalPages) {
            currentPage++;
            showPage(currentPage);
        }
    });

    $("#last-page").click(function () {
        window.scrollTo({
            top: 0,
            behavior: 'smooth'
        });
        currentPage = totalPages;
        showPage(currentPage);
    });

    $('#my-table-search-input').keyup(function () {
        $('#not-found').remove();
        var searchText = $(this).val().toLowerCase();
        var visibleRows = $('#my-table tbody tr').filter(function () {
            var rowText = $(this).text().toLowerCase();
            return rowText.indexOf(searchText) !== -1;
        });
        $('#my-table tbody tr').hide();
        if (visibleRows.length > 0) {
            visibleRows.show();
            console.log(visibleRows.length);
            $('#total-entity').text(visibleRows.length);
        } else {
            $('#total-entity').text(0);
            $('#my-table tbody').append(notFoundMessage);
        }
        items = visibleRows;
        totalItems = items.length;
        totalPages = Math.ceil(totalItems / itemsPerPage);
        if (totalPages == 0) {
            totalPages = 1
        }
        showPage(1);
        updateButtons();
    });

    $('#items-per-page').on('change', function () {
        var selectedValue = $(this).val();
        itemsPerPage = selectedValue;
        totalPages = Math.ceil(totalItems / itemsPerPage);
        if (totalPages == 0) {
            totalPages = 1
        }
        showPage(1);
        updateButtons();
    });

    function showPage(page) {
        $(".page-number").text(currentPage + "/" + totalPages);
        items.hide();
        items.slice((page - 1) * itemsPerPage, page * itemsPerPage).show();
        updateButtons();
    }

    function updateButtons() {
        $("#first-page").toggle(currentPage > 1);
        $("#prev-page").toggle(currentPage > 1);
        $("#next-page").toggle(currentPage < totalPages);
        $("#last-page").toggle(currentPage < totalPages);
    }


});