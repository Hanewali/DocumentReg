// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// $( function() {
// } );

var element = document.getElementsByClassName("field-validation-error")[0]
if(element != undefined){
    element.classList.add("alert")
    element.classList.add("alert-danger")    
}

