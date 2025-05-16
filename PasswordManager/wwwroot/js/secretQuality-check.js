// wwwroot/js/secretQuality-check.js
const strengthLevels = [
    { value: 0, text: "VeryWeak", color: "#dc3545" },
    { value: 1, text: "Weak", color: "#dc3545" },
    { value: 2, text: "Fair", color: "#fd7e14" },
    { value: 3, text: "Good", color: "#ffc107" },
    { value: 4, text: "Strong", color: "#28a745" },
    { value: 5, text: "VeryStrong", color: "#007bff" }
];

function getPasswordStrength(password) {
    let score = 0;
    let length_scaling = 3;

    if (password.length < 8)
        return 0;

    let has_digit = /[0-9]/i.test(password);      //містить цифру
    let has_letter = /[a-z]/i.test(password);      //містить літеру
    let has_symbol = /\W/i.test(password);         //містить символ
    let digits_repeats = /[0-9]{4,}/i.test(password);  //цифри не повторюються більше N разів
    let letters_repeats = /[a-z]{4,}/i.test(password);  //літери не повторюються більше N разів
    let symbols_repeats = /\W{4,}/i.test(password);     //символи не повторюються більше N разів
    let digit_repeat = /([0-9])\1+/i.test(password); //цифра не повторюється більше N разів
    let letter_repeat = /([a-z])\1+/i.test(password); //літера не повторюється більше N разів
    let symbol_repeat = /(\W)\1+/i.test(password);    //символ не повторюється більше N разів

    //оцінка збільшується пропорційно розміру
    score = (password.length >= 8) ? score + (password.length - (password.length % length_scaling)) / length_scaling : 0;

    score = (has_digit) ? score + 1 : score - 1;
    score = (has_letter) ? score + 1 : score - 1;
    score = (has_symbol) ? score + 1 : score - 3;
    score = (digits_repeats) ? score - 3 : score + 1;
    score = (letters_repeats) ? score - 3 : score + 1;
    score = (symbols_repeats) ? score - 2 : score + 1;
    score = (digit_repeat) ? score - 2 : score + 1;
    score = (letter_repeat) ? score - 2 : score + 1;
    score = (symbol_repeat) ? score - 2 : score + 3;

    score = (score < 0) ? 0 : (score > 5) ? 5 : score;

    return score;
}

function updateSecretQualitySelect(strength) {
    const select = document.getElementById("secretQualitySelect");
    const level = strengthLevels[strength];
    select.selectedIndex = strength;
    select.style.backgroundColor = level.color;

    document.getElementById("hiddenSecretQuality").value = select.options[select.selectedIndex].value;
}

document.getElementById("masterPasswordInput").addEventListener("input", function () {
    const password = this.value;
    const strength = getPasswordStrength(password);
    updateSecretQualitySelect(strength);
});

document.addEventListener("DOMContentLoaded", function () {
    // const password = document.getElementById("passwordValue").value;
    // const strength = getPasswordStrength(password);
    // updateSecretQualitySelect(strength);
    const select = document.getElementById("secretQualitySelect");
    const level = strengthLevels[select.selectedIndex];
    select.style.backgroundColor = level.color;
});