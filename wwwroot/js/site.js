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

        window.location.replace("/Home/delete/" + id)
    } 
}

function getList() {
    if (document.getElementById("listView").style.display == 'none') {
        document.getElementById("listView").style.display = 'block';
        document.getElementById("gridView").style.display = 'none';
       
     }
}
function getGrid() {
    if (document.getElementById("gridView").style.display == 'none') {
        document.getElementById("gridView").style.display = 'block';
        document.getElementById("listView").style.display = 'none';
    }
}

function removeImage() {
  var element = document.getElementById("img");
	element.parentNode.removeChild(element);
  
}