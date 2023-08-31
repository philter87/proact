let proactCurrentValueMap = {};

async function trigger(triggerOptions) {
    if(triggerOptions?.ValueMapperId) {
        
        triggerOptions.Value = proactCurrentValueMap[triggerOptions.Id] || triggerOptions.InitialValue + '';
        
    }
    console.log("triggerOptions", triggerOptions)
    let encodedString = window.btoa(JSON.stringify(triggerOptions));
    let url = window.location.pathname + "?" + new URLSearchParams({triggerOptions: encodedString})
    let response = await fetch(url, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json"
        }
    });
    
    
    let jsonResult = await response.json();
    if(jsonResult?.Value) {
        proactCurrentValueMap[triggerOptions.Id] = jsonResult?.Value
    }
    
    for (let id of Object.keys(jsonResult.IdToHtml)){
        let element = document.querySelector('[data-dynamic-html-id="' + id + '"]')
        if(element){
            element.outerHTML = jsonResult.IdToHtml[id]
        } else {
            console.log("The dynamic html with id '" + id + "' changed by '" + triggerOptions.Id + "' was not found")
        }
    }
}