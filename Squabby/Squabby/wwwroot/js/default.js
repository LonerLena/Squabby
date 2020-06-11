function postData(url) {
    var xhr = new XMLHttpRequest();
    xhr.open("POST", url, true);
    xhr.send();
}

function getData(url, callback) {
    var xhr = new XMLHttpRequest();
    xhr.open("GET", url, true);
    xhr.onload = function() {
      var status = xhr.status;
      if (status === 200) {
        callback(null, xhr.response);
      } else {
        callback(status, xhr.response);
      }
    };
    xhr.send();
}