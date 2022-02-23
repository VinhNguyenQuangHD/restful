function error(place, errorMess) {
    document.querySelector(place).innerHTML = errorMess;
}
function validatorDK() {
    var TenNguoiDung = document.getElementById('DK_TenNguoiDung').value.trim();
    var SoDienThoai = document.getElementById('DK_SoDienThoai').value.trim();
    var DiaChi = document.getElementById('DK_DiaChi').value.trim();
    var UserName = document.getElementById('DK_UserName').value.trim();
    var password = document.getElementById('DK_PassWord').value.trim();
    var regexnumber = /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/im
    var regexEmail = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
    if (TenNguoiDung == "") {
        error('.error__message__TenND', 'Vui lòng nhập email');
        return false;
    }
    else if (!regexEmail.test(TenNguoiDung)) {
        error('.error__message__TenND', 'Vui lòng nhập đúng email');
        return false;
    }
    else {
        error('.error__message__TenND', '');
    }

    if (SoDienThoai == "" || SoDienThoai.length < 10 || SoDienThoai.length > 10) {
        error('.error__message__SDT', 'Số điện thoại có 10 chữ số');
        return false;
    }
    else if (!regexnumber.test(SoDienThoai)){
        error('.error__message__SDT', 'Số điện thoại chỉ được chứa số');
        return false;
    }
    else {
        error('.error__message__SDT', '');
    }
    if (DiaChi == "") {
        error('.error__message__DiaChi', 'Vui lòng nhập địa chỉ');
        return false;
    }
    else {
        error('.error__message__DiaChi', '');
    }
    if (UserName == "") {
        error('.error__message__UserNameDangKY', 'Vui lòng nhập địa chỉ');
        return false;
    }
    else {
        error('.error__message__UserNameDangKY', '');
    }
    if (password == "" || password.length<6) {
        error('.error__message__PasswordDK', 'PassWord phải có hơn 6 chữ số');
        return false;
    }
    else {
        error('.error__message__PasswordDK', '');
    }

}