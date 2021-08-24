// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function() {
    checkLogin("#account");
});

//validaciones
var emailPattern = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,4}$";

function checkInput(idInput, pattern) {
    return $(idInput).val().match(pattern) ? true : false;
}

function checkLongInput(idInput) {
    return $(idInput).val().length >= 5 ? true : false;
}

function enableSubmit(idForm) {
    $(idForm + " input.submit").removeAttr("disabled");
}

function disableSubmit(idForm) {
    $(idForm + " input.submit").attr("disabled", "disabled");
}

function checkLogin(idForm) {
    $(idForm + " *").on("change keydown",
        function() {
            if (checkInput("#Email", emailPattern) &&
                checkLongInput("#Password")) {
                enableSubmit(idForm);
            } else {
                disableSubmit(idForm);
            }
        });
}

$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})