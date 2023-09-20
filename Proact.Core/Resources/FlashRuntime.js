let proactCurrentValueMap = {};

async function changeDynamicValue(opts) {
    if(opts?.ValueMapperId) {
        opts.Value = proactCurrentValueMap[opts.Id] || opts.InitialValue + '';
    }
    let url = window.location.pathname + "?" + new URLSearchParams({ValueChangeRequest: 'true'});
    let response = await fetch(url, {method: "POST", headers: {"Content-Type": "application/json"}, body: JSON.stringify(opts)});
    
    let jsonResult = await response.json();
    if(jsonResult?.Value) {
        proactCurrentValueMap[opts.Id] = jsonResult?.Value
    }
    
    for (let id of Object.keys(jsonResult.IdToHtml)){
        let elements = document.querySelectorAll('[data-dynamic-html-id="' + id + '"]')
        if(elements.length == 0){
            console.log("The dynamic html with id '" + id + "' changed by '" + opts.Id + "' was not found")
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
    changeDynamicValue(triggerOpts)
}

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