document.addEventListener('DOMContentLoaded', function () {

    document.getElementById('btnAddOffer').addEventListener('click', function (event) {
        let isValid = true;
        const name = document.getElementById('txtOfferSupplierName_Add');
        const desc = document.getElementById('txtOfferDescription_Add');
        const price = document.getElementById('txtOfferPrice_Add');

        // Clear previous invalid states
        [name,desc,price].forEach(field => {
            field.classList.remove('is-invalid');
        });

        // Validate title
        if (!name.value) {
            name.classList.add('is-invalid');
            isValid = false;
        }if (!desc.value) {
            desc.classList.add('is-invalid');
            isValid = false;
        }if (!price.value) {
            price.classList.add('is-invalid');
            isValid = false;
        }

        // If form is invalid, prevent submission
        if (!isValid) {
            event.preventDefault();
        }
    });
    document.getElementById('btnUpdateOffer').addEventListener('click', function (event) {
        let isValid2 = true;
        const name2 = document.getElementById('txtOfferSupplierName_Update');
        const desc2 = document.getElementById('txtOfferDescription_Update');
        const price2 = document.getElementById('txtOfferPrice_Update');

        // Clear previous invalid states
        [name2,desc2,price2].forEach(field => {
            field.classList.remove('is-invalid');
        });

        // Validate title
        if (!name2.value) {
            name2.classList.add('is-invalid');
            isValid2 = false;
        }if (!desc2.value) {
            desc2.classList.add('is-invalid');
            isValid2 = false;
        }if (!price2.value) {
            price2.classList.add('is-invalid');
            isValid2 = false;
        }

        // If form is invalid, prevent submission
        if (!isValid2) {
            event.preventDefault();
        }
    });
});

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
function confirmOffer(id, supplierName, description, price, statusId, approverUserId, requestId) {
    var offer = {
        Id: id,
        SupplierName: supplierName,
        Description: description,
        Price: price,
        StatusId: 2,
        ApproverUserId: userId,
        RequestId: requestId
    };
    $.ajax({
        url: '/UpdateOffer',
        type: 'post',
        data: offer,
        success: function (response) {
            location.reload();
            $("#modal-Offers").modal("show");


        },
        error: function () {
            Swal.fire({
                icon: 'error',
                title: 'Hata',
                text: 'Bir hata oluştu',
            });
        }
    });
}
function rejectOffer(id, supplierName, description, price, statusId, approverUserId, requestId) {
    var offer = {
        Id: id,
        SupplierName: supplierName,
        Description: description,
        Price: price,
        StatusId: 3,
        ApproverUserId: userId,
        RequestId: requestId
    };
    $.ajax({
        url: '/UpdateOffer',
        type: 'post',
        data: offer,
        success: function (response) {
            location.reload();

        },
        error: function () {
            Swal.fire({
                icon: 'error',
                title: 'Hata',
                text: 'Bir hata oluştu',
            });
        }
    });
}
function openOffers(id, name, description, requestId, supplierName, price, statusId, approverUserId, statusName, approverUser) {
    $("#modal-Offers").modal("show");

    if (approverUser == null) {
        approverUser = "------";
    }

    // Yeni bir satır oluştur
    var newRow = "<tr>" +
        "<td>" + id + "</td>" +
        "<td>" + name + "</td>" +
        "<td>" + supplierName + "</td>" +
        "<td>" + description + "</td>" +
        "<td>" + price + "</td>" +
        "<td>" + statusName + "</td>" +
        "<td>" + approverUser + "</td>" +
        "<td>";
    if (statusId == 2) {
        newRow += "<div class='btn btn-success'> Onaylandı</div>";
    }
    else if (statusId == 3) {
        newRow += "<div class='btn btn-danger'> Reddedildi</div>";
    }
    else {
        newRow +="<div class='dropdown'>" +
            "<button class='btn btn-secondary dropdown-toggle' type='button' id='dropdownMenuButton' data-toggle='dropdown' aria-haspopup='true' aria-expanded='false'>" +
            "İşlemler" +
            "</button>"+ "<div class='dropdown-menu' aria-labelledby='dropdownMenuButton'>" +
            `<button id='btnOfferUpdate'
                onclick='updateOffer(${id}, "${supplierName}", "${description} ", ${price}, ${statusId}, ${approverUserId}, ${requestId})' class='btn dropdown-item btn'>Güncelle</button>` +
            `<button id='btnConfirmOffer' onclick='confirmOffer(

            "${id}",
            "${supplierName}",
            "${description}",
            ${price},
            ${statusId},
            ${approverUserId},
            ${requestId}

                )' class='dropdown-item btn'>Onayla</button>` +
            `<button id='btnRejectOffer'
                onclick='rejectOffer(${id}, "${supplierName}", "${description}", ${price}, ${statusId}, ${approverUserId}, ${requestId})' class='dropdown-item btn'>Reddet</button>` +
            "</div>";
    }
        
        newRow += "</div>" +
        "</td>" +
        "</tr>";

    // Yeni satırı tabloya ekle
    $("#offerTable tbody").append(newRow);
}
function updateOffer(id, supplierName, description, price, statusId, approverUserId, requestId) {
    $("#modal-Offers").modal("hide");
    $("#modal-UpdateOffers").modal("show");
    $('#txtOfferSupplierName_Update').val(supplierName);
    $('#txtOfferDescription_Update').val(description);
    $('#txtOfferPrice_Update').val(price);
    $('#updateOfferRequestId').val(requestId);
    $('#updateOfferId').val(id);
    $('#updateOfferApproverUserId').val(approverUserId);
    $('#updateOfferStatusId').val(statusId);

}


function addNewOffer(id) {
    $("#modal-NewOffer").modal("show");
    $("#RequestId").val(id);
}
function postData(id) {
    var rol = {
        Id: 0,
        Name: "string",
        RequestId: id,
        SupplierName: "string",
        Description: "string",
        Price: 0,
        StatusId: 0,
        StatusName: null,
        ApproverUserId: 0,
        ApproverUser: null
    };
    $.ajax({
        url: '/OffersByRequest',
        type: 'post',
        data: rol,
        success: function (response) {
            $("#offerTable tbody").empty();
            if (response.length === 0) {
                Swal.fire({
                    icon: 'warning',
                    title: 'Uyarı',
                    text: 'Henüz Teklif Yok!',
                });
            } else {
                response.forEach(function (offer) {
                    openOffers(offer.id, offer.name, offer.description, offer.requestId, offer.supplierName, offer.price, offer.statusId, offer.approverUserId, offer.statusName, offer.approverUser);
                });
            }

        },
        error: function () {
            Swal.fire({
                icon: 'error',
                title: 'Hata',
                text: 'Bir hata oluştu',
            });
        }
    });
}