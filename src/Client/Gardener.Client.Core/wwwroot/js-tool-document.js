﻿//设置title
export function setDocumentTitle(title) {
    document.title = title;
};
//下载文件
export function downloadFile(url) {
    var iframe;
    try {
        iframe = document.createElement('<iframe name="temp_iframe">');
    } catch (ex) {
        iframe = document.createElement('iframe');
    }
    iframe.id = 'temp_iframe';
    iframe.name = 'temp_iframe';
    iframe.width = 0;
    iframe.height = 0;
    iframe.marginHeight = 0;
    iframe.marginWidth = 0;
    iframe.style.display = "none";
    iframe.src = url
    var elem = document.getElementsByTagName("body")[0];
    elem.appendChild(iframe);
};
//窗口滚动条滚动
export function scrollBar(boxId,height) {
    var div = document.getElementById(boxId);
    if (div) {
        div.scrollTop = height;
    } else {
        console.warn(boxId + ' not find!');
    }
}
//窗口滚动条滚到最下面
export function scrollBarToBottom(boxId) {
    var div = document.getElementById(boxId);
    if (div) {
        div.scrollTop = div.scrollHeight;
    } else {
        console.warn(boxId + ' not find!');
    }
}
//窗口滚动条滚到最上面
export function scrollBarToTop(boxId) {
    scrollBar(boxId, 0);
}
//赋值文本到粘贴板
export function copyTextToClipboard(text) {
    const textArea = document.createElement('textArea')
    textArea.value = text
    textArea.style.width = 0
    textArea.style.position = 'fixed'
    textArea.style.left = '-999px'
    textArea.style.top = '10px'
    textArea.setAttribute('readonly', 'readonly')
    document.body.appendChild(textArea)

    textArea.select()
    document.execCommand('copy')
    document.body.removeChild(textArea)
}