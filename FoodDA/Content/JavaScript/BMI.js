function error(place, errorMess) {
    document.querySelector(place).innerHTML = errorMess;
}
function validationBMI() {
    var CanNang = document.getElementById('CanNang').value.trim();
    var ChieuCao = document.getElementById('ChieuCao').value.trim();
    var regexNumberpositive = /^[1-9]\d*$/g
    var regexNumberpositive1 = /^[1-9]\d*$/g
    var BMI = (CanNang / (ChieuCao * ChieuCao)) * 10000
    var kq = BMI.toFixed(2);
    if (CanNang == "") {
        error('.error__cannang', 'Vui lòng nhập cân nặng của bạn');
        return false;
    }
    else if (!regexNumberpositive.test(CanNang))
    {
        error('.error__cannang', 'Vui lòng chỉ nhập số dương lớn hơn 0');
        return false;
    }
    else {
        error('.error__cannang', '');
    }


    if (ChieuCao == "") {
        error('.error__chieucao', 'Vui lòng nhập chiều cao');
        return false;
    }
    else if (!regexNumberpositive1.test(ChieuCao)) {
        error('.error__chieucao', 'Vui lòng chỉ nhập số dương lớn hơn 0');
        return false;
    }
    else {
        error('.error__chieucao', '');
    }
    if (kq >= 40) {
        alert('BMI CỦA BẠN LÀ : ' + kq +"\nBẠN ĐANG TRONG TÌNH TRẠNG BÉO PHÌ CẤP ĐỘ 3");
    }
    if (kq < 40 && kq > 35) {
        alert('BMI CỦA BẠN LÀ : ' + kq + "\nBẠN ĐANG TRONG TÌNH TRẠNG BÉO PHÌ CẤP ĐỘ 2");
    }
    if (kq <= 35 && kq > 30) {
        alert('BMI CỦA BẠN LÀ : ' + kq + "\nBẠN ĐANG TRONG TÌNH TRẠNG BÉO PHÌ CẤP ĐỘ 1");
    }
    if (kq <= 30 && kq > 25) {
        alert('BMI CỦA BẠN LÀ : ' + kq + "\nBẠN ĐANG TRONG TÌNH TRẠNG THỪA CÂN");
    }
    if (kq <= 25 && kq > 18) {
        alert('BMI CỦA BẠN LÀ : ' + kq + "\nCƠ THỂ CỦA BẠN KHÔNG CÓ VẤN ĐỀ VỀ CÂN NĂNG");
    }
    if (kq <= 18 && kq > 17) {
        alert('BMI CỦA BẠN LÀ : ' + kq + "\nBẠN ĐANG TRONG TÌNH TRẠNG GẦY CẤP ĐỘ 1");
    }
    if (kq <= 17 && kq > 16) {
        alert('BMI CỦA BẠN LÀ : ' + kq + "\nBẠN ĐANG TRONG TÌNH TRẠNG GẦY CẤP ĐỘ 2");
    }
    if (kq <= 16) {
        alert('BMI CỦA BẠN LÀ : ' + kq + "\nBẠN ĐANG TRONG TÌNH TRẠNG GẦY CẤP ĐỘ 3");
    }

}