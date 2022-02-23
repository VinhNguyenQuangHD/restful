function error(place, errorMess) {
    document.querySelector(place).innerHTML = errorMess;
}
function validationCalo() {
    var min = document.getElementById('min').value.trim();
    var max = document.getElementById('max').value.trim();
    var regexNumberpositive = /^[1-9]\d*$/g
    var regexNumberpositive1 = /^[1-9]\d*$/g
    if (min == "") {
        error('.error__min', 'Vui lòng nhập min');
        return false;
    }
    else if (!regexNumberpositive.test(min)) {
        error('.error__min', 'Vui lòng nhập số nguyên dương');
        return false;
    }
    else {
        error('.error__min', '');
    }

    if (max == "") {
        error('.error__max', 'Vui lòng nhập max');
        return false;
    }
    else if (!regexNumberpositive1.test(max)) {
        error('.error__max', 'Vui lòng nhập số nguyên dương');
        return false;
    }
    else {
        error('.error__max', '');
    }

}