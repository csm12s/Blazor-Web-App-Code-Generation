//聊天窗口滚动条滚到最下面
export function chatMessageBoxToBottom() {
    var div = document.getElementById('message-box');
    div.scrollTop = div.scrollHeight;
}
