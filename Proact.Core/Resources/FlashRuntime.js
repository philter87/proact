let proactCurrentValueMap = {};

async function trigger(triggerName, triggerOptions) {
    if(triggerOptions?.IsValueMapper) {
        
        triggerOptions.Value = proactCurrentValueMap[triggerName] || triggerOptions.InitialValue;
        console.log("IsValueMapper", triggerOptions)
    }
    
    let json = JSON.stringify({
        triggerOptions: triggerOptions,
        triggerId: triggerName
    });
    
    console.log(json)
    let encodedString = window.btoa(json);
    let url = window.location.pathname + "?" + new URLSearchParams({triggerBody: encodedString})
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
        proactCurrentValueMap[triggerName] = jsonResult?.Value
    }

    let element = document.querySelector('[data-trigger-id="' + triggerName + '"]')
    if(element){
        element.outerHTML = jsonResult.Html
    } else {
        console.log("The trigger with id '" + triggerName + "' was not attached to any html element")
    }
}