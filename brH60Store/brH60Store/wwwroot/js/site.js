function menuExpand() {
    var menu = document.getElementsByClassName("menu-container")[0];
    if (menu.classList.contains("active")) {
        menu.classList.remove("active");
    } else {
        menu.classList.add("active");
    }
}