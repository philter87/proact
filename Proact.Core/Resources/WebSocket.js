let webSocket = new WebSocket('wss://' + window.location.host + '/ws')

webSocket.onopen = () => {
    console.log("WS Open")
}

webSocket.onmessage = (evt) => {
    let wsMessage = JSON.parse(evt.data);
    if(wsMessage.Type == 'HotReload'){
        location.reload();
        console.log("Data received", wsMessage);
    }
}

webSocket.onclose = () => {
    console.log("WS Close")
}