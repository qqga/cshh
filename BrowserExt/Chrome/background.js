const id_addToCshh = "addToCshh";

import { getCshhPage } from '/common.js'

chrome.contextMenus.onClicked.addListener(onClickHandler);

chrome.runtime.onInstalled.addListener(function () {

    chrome.contextMenus.create({
        "title": "add to cshh", "contexts": ["selection"],
        "id": id_addToCshh
    });

});

function onClickHandler(info, tab) {
    if (info.menuItemId == id_addToCshh) {
        addWordToCshh(info.selectionText);
    }
};

function addWordToCshh(word) {

    word = word ? word.trim() : "";

    chrome.tabs.query({ active: true, currentWindow: true }, function (tabs) {
        getCshhPage(word, function (responseText) {
            chrome.tabs.sendMessage(tabs[0].id, { action: "showHtmlText", htmlText: responseText }, function (response) { });
        });

    });
}


