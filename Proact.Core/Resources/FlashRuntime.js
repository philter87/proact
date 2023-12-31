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

    let valueChangeRenders = await response.json();
    
    for(let jsonResult of valueChangeRenders){
        if (jsonResult?.Value) {
            proactCurrentValueMap[id] = jsonResult?.Value
        }
        for (let htmlChange of jsonResult.Changes) {
            let elements = document.querySelectorAll('[data-dynamic-value-id="' + htmlChange.Id + '"]')
            if (elements.length === 0) {
                console.log("The dynamic html with id '" + htmlChange.Id + "' changed by '" + id + "' was not found")
            }
            for (let e of elements) {
                e.outerHTML = htmlChange.Html;
            }
        }
    }
    
    
}
window.addEventListener('popstate', async e => {
    const newPath = document.location.pathname;
    console.log(newPath)
    return changeDynamicValue('proact-route-url', newPath)
});
async function proactNavigate(path, event){
    if(event){
        event.preventDefault();
    }
    window.history.pushState({}, '', path)
    return changeDynamicValue('proact-route-url', path)
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