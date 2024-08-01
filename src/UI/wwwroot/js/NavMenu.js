window.navMenu = {
    enableCollapse: function () {
        document.getElementById('nav-menu-collapse').addEventListener('click', collapseNavMenu);
    }
};
function collapseNavMenu() {
    const navMenu = document.getElementById('nav-menu');
    const collapseIcon = document.getElementById('nav-menu-collapse');

    if (collapseIcon.classList.contains('bi-text-indent-right')) {
        collapseIcon.classList.remove('bi-text-indent-right');
        collapseIcon.classList.add('bi-justify');
    }
    else {
        collapseIcon.classList.remove('bi-justify');
        collapseIcon.classList.add('bi-text-indent-right');
    }
    
    navMenu.classList.toggle('sidebar-collapse');
}