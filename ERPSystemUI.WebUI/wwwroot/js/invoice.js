document.addEventListener('DOMContentLoaded', function () {

    document.getElementById('btnFaturaAdd').addEventListener('click', function (event) {
        let isValid = true;
        const invoiceno = document.getElementById('txtInvoiceNo_Add');
        const compname = document.getElementById('txtCompanyName_Add');
        const compno = document.getElementById('txtCompanyNo_Add');
        const compmail = document.getElementById('txtCompanyMail_Add');
        const supname = document.getElementById('txtSupplierName_Add');
        const supmail = document.getElementById('txtSupplierMail_Add');
        const supno = document.getElementById('txtSupplierPhone_Add');
        const invdate = document.getElementById('txtInvoiceDate_Add');
        const supaddress = document.getElementById('txtSupplierAddress_Add');

        // Clear previous invalid states
        [invoiceno, compname, compno, compmail, supname, supmail, supno, invdate, supaddress].forEach(field => {
            field.classList.remove('is-invalid');
        });

        // Validate title
        if (!invoiceno.value) {
            invoiceno.classList.add('is-invalid');
            isValid = false;
        } if (!compname.value) {
            compname.classList.add('is-invalid');
            isValid = false;
        } if (!compno.value) {
            compno.classList.add('is-invalid');
            isValid = false;
        } if (!compmail.value) {
            compmail.classList.add('is-invalid');
            isValid = false;
        } if (!supname.value) {
            supname.classList.add('is-invalid');
            isValid = false;
        } if (!supmail.value) {
            supmail.classList.add('is-invalid');
            isValid = false;
        } if (!supno.value) {
            supno.classList.add('is-invalid');
            isValid = false;
        } if (!invdate.value) {
            invdate.classList.add('is-invalid');
            isValid = false;
        } if (!supaddress.value) {
            supaddress.classList.add('is-invalid');
            isValid = false;
        }

        // If form is invalid, prevent submission
        if (!isValid) {
            event.preventDefault();
            firstModal();
        }
        if (isValid) {
            postInvoice();
        }
    });
});


var copyCount = 0;
function createCopies() {
    var rowDiv = document.querySelector('.deneme');
    var originalDivs = document.querySelectorAll('.denemelerde');

    if (copyCount < originalDivs.length) {
        var clone = originalDivs[copyCount].cloneNode(true);
        rowDiv.appendChild(clone);
        copyCount++; // Sayaç arttırıldı
    }
}
function removeCopy() {
    var rowDiv = document.querySelector('.deneme');
    var copies = rowDiv.querySelectorAll('.denemelerde');

    if (copyCount > 0) {
        rowDiv.removeChild(copies[copyCount - 1]); // Son eklenen kopyayı kaldır
        copyCount--; // Sayaç azaltıldı
    }
}
function closeModal() {
    $("#modal-Categories").modal("hide");

}
function firstModal() {
    $("#secondModal").modal("hide");
    $("#staticBackdrop").modal("show");
}
function secondModalShow() {

    $("#staticBackdrop").modal("hide");
    $("#secondModal").modal("show");
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
// function confirmDelete() {
//     if (confirm("Silmek istediğinizden emin misiniz?")) {
//         $('#deleteProductForm').submit();
//     }
// }
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



var invoiceDetailsList = [];

function addInvoiceDetail() {
    var productName = document.getElementById("txtProductName_Add").value;
    var productDescription = document.getElementById("txtProductDescription_Add").value;
    var quantity = document.getElementById("txtQuantity_Add").value;
    var unitPrice = document.getElementById("txtUnitPrice_Add").value;

    // Herhangi bir input boş ise ekleme yapma
    if (productName === "" || productDescription === "" || quantity === "" || unitPrice === "") {
        // Eğer bir veya daha fazla input boş ise kullanıcıya uyarı verebilirsiniz
        Swal.fire({
            title: "Uyarı!",
            text: "Lütfen Boşlukları Doldurunuz.",
            icon: "warning"
        });
        return; // Fonksiyondan çık
    }

    var newDetail = {
        Id: 0,
        InvoiceId: 0,
        ProductName: productName,
        ProductDescription: productDescription,
        Quantity: quantity,
        UnitPrice: unitPrice
    };

    // Fatura detaylarını listeye ekle
    invoiceDetailsList.push(newDetail);

    // Fatura detaylarını ekledikten sonra input alanlarını temizle
    document.getElementById("txtProductName_Add").value = "";
    document.getElementById("txtProductDescription_Add").value = "";
    document.getElementById("txtQuantity_Add").value = "";
    document.getElementById("txtUnitPrice_Add").value = "";

    // Tagbox oluştur ve içine ürün adını ekle
    var tagbox = document.createElement("div");
    tagbox.className = "tagbox";
    tagbox.textContent = "Ürün Ad:  " + productName + " / " + "Ürün Açıklama:  " + productDescription + " / " + "Miktar:  " + quantity + " / " + "Birim Fiyat:  " + unitPrice;

    // Silme butonunu oluştur
    var deleteButton = document.createElement("button");
    deleteButton.textContent = "X";
    deleteButton.className = "btn btn-danger btn-sm";
    deleteButton.style = "margin: 5px;";
    deleteButton.onclick = function () {
        // Tagbox'ı listeden kaldır
        var index = invoiceDetailsList.findIndex(detail => detail.ProductName === productName);
        if (index !== -1) {
            invoiceDetailsList.splice(index, 1);
        }
        // Tagbox'ı görsel olarak sil
        tagbox.remove();
    };

    // Tagbox'a silme butonunu ekle
    tagbox.appendChild(deleteButton);

    // invoiceTagBox'a tagbox'ı ekle
    document.getElementById("invoiceTagBox").appendChild(tagbox);
    console.log("Ege burdaydı!");
}

function postInvoice() {


    var invoiceDTO = {
        Id: 0,
        CompanyId: $("#txtCompanyId").val(),
        InvoiceDate: $("#txtInvoiceDate_Add").val(),
        InvoiceNo: $("#txtInvoiceNo_Add").val(),
        SupplierName: $("#txtSupplierName_Add").val(),
        SupplierPhone: $("#txtSupplierPhone_Add").val(),
        SupplierAddress: $("#txtSupplierAddress_Add").val(),
        SupplierMail: $("#txtSupplierMail_Add").val(),
        CompanyName: $("#txtCompanyName_Add").val(),
        CompanyPhone: $("#txtCompanyNo_Add").val(),
        CompanyMail: $("#txtCompanyMail_Add").val(),
        InvoiceDetails: invoiceDetailsList
    };

    $.ajax({
        url: '/Invoice/AddInvoice',
        type: 'POST',
        data: invoiceDTO,
        success: function (response) {
            // İstek başarılıysa burası çalışır
            location.reload();
        },
        error: function (xhr, status, error) {
            // İstek başarısız olduğunda burası çalışır
            Swal.fire({
                title: "Hata!",
                html: "Bir Hata oluştu daha sonra tekrar deneyiniz!",
                icon: "error"
            });
        }
    });


}
