function showRegister() {
    var menuSize = document.getElementById("menu").scrollHeight;
    var registerSize = document.getElementById("signin").scrollHeight;
    document.getElementById("menu").style.height = menuSize + "px";
    document.getElementById("signin").classList.add("unselectedform");
    document.getElementById("signinselector").classList.add("unselected");
    setTimeout(function() {
        document.getElementById("signin").style.display = "none";
    },500);
    setTimeout(function() {
        document.getElementById("register").style.display = "block"
        var signInSize = document.getElementById("register").scrollHeight;
        document.getElementById("menu").style.height = menuSize - registerSize + signInSize + "px";
        setTimeout(function() {
            document.getElementById("registerselector").classList.remove("unselected");
            document.getElementById("register").classList.remove("unselectedform");
        },50);
    },500);
}
function showSignIn() {
    var menuSize = document.getElementById("menu").scrollHeight;
    var registerSize = document.getElementById("register").scrollHeight;
    document.getElementById("menu").style.height = menuSize + "px";
    document.getElementById("register").classList.add("unselectedform");
    document.getElementById("registerselector").classList.add("unselected");
    setTimeout(function() {
        document.getElementById("register").style.display = "none";
    },500);
    setTimeout(function() {
        document.getElementById("signin").style.display = "block"
        var signInSize = document.getElementById("signin").scrollHeight;
        document.getElementById("menu").style.height = menuSize - registerSize + signInSize + "px";
        setTimeout(function() {
            document.getElementById("signinselector").classList.remove("unselected");
            document.getElementById("signin").classList.remove("unselectedform");
        },50);
    },500);
}