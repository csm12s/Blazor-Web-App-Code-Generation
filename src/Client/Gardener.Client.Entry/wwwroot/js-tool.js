//设置title
window.document.setTitle = function (title) {
    document.title = title;
};
window.document.downloadFile = function (url) {
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