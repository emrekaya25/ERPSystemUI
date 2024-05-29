document.addEventListener('DOMContentLoaded', function () {

    document.getElementById('btnAddUser').addEventListener('click', function (event) {
        let isValid = true;
        const name = document.getElementById('txtUserName_Add');
        const email = document.getElementById('txtUserEmailName_Add');
        const no = document.getElementById('txtPhoneNumber_Add');
        const pass = document.getElementById('txtPassword_Add');
        const dep = document.getElementById('ddlUserDepartment_Add');

        // Clear previous invalid states
        [name, email, no,pass,dep].forEach(field => {
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

    document.addEventListener('DOMContentLoaded', function () {
        var searchInput = document.getElementById('searchInput');
        var userCards = document.querySelectorAll('.user-card');

        searchInput.addEventListener('input', function () {
            var searchText = searchInput.value.toLowerCase().trim();

            userCards.forEach(function (userCard) {
                var userName = userCard.querySelector('.card-title').textContent.toLowerCase();

                if (userName.includes(searchText)) {
                    userCard.style.display = 'block';
                } else {
                    userCard.style.display = 'none';
                }
            });
        });
    });

});
$(function () {
    $("#ara").keyup(function () {
        var deger = $(this).val().toLowerCase();
        $("#AranacakZimbirti .arama").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(deger) > -1);
        });
    });
});