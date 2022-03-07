// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

    function getid() {
        var id = prompt("enter id");
        //location.replace("https://localhost:44392/Home/index");
       
}
function getResponce(id) {
    var text = "Are You Sure to Delete it?";
    if (confirm(text) == true) {
        window.location.replace("https://localhost:44392/Home/delete/" + id)
    } 
}