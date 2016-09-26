var recipientInput = document.getElementsByName('To');
var recipientLabel = document.querySelectorAll('[for="To"]');
var dropdownMenu = document.getElementById('dropdownMenu');

recipientInput[0].addEventListener("click", showContactsDropdown);
recipientLabel[0].addEventListener("click", showContactsDropdown);

function showContactsDropdown() {
    dropdownMenu.removeAttribute('class');
}