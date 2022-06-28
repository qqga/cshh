document.getElementById("waitCshhAjax").hidden = true;

document.getElementById("clearSet").onclick = function (h, e) {

    document.getElementById('setNameInput').value = '';
}

var btnSubmit = document.getElementById("submitAddWordAjax");
btnSubmit.onclick = function (h, e) {

    submitAddWordAjaxClick();
}
var urlPost = btnSubmit.dataset.urlpost;

function submitAddWordAjaxClick() {
    beginAddWordAjax();

    let sendData = {
        WordText: document.getElementById("wordTextInput").value,
        SetName: document.getElementById("setNameInput").value,
        WordLanguage_Id: document.getElementById("wordLanguage_IdInput").value,
        Status_Id: document.getElementById("status_IdInput").value,
        userKey: document.getElementById("userKeyInput").value,//wtf mb post 
        Example: document.getElementById("exampleInput").value,
        Translates: document.getElementById("translatesInput").value,
        TranslateLanguage_Id: document.getElementById("translateLanguage_IdInput").value,
    };

    let xhr = new XMLHttpRequest();

    xhr.open("POST", urlPost + "/?" + encodeQueryData(sendData), false);
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4) {
            if (xhr.status == 200) {
                successAddWordAjax(xhr.responseText);
            }
            else {
                failAddWordAjax(xhr.responseText);
            }
        }
    }

    xhr.onerror = function (e) {
        failAddWordAjax(JSON.stringify(e));
    }

    xhr.onloadend = function (e) {
        completeAddWordAjax();
    }

    try {
        xhr.send();
    }
    catch (ex) {
        alert(JSON.stringify(ex));
        failAddWordAjax("Попробуйте через кнопку на панели.(https?)")
        completeAddWordAjax();
    }

}
function failAddWordAjax(err) {
    log("Error: " + err, true);
}

function beginAddWordAjax() {
    document.getElementById("submitAddWordAjax").setAttribute("disabled", "disabled");
    document.getElementById("waitCshhAjax").hidden = false;
}
function completeAddWordAjax() {
    document.getElementById("submitAddWordAjax").removeAttribute("disabled");
    document.getElementById("waitCshhAjax").hidden = true;
}

function successAddWordAjax(data, st, resp) {
    log(data);
}

function log(message, isErr) {
    let infoEl = document.getElementById('ajaxResInfo');
    infoEl.innerText = message;
    if (isErr) {
        infoEl.className = "err";
    }
    else {
        infoEl.className = "ok";
    }

}


function encodeQueryData(data) {
    let ret = [];
    for (let d in data)
        ret.push(encodeURIComponent(d) + '=' + encodeURIComponent(data[d]));
    return ret.join('&');
}
