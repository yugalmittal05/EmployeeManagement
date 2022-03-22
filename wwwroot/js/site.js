// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function getResponce(id) {
    var text = "Are You Sure to Delete it?";
    if (confirm(text) == true) {

        window.location.replace("/Home/delete/" + id)
    } 
}

function getList() {
    if (document.getElementById("listView").style.display == 'none') {
        document.getElementById("listView").style.display = 'block';
        document.getElementById("gridView").style.display = 'none';
    }
    else {
        document.getElementById("listView").style.display = 'none';
        document.getElementById("gridView").style.display = 'Block';
    }
}

function removeImage(id) {
    var text = "Are you sure to Delete this Image?";
    if (confirm(text) == true) {
        window.location.replace("/Home/removeImage/" + id);
    }
}