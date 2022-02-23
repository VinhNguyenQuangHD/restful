jQuery(document).ready(function () {
    jQuery(window).scroll(function () {
        if (jQuery(this).scrollTop()) {
            jQuery('.Header__menu').addClass('sticky');
            jQuery('.Header__menu__list').addClass('sticky');
        }
        else {
            jQuery('.Header__menu').removeClass('sticky');
            jQuery('.Header__menu__list').removeClass('sticky');
        }
    })
})