// JavaScript source code


function save_options() {

    var urlOpt = document.getElementById('urlOpt').value;
    var userKeyOpt = document.getElementById('userKeyOpt').value;
    var defaultSetNameOpt = document.getElementById('defaultSetNameOpt').value;

    chrome.storage.sync.set({
        urlOpt,
        userKeyOpt,
        defaultSetNameOpt
    }, function () {
        // Update status to let user know options were saved.
        var status = document.getElementById('status');
        status.textContent = 'Options saved.';
        setTimeout(function () {
            status.textContent = '';
        }, 750);
    });
}

// Restores select box and checkbox state using the preferences
// stored in chrome.storage.
function restore_options() {
    alert('a1');
    // Use default value color = 'red' and likesColor = true.
    chrome.storage.sync.get({
        urlOpt: 'http://localhost:4381/polyglot/UserWord/AddWordExt',
        userKeyOpt: '',
        defaultSetNameOpt:'Default'
    }, function (items) {
        alert('a');
        document.getElementById('urlOpt').value = items.urlOpt;
        document.getElementById('userKeyOpt').value = items.userKeyOpt;
        document.getElementById('defaultSetNameOpt').value = items.defaultSetNameOpt;
    });
}
alert('z');
restore_options();
//document.addEventListener('DOMContentLoaded', restore_options);
document.getElementById('save').addEventListener('click',
    save_options);