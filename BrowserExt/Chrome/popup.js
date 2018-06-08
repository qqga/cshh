
const encodeQueryData = chrome.extension.getBackgroundPage().encodeQueryData;
const getOptions = chrome.extension.getBackgroundPage().getOptions;

chrome.tabs.executeScript({
    code: "window.getSelection().toString();"
}, function (selection) {

    let selectedText = selection ? selection[0].trim() : "";

    showFrameCshh(selectedText);

});

function showFrameCshh(selectedText) {
    
    getOptions(function (opt) {
        let frame = createFrameCshh(selectedText, opt.userKeyOpt, opt.urlOpt, opt.defaultSetNameOpt);       
        document.body.appendChild(frame);
    });
}

function createFrameCshh(selectedText, userKey, cshhUrl, defaultSetName) {
    var frame = document.createElement('iframe');
    frame.setAttribute('id', 'cshhframe');
    frame.setAttribute('src', cshhUrl + '?' + encodeQueryData({ word: selectedText, userKey, defaultSetName }));
    return frame;
}
