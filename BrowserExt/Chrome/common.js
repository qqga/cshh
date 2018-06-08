
function encodeQueryData(data) {
    let ret = [];
    for (let d in data)
        ret.push(encodeURIComponent(d) + '=' + encodeURIComponent(data[d]));
    return ret.join('&');
}


function getOptions(callback) {

    chrome.storage.sync.get({ urlOpt: '', userKeyOpt: '', defaultSetNameOpt: 'Default' }, function (opt) {

        if (!opt.urlOpt) {
            alert('Set url in extension settings!');
            return;
        }
        if (!opt.userKeyOpt) {
            alert('Set userKey in extension settings!');
            return;
        }

        callback(opt);
    });
}