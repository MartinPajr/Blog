﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function getToken() {
    var token = Cookies.get('token');
    return token;
}
function removeToken() {
    var token = Cookies.get('token');
    Cookies.remove('token', token);
}

