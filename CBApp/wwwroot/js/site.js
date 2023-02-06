// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Store the toggle button on the navbar
const toggleButton = document.getElementById("toggle-button");
// Store the lists of links inside the navbar
const mainNavList = document.getElementById("navbar-list");
const loginList = document.getElementById("login-list");

// Add an event listener to the toggle (tri-bar) menu button that toggles the link visibility on and off
toggleButton.addEventListener("click", () => {
    mainNavList.classList.toggle("active");
    loginList.classList.toggle("active");
})
