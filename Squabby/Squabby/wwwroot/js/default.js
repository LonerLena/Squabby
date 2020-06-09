var oldPos = window.pageYOffset;
window.onscroll = function() {
  var newPos = window.pageYOffset;
  if (oldPos > newPos) {
    document.getElementById("navbar").style.top = "0";
  } else {
    document.getElementById("navbar").style.top = "-3rem";
  }
  oldPos = newPos;
} 