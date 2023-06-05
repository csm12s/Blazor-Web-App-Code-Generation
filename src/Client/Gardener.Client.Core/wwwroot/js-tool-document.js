//设置title
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

//聊天窗口滚动条滚到最下面
export function scrollBarToBottom(boxId) {
    var div = document.getElementById(boxId);
    if (div) {
        div.scrollTop = div.scrollHeight;
    }
}
