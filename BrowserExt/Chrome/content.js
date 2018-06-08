
var mouseEvent = { pageX: 0, pageY: 0 };

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



///------------inject--------------
function injectHtmlToDocument(htmlText, mouseEvent) {
    console.trace("injectHtmlToDocument");
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
    nodeScriptReplace(container);
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

///------------run scripts--------
//when inject received by ajax html text, we need to exec contained scrips
function nodeScriptReplace(node) {
    if (nodeScriptIs(node) === true) {
        node.parentNode.replaceChild(nodeScriptClone(node), node);
    }
    else {
        var i = 0;
        var children = node.childNodes;
        while (i < children.length) {
            nodeScriptReplace(children[i++]);
        }
    }

    return node;
}
function nodeScriptIs(node) {
    return node.tagName === 'SCRIPT';
}
function nodeScriptClone(node) {
    var script = document.createElement("script");
    script.text = node.innerHTML;
    for (var i = node.attributes.length - 1; i >= 0; i--) {
        script.setAttribute(node.attributes[i].name, node.attributes[i].value);
    }
    return script;
}
///===========run scripts=============

