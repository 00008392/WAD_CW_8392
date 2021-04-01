
const navbar = document.querySelector('.navigation');
window.addEventListener('scroll', function () {

    if (pageYOffset > navbar.offsetTop) {
        navbar.style.position = 'fixed';
        navbar.style.width = '100%';
    } else {
        navbar.style.position = 'unset';
    }
})
