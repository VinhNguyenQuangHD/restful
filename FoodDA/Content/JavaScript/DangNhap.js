function error(place, errorMess) {
    document.querySelector(place).innerHTML = errorMess;
}
function validator() {
    var username = document.getElementById('UserName').value.trim();
    console.log(username);
    var password = document.getElementById('PassWord').value.trim();
    if (username =="") {
        error('.error__message__username', 'Vui lòng nhập User name');
        return false;
    }
    else {
        error('.error__message__username', '');
    }

    if (password == "") {
        error('.error__message__password', 'Vui lòng nhập Password');
        return false;
    }
    else {
        error('.error__message__username', '');
    }
    
}