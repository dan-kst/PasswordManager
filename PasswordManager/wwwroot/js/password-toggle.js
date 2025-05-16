// wwwroot/js/password-toggle.js
document.addEventListener('DOMContentLoaded', function () {
    var toggleBtn = document.getElementById('togglePasswordBtn');
    if (toggleBtn) {
        toggleBtn.addEventListener('click', function () {
            var pwdInput = document.getElementById('masterPasswordInput');
            if (pwdInput.type === 'password') {
                pwdInput.type = 'text';
                this.textContent = 'Hide';
            } else {
                pwdInput.type = 'password';
                this.textContent = 'Show';
            }
        });
    }
});
