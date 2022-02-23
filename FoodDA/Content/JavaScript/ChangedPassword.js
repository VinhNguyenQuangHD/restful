function error(place, errorMess) {
    document.querySelector(place).innerHTML = errorMess;
}
function validatorChangedPassWord() {
    var username = document.getElementById('txtEmail').value.trim();
    var passwordold = document.getElementById('txtOldPassWord').value.trim();
    var passwordNew = document.getElementById('txtNewPassWord').value.trim();
    if (username == "") {
        error('.error__message__usernameChanged','Vui lòng nhập User name');
        return false;
    }
    else {
        error('.error__message__usernameChanged', '');
    }

    if (passwordold == "") {
        error('.error__message__passwordChanged', 'Vui lòng nhập Password');
        return false;
    }
    else {
        error('.error__message__passwordChanged', '');
    }
    if (passwordNew == "")
    {
        error('.error__message__passwordCheckinChanged', 'Vui lòng nhập Password');
        return false;
    }
    else if (passwordNew == passwordold) {
        error('.error__message__passwordCheckinChanged', 'mật khẩu mới không được trùng MK cũ');
        return false;
    }
    else {
        error('.error__message__passwordCheckinChanged','');
    }

}