const id_addToCshh = "addToCshh";

chrome.contextMenus.onClicked.addListener(onClickHandler);

chrome.runtime.onInstalled.addListener(function () {

    chrome.contextMenus.create({
        "title": "add to cshh", "contexts": ["selection"],
        "id": id_addToCshh
    });

});

function onClickHandler(info, tab) {


    if (info.menuItemId == id_addToCshh) {

        //console.log("item " + info.menuItemId + " was clicked");
        //console.log("info: " + JSON.stringify(info));
        //console.log("tab: " + JSON.stringify(tab));
        console.trace("onClickHandler");
        addWordToCshh(info.selectionText);
    }
};



function addWordToCshh(word) {

    console.trace("addWordToCshh, word:" + word);

    word = word ? word.trim() : "";

    chrome.tabs.query({ active: true, currentWindow: true }, function (tabs) {
        getCshhPage(word, function (xhr) {
            chrome.tabs.sendMessage(tabs[0].id, { action: "showHtmlText", htmlText: xhr.responseText }, function (response) { });
        });

    });
}

function getCshhPage(word, callback) {
    console.trace("getCshhPage, word: " + word);
    getOptions(function (opt) {
        console.trace("getCshhPage.storage.sync.get, opt: " + JSON.stringify(opt));
        getCshhPageAjax(word, callback, opt.urlOpt, opt.userKeyOpt, opt.defaultSetNameOpt);
    });
}

function getCshhPageAjax(word, callback, url, userKey, defaultSetName) {
    console.trace("getCshhPageAjax, args:" + JSON.stringify(arguments));
    var xhr = new XMLHttpRequest();
    xhr.open("GET", url + '?' + encodeQueryData({ word, userKey, defaultSetName }), true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4) {
            console.trace("getCshhPageAjax.readystate==4, xhr.status: " + xhr.status);
            callback(xhr);
        }
    }
    xhr.onerror = function () {
        alert("err " + JSON.stringify(arguments));
    }
    xhr.send();
}


