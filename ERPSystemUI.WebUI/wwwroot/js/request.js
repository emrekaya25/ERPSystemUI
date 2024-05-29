document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('btnAddRequest').addEventListener('click', function (event) {
        let isValid = true;
        const title = document.getElementById('txtRequestTitle_Add');
        const description = document.getElementById('txtRequestDescription_Add');
        const quantity = document.getElementById('txtRequestQuantity_Add');
        const unit = document.getElementById('ddlRequestUnit_Add');
        const product = document.getElementById('ddlRequestProduct_Add');

        // Clear previous invalid states
        [title, description, quantity, unit, product].forEach(field => {
            field.classList.remove('is-invalid');
        });

        // Validate title
        if (!title.value) {
            title.classList.add('is-invalid');
            isValid = false;
        }

        // Validate description
        if (!description.value) {
            description.classList.add('is-invalid');
            isValid = false;
        }

        // Validate quantity
        if (quantity.value <= 0) {
            quantity.classList.add('is-invalid');
            isValid = false;
        }

        // Validate unit
        if (!unit.value) {
            unit.classList.add('is-invalid');
            isValid = false;
        }

        // Validate product
        if (!product.value) {
            product.classList.add('is-invalid');
            isValid = false;
        }

        // If form is invalid, prevent submission
        if (!isValid) {
            event.preventDefault();
        }
    });

    document.getElementById('btnUpdateRequestden').addEventListener('click', function (e) {
        let isValid2 = true;
        const title2 = document.getElementById('txtRequestTitle_Update');
        const description2 = document.getElementById('txtRequestDescription_Update');
        const quantity2 = document.getElementById('txtRequestQuantity_Update');
        const unit2 = document.getElementById('ddlRequestUnit_Update');
        const product2 = document.getElementById('ddlRequestProduct_Update');

        // Clear previous invalid states
        [title2, description2, quantity2, unit2, product2].forEach(field => {
            field.classList.remove('is-invalid');
        });

        // Validate title
        if (!title2.value) {
            title2.classList.add('is-invalid');
            isValid2 = false;
        }

        // Validate description
        if (!description2.value) {
            description2.classList.add('is-invalid');
            isValid2 = false;
        }

        // Validate quantity
        if (quantity2.value <= 0) {
            quantity2.classList.add('is-invalid');
            isValid2 = false;
        }

        // Validate unit
        if (!unit2.value) {
            unit2.classList.add('is-invalid');
            isValid2 = false;
        }

        // Validate product
        if (!product2.value) {
            product2.classList.add('is-invalid');
            isValid2 = false;
        }

        // If form is invalid, prevent submission
        if (!isValid2) {
            e.preventDefault();
        }
    });


});
function openRequestDetailModal(id, title, description, quantity, unitid, productid, requesterid, approverid, statusid) {
    $("#modal-RequestDetail").modal("show");
    $("#txtRequestId_Update").val(id);
    $("#txtRequestTitle_Update").val(title);
    $("#txtRequestDescription_Update").val(description);
    var formattedQuantity = quantity.replace(",", ".");
    $('#txtRequestQuantity_Update').val(formattedQuantity);
    $("#ddlRequestUnit_Update").val(unitid);
    $("#ddlRequestProduct_Update").val(productid);
    $("#ddlRequestRequester_Update").val(requesterid);
    $("#ddlRequestApprover_Update").val(approverid);
    $("#ddlRequestStatus_Update").val(statusid);
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
