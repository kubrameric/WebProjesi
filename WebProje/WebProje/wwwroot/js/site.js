// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function SetCulture(selectedValue) {
    var cultere = "/?culture=" + selectedValue + "&ui-culture=" + selectedValue;
    document.getElementById("cultureForm").action = cultere;
    document.getElementById("cultureForm").submit();
}