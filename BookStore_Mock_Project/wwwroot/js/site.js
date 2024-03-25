// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Initialization for ES Users

$(document).ready(function () {
    $('#myTab a').on('click', function (e) {
        e.preventDefault();
        $(this).tab('show');
    });
});

var connection = new signalR.HubConnectionBuilder().withUrl("/signalRServer").build();

connection.on("LoadBooks", function () {
    location.href = '/Admin/Book_Page/Index';
});