var recipientInput = document.getElementsByName('To')[0];
var recipientLabel = document.querySelectorAll('[for="To"]')[0];
var dropdownMenu = document.getElementById('dropdownMenu');
var html = "<ul>";
var contacts = JSON.parse(localStorage.getItem("contacts"));
var keys = Object.keys(contacts);
for (var i = 0; i < keys.length; i++) {
    html += "<li class='contact-list-item' id='" + keys[i] + "'>" + contacts[keys[i]] + "</li>";
}
html += "</ul>";
dropdownMenu.innerHTML = html;

recipientInput.addEventListener("click", showContactsDropdown);
recipientLabel.addEventListener("click", showContactsDropdown);

function showContactsDropdown() {
    dropdownMenu.className = "";
}

dropdownMenu.addEventListener("click", function (e) {
    if (e.target && e.target.matches("li.contact-list-item")) {
        recipientInput.value = "+" + e.target.id;
        dropdownMenu.className = "hidden";
    }
});


///fill city input box with Chuck Norris
var chuckSpan = document.getElementById('chuck');
console.log(chuckSpan);
chuckSpan.addEventListener("click", function (e) {
    document.getElementsByName("Body")[0].value = "CHUCK NORRIS";
});