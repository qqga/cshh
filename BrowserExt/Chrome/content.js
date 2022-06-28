var mouseEvent = { pageX: 0, pageY: 0 };
let commonJs;
//import { nodeScriptReplace } from '/common.js'
(async () => {
    const src = chrome.runtime.getURL("/common.js");
    commonJs = await import(src);    
})();


document.addEventListener('mouseup', function (e) {
    if (e.button == 2) {
        mouseEvent = e;
    }
});

chrome.runtime.onMessage.addListener(
    function (request, sender, sendResponse) {
        if (request.action == "showHtmlText") {
            //sendResponse({ pageX: mouseEvent.pageX, pageY: mouseEvent.pageY });            
            injectHtmlToDocument(request.htmlText, mouseEvent);
        }
    })

//function injectHtmlToDocument2(mouseEvent) {    
//    let ref = chrome.runtime.getURL("popup.html")
//    fetch(ref).then(resp => resp.text().then(html => injectHtmlToDocument(html, mouseEvent)));
//}


///------------inject--------------
function injectHtmlToDocument(htmlText, mouseEvent) {

    let el = document.createElement('div');
    el.innerHTML = htmlText;
    injectElToDocument(el, mouseEvent);
}

function injectElToDocument(el, mouseEvent) {
    const containerName = "injectedCshhDiv";
    let existEl = document.getElementById(containerName);
    if (existEl)
        document.body.removeChild(existEl);

    let container = document.createElement('div');
    container.id = containerName;
    container.appendChild(el);
    makeClosable(container);

    if (mouseEvent) {
        container.style = "left:" + mouseEvent.pageX + "px;top:" + (mouseEvent.pageY + 10) + "px;";
    }

    document.body.appendChild(container);
    commonJs.nodeScriptReplace(container);
}

function makeClosable(el) {
    let div = document.createElement('div');
    let closeButton = document.createElement('button');
    closeButton.id = "closeBtnCshh";
    div.appendChild(closeButton);
    el.appendChild(div);

    closeButton.innerText = "Закрыть";
    closeButton.onclick = function () {

        document.body.removeChild(el);
    }
}

//===========inject=================

