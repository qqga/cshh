import { get_options, set_options } from '/common.js'

function save_options() {

    var urlOpt = document.getElementById('urlOpt').value;
    var userKeyOpt = document.getElementById('userKeyOpt').value;
    var defaultSetNameOpt = document.getElementById('defaultSetNameOpt').value;

    set_options({ urlOpt, userKeyOpt, defaultSetNameOpt },
        function () {
        // Update status to let user know options were saved.
        var status = document.getElementById('status');
        status.textContent = 'Options saved.';
        setTimeout(function () {
            status.textContent = '';
        }, 750);
    });
}

function restore_options() {
    
    get_options( function (items) {        
        document.getElementById('urlOpt').value = items.urlOpt;
        document.getElementById('userKeyOpt').value = items.userKeyOpt;
        document.getElementById('defaultSetNameOpt').value = items.defaultSetNameOpt;
    });
}

restore_options();
document.getElementById('save').addEventListener('click', save_options);