mergeInto(LibraryManager.library, {
    _OpenURL: function(url) {
        //If session is active and user agent is Mobile VR, end the session to show them the browser
        if (window.xrManager.xrSession != null && window.navigator.userAgent.includes("VR"))
            window.xrManager.xrSession.end();
            
        if (typeof url == "string")
            window.open(url);
        else if (typeof url == "number")
            window.open(Pointer_stringify(url));
    },
    _OpenAuthURL: function(url, _id, _secret) {
        var child = window.open(Pointer_stringify(url));
        var id = Pointer_stringify(_id);
        var secret = Pointer_stringify(_secret);
        var interval = setInterval(function() {
            try {
                // When redirected back to redirect_uri
                if (child.location.hostname === location.hostname) {
                    clearInterval(interval); // Stop Interval
                    
                    // Auth Code Flow
                    const urlParams = new URLSearchParams(child.location.search);
                    const authCode = urlParams.get('code').toString();
                    
                    const data = {
                        code: authCode,
                        client_id: id,
                        client_secret: secret,
                        headers: {
                          'Content-Type': 'application/json'
                        },
                        redirect_uri: "https://localhost:8000/authorize",
                        grant_type: "authorization_code"
                    };
                    
                    fetch("https://oauth2.googleapis.com/token", {
                        method: "POST",
                        mode: "cors",
                        body: JSON.stringify(data)
                    }).then(function(results) {
                        results.json().then(function(data) {
                            console.log(data);
                            unityInstance.SendMessage('GoogleIdentity', 'ReceiveToken', JSON.stringify(data));
                        });
                    });
                    
                    child.close();
                }
            }
            catch(e) {
                // Child window in another domain
                console.log("Still logging in ...");
            }
        }, 50);
    },
    ExportGltf: function(path) {                   
        var filePath = Pointer_stringify(path);
        console.log(filePath);
        
        var openReq = indexedDB.open("/idbfs").onsuccess = function(event) {
            var db = event.target.result;
            var objectStore = db.transaction("FILE_DATA").objectStore("FILE_DATA").openCursor(filePath).onsuccess = function (event) {
                var cursor = event.target.result;
                if (cursor) {
                    console.log(cursor);
                    var filename = filePath.split("/").pop();
                
                    var glbFile = cursor.value.contents.buffer;

                    // Get window.URL object
                    var URL = window.URL || window.webkitURL;

                    // Create an invisible <a> element
                    var a = document.createElement("a");
                    a.style.display = "none";
                    document.body.appendChild(a);

                    // Set the HREF to a Blob representation of the data to be downloaded
                    a.href = URL.createObjectURL(new Blob([glbFile], {type: "model/gltf-binary"}));

                    // Use download attribute to set set desired file name
                    a.setAttribute("download", filename);

                    // Trigger the download by simulating click
                    a.click();

                    // Cleanup
                    window.URL.revokeObjectURL(a.href);
                    document.body.removeChild(a);
                }
                else {
                    console.log("Not found");
                    console.log(event);
                }
            };
        };
    },
    ShowSecrets: function(secrets) {
        unityInstance.Module.showSecrets();
    }
});