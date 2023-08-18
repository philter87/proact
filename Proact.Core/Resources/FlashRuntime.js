let proactCurrentValueMap = {};

async function trigger(triggerOptions) {
    if(triggerOptions?.IsValueMapper) {
        
        triggerOptions.Value = proactCurrentValueMap[triggerOptions.Id] || triggerOptions.InitialValue;
        
    }
    console.log("IsValueMapper", triggerOptions)
    
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
    console.log(jsonResult);
    if(jsonResult?.Value) {
        proactCurrentValueMap[triggerOptions.Id] = jsonResult?.Value
    }

    let element = document.querySelector('[data-trigger-id="' + triggerOptions.Id + '"]')
    if(element){
        element.outerHTML = jsonResult.Html
    } else {
        console.log("The trigger with id '" + triggerName + "' was not attached to any html element")
    }
}