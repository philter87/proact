let proactCurrentValueMap = {};

async function trigger(options) {
    if(options?.ValueMapperId) {
        options.Value = proactCurrentValueMap[options.Id] || options.InitialValue + '';
    }
    let encodedString = window.btoa(JSON.stringify(options));
    let url = window.location.pathname + "?" + new URLSearchParams({triggerOptions: encodedString})
    let response = await fetch(url, {method: "GET", headers: {"Content-Type": "application/json"}});
    
    let jsonResult = await response.json();
    if(jsonResult?.Value) {
        proactCurrentValueMap[options.Id] = jsonResult?.Value
    }
    
    for (let id of Object.keys(jsonResult.IdToHtml)){
        let elements = document.querySelectorAll('[data-dynamic-html-id="' + id + '"]')
        if(elements.length == 0){
            console.log("The dynamic html with id '" + id + "' changed by '" + options.Id + "' was not found")
        }
        for(let e of elements){
            e.outerHTML = jsonResult.IdToHtml[id]
        }
    }
}

const proactFormSubmit = (triggerOpts, event) => {
    event.preventDefault();
    let value = {}
    for (let e of event.target.elements){
        if(e.name){
            value[e.name] = e.value;
        }
    }
    triggerOpts.Value = JSON.stringify(value);
    trigger(triggerOpts)
}

let webSocket = new WebSocket('wss://localhost:7249/ws')

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