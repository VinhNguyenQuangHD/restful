function CreateOver(rating) {
    for (var i = 1; i <= rating; i++) {
        jQuery("#span" + i).attr('class', 'fas fa-star');
    }
}
function CreateOut(rating) {
    for (var i = 1; i <= rating; i++) {
        jQuery("#span" + i).attr('class', 'far fa-star');
    }
}
function CreateClick(rating) {
    jQuery("#lblRating").val(rating);
    for (var i = 1; i <= rating; i++) {
        jQuery("#span" + i).attr('class', 'fas fa-star');
    }
    for (var i = rating + 1; i <= 5; i++) {
        jQuery("#span" + i).attr('class', 'far fa-star');
    }
}
function CreateSelected() {
    var rating = jQuery("#lblRating").val();
    for (var i = 1; i <= rating; i++) {
        jQuery("#span" + i).attr('class', 'fas fa-star');
    }
}
function VerifyRating() {
    var rating = jQuery("#lblRating").val();
    if (rating == "0") {
        alert("vui lòng chọn mức dộ bình luận");
        return false;
    }
}