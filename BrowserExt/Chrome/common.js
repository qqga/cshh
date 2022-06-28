
export function encodeQueryData(data) {
    let ret = [];
    for (let d in data)
        ret.push(encodeURIComponent(d) + '=' + encodeURIComponent(data[d]));
    return ret.join('&');
}


//export function getOptions(callback) {
//    chrome.storage.sync.get({ urlOpt: '', userKeyOpt: '', defaultSetNameOpt: 'Default' }, function (opt) {
//        callback(opt);
//    });
//}

export function messageBox(msg){
    chrome.notifications.create({ message: msg, iconUrl: '/images/cheese128.png', type: 'basic', title: 'Polyglot extension' })
}

//-------------getCshhPage----------------
export function getCshhPage(word, callback) {

    get_options(function (opt) {

        if (!opt.urlOpt) {
            messageBox('Set url in extension settings!');
            return;
        }
        if (!opt.userKeyOpt) {
            messageBox('Set userKey in extension settings!');
            return;
        }

        getCshhPageAjax(word, callback, opt.urlOpt, opt.userKeyOpt, opt.defaultSetNameOpt);
    });
}

function getCshhPageAjax(word, callback, url, userKey, defaultSetName) {
    fetch(url + '?' + encodeQueryData({ word, userKey, defaultSetName }))
        .then(resp => resp.text().then(responseText => callback(responseText)))
        .catch(err => messageBox("err " + err));
}
//=============getCshhPage================



///------------run scripts--------
//when inject received by ajax html text, we need to exec contained scrips by replacing them. 
//Form v3 manifest local scripts only avalible, so replace to local scripts...

export function nodeScriptReplace(node, toLocal = true) {
    var scripts = [...node.getElementsByTagName("script")].filter(script => script.src);
    for (let script of scripts) {
        script.parentNode.replaceChild(nodeScriptClone(script, toLocal), script);
    }
}

function nodeScriptClone(node, toLocal = true) {
    var script = document.createElement("script");
    script.text = node.innerHTML;
    for (var i = node.attributes.length - 1; i >= 0; i--) {
        let attributeName = node.attributes[i].name;
        let attributeValue = node.attributes[i].value;
        let attributeValueCorrect = attributeName === "src" && toLocal ? chrome.runtime.getURL(attributeValue.split('/').pop()) : attributeValue;
        script.setAttribute(attributeName, attributeValueCorrect);
    }
    return script;
}

///===========run scripts=============

//-------------options---------------


export function set_options(options, callback) {
    chrome.storage.sync.set(options, callback);
}


export function get_options(callback) {

    // Use default value color = 'red' and likesColor = true.
    chrome.storage.sync.get({
        urlOpt: 'https://1bite.ru/polyglot/UserWord/AddWordExt',
        userKeyOpt: '',
        defaultSetNameOpt: 'Default'
    }, callback);
}