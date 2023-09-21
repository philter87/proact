let proactCurrentValueMap = {};

async function changeDynamicValue(id, value, opts) {
    if (opts?.ValueMapperId) {
        value = proactCurrentValueMap[id];
    }
    let valueChangeRequest = {...opts, Id: id, Value: value}
    let url = window.location.pathname + "?" + new URLSearchParams({ValueChangeRequest: 'true'});
    let response = await fetch(url, {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify(valueChangeRequest)
    });

    let jsonResult = await response.json();
    if (jsonResult?.Value) {
        proactCurrentValueMap[id] = jsonResult?.Value
    }
    for (let htmlChange of jsonResult.HtmlChanges) {
        let elements = document.querySelectorAll('[data-dynamic-html-id="' + htmlChange.Id + '"]')
        if (elements.length === 0) {
            console.log("The dynamic html with id '" + htmlChange.Id + "' changed by '" + id + "' was not found")
        }
        for (let e of elements) {
            e.outerHTML = htmlChange.Html;
        }
    }
}
window.addEventListener('popstate', async e => {
    const newPath = document.location.pathname;
    console.log(newPath)
    return changeDynamicValue('proact-routing', newPath)
});
async function proactNavigate(path, event){
    if(event){
        event.preventDefault();
    }
    window.history.pushState({}, '', path)
    return changeDynamicValue('proact-routing', path)
}

const proactFormSubmit = async (id, event) => {
    event.preventDefault();
    let value = {}
    for (let e of event.target.elements){
        if(e.name){
            value[e.name] = e.value;
        }
    }
    await changeDynamicValue(id, JSON.stringify(value))
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