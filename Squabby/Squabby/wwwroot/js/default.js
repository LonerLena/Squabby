function postData(url) {
    var xhr = new XMLHttpRequest();
    xhr.open("POST", url, true);
    xhr.send();
}