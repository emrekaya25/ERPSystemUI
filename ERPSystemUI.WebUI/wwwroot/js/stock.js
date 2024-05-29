document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('btnAddStock').addEventListener('click', function (event) {
        let isValid = true;
        const product = document.getElementById('ddlStockProduct_Add');
        const quantity = document.getElementById('txtStockQuantity_Add');
        const unit = document.getElementById('ddlStockUnit_Add');
        const dep = document.getElementById('ddlStockDepartment_Add');

        // Clear previous invalid states
        [product, quantity,unit,dep].forEach(field => {
            field.classList.remove('is-invalid');
        });

        // Validate title
        if (!product.value) {
            product.classList.add('is-invalid');
            isValid = false;
        } if (!quantity.value) {
            quantity.classList.add('is-invalid');
            isValid = false;
        } if (!unit.value) {
            unit.classList.add('is-invalid');
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
    document.getElementById('btnUpdateStockSG').addEventListener('click', function (event) {
        let isValid2 = true;
        const quan = document.getElementById('txtStockQuantity_AddSG');

        // Clear previous invalid states
        [quan].forEach(field => {
            field.classList.remove('is-invalid');
        });

        // Validate title
        if (!quan.value) {
            quan.classList.add('is-invalid');
            isValid2 = false;
        } 
        // If form is invalid, prevent submission
        if (!isValid2) {
            event.preventDefault();
        }
    });
    document.getElementById('btnRemoveStock').addEventListener('click', function (event) {
        let isValid3 = true;
        const user = document.getElementById('ddlStockReciever_Remove');
        const quant = document.getElementById('txtStockQuantity_Remove');

        // Clear previous invalid states
        [user,quant].forEach(field => {
            field.classList.remove('is-invalid');
        });

        // Validate title
        if (!user.value) {
            user.classList.add('is-invalid');
            isValid3 = false;
        }if (!quant.value) {
            quant.classList.add('is-invalid');
            isValid3 = false;
        } 
        // If form is invalid, prevent submission
        if (!isValid3) {
            event.preventDefault();
        }
    });
    document.getElementById('btnUpdateStockJunk').addEventListener('click', function (event) {
        let isValidjunk = true;
        const junk = document.getElementById('txtStockQuantity_Junk');

        // Clear previous invalid states
        [junk].forEach(field => {
            field.classList.remove('is-invalid');
        });

        // Validate title
        if (!junk.value) {
            junk.classList.add('is-invalid');
            isValidjunk = false;
        }
        // If form is invalid, prevent submission
        if (!isValidjunk) {
            event.preventDefault();
        }
    });
});

function submitForm(itemId) {
    document.getElementById("getStockForm_" + itemId).submit();
}
$('.dropdown-menu').on('click', function (e) {
    e.stopPropagation();
});
// Stok güncelleme işlemi
function updateStock(itemId, quantity, unitId, productId) {
    // Stok güncelleme işlemi
}

// Stok çıkış işlemi
function postData(itemId) {
    // Stok çıkış işlemi
}

function addStockQuantity(id, unit, product) {
    $("#modal-UpdateStock").modal("show");
    $("#updateId").val(id);
    $("#updateUnitId").val(unit);
    $("#updateProductId").val(product);
    $("#updateProcessTypeId").val(1);
}
function decreaseStockQuantity(id, unit, product) {
    $("#modal-RemoveStock").modal("show");
    $("#removeId").val(id);
    $("#removeUnitId").val(unit);
    $("#removeProductId").val(product);
    $("#removeProcessTypeId").val(2);
}
function scrappingStockQuantity(id, unit, product) {
    $("#modal-ScrappingStock").modal("show");
    $("#scrappingId").val(id);
    $("#scrappingUnitId").val(unit);
    $("#scrappingProductId").val(product);
    $("#scrappingProcessTypeId").val(4);
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