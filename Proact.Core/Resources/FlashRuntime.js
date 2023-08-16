async function trigger(triggerName, value) {
    let json = JSON.stringify({
        value: value || "",
        isValueEmpty: !value,
        triggerId: triggerName
    });
    console.log(json)
    let encodedString = window.btoa(json);
    let url = window.location.pathname + "?" + new URLSearchParams({triggerBody: encodedString})
    let response = await fetch(url, {
        method: "GET",
        headers: {
            "Content-Type": "text/plain",
            "Accept": "text/html"
        }
    });
    

    let htmlRaw = await response.text();

    let element = document.querySelector('[data-trigger-id="' + triggerName + '"]')
    if(element){
        element.outerHTML = htmlRaw
    } else {
        console.log("The trigger with id '" + triggerName + "' was not attached to any html element")
    }
}