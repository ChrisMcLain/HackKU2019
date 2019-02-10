// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
window.onresize = function(event) {
    var issuesContainers= document.getElementsByClassName("issue_container");
    console.log(issuesContainers.length);
    for (var container in issuesContainers){
        var triangle = container.getElementsByClassName("arrow-left")[0];
        var issues = container.getElementsByClassName("issue")[0];
        var heightOfBoxDiv2 =issues.style.height/2;
        triangle.style.borderBottom=heightOfBoxDiv2+"px solid transparent;"
        triangle.style.borderTop=heightOfBoxDiv2+"px solid transparent;"

    }
};