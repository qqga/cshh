
import { messageBox, getCshhPage, nodeScriptReplace } from '/common.js'

chrome.tabs.query({ active: true, currentWindow: true }).then(tabs => {
    let result;
    try {
        chrome.scripting.executeScript({
            target: { tabId: tabs[0].id },
            function: () => getSelection().toString(),
        }).then(selection => showFrameCshh(selection[0].result));

    } catch (e) {
        messageBox(e); // ignoring an unsupported page like chrome://extensions
    }
});

function showFrameCshh(selectedText) {

    getCshhPage(selectedText, createEl);

    //getOptions(function (opt) {
    //    let frame = createFrameCshh(selectedText, opt.userKeyOpt, opt.urlOpt, opt.defaultSetNameOpt);
    //    document.body.appendChild(frame);
    //});
}


function createEl(htmlText) {
    let el = document.createElement('div');
    el.innerHTML = htmlText;    
    document.body.appendChild(el);
    nodeScriptReplace(el);
}

//function createFrameCshh(selectedText, userKey, cshhUrl, defaultSetName) {
//    var frame = document.createElement('iframe');
//    frame.setAttribute('id', 'cshhframe');
//    frame.setAttribute('src', cshhUrl + '?' + encodeQueryData({ word: selectedText, userKey, defaultSetName }));    
//    return frame;
//}
