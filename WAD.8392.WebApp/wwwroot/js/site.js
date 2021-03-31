// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
const navbar = document.querySelector('.navigation');
window.addEventListener('scroll', function () {

    if (pageYOffset > navbar.offsetTop) {
        navbar.style.position = 'fixed';
        navbar.style.width = '100%';
    } else {
        navbar.style.position = 'unset';
    }
})
// Write your JavaScript code.
