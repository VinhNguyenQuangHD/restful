function error(place, errorMess) {
    document.querySelector(place).innerHTML = errorMess;
}
function validatorSuaGioHang() {
    var username = document.getElementById('txtSoLuong').value.trim();
    var regexnumber = /[0-9]/g;
    
    if (username == "") {
        error('.error__SuaGioHang', 'Vui lòng không để trống');
        return false;
    }
    else if (!regexnumber.test(username))
    {
        error('.error__SuaGioHang', 'Vui lòng chỉ nhập số');
        return false;
    }
    else if (username<=0) {
        error('.error__SuaGioHang', 'Vui nhập số lượng lớn hơn 0');
        return false;
    }
    else {
        error('.error__SuaGioHang','');
    }

}