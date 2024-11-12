function validateForm() {
    console.log("validateForm dipanggil");
    var name = document.getElementById("name").value;
    var email = document.getElementById("email").value;
    var phone_number = document.getElementById("phone_number").value;
    var password = document.getElementById("password").value;
    var confirmPassword = document.getElementById("ConfirmPassword").value;
    var address = document.getElementById("address").value;
    var driver_license_number = document.getElementById("driver_license_number").value;
    var isValid = true;

    // Validasi Nama Lengkap
    if (!/^[a-zA-Z\s]+$/.test(name)) {
        document.getElementById("usernameerror").innerText = "Nama harus berisi huruf saja";
        isValid = false;
    } else {
        document.getElementById("usernameerror").innerText = "";
    }

    // Validasi Email
    if (!email.endsWith("@example.com")) {
        document.getElementById("emailerror").innerText = "Email harus diakhiri dengan @example.com";
        isValid = false;
    } else {
        document.getElementById("emailerror").innerText = "";
    }

    // Validasi Nomor Telepon
    if (isNaN(phone_number) || phone_number.length !== 12) {
        document.getElementById("phoneerror").innerText = "Nomor telepon harus berupa angka dan 12 digit";
        isValid = false;
    } else {
        document.getElementById("phoneerror").innerText = "";
    }

    // Validasi Driver License Number
    if (driver_license_number.length < 5 || driver_license_number.length > 10) {
        document.getElementById("driverlicenseerror").innerText = "Driver License harus antara 5-10 karakter";
        isValid = false;
    } else {
        document.getElementById("driverlicenseerror").innerText = "";
    }

    // Validasi Password
    if (password.length < 8) {
        document.getElementById("passerror").innerText = "Password minimal harus 8 karakter";
        isValid = false;
    } else {
        document.getElementById("passerror").innerText = "";
    }

    // Validasi Konfirmasi Password
    if (password !== confirmPassword) {
        document.getElementById("confirmpasserror").innerText = "Password tidak cocok";
        isValid = false;
    } else {
        document.getElementById("confirmpasserror").innerText = "";
    }

    return isValid;
}
