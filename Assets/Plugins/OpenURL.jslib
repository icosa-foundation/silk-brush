mergeInto(LibraryManager.library, {
    _OpenURL: function(url) {
        //If session is active and user agent is Mobile VR, end the session to show them the browser
        if (window.xrManager.xrSession != null && window.navigator.userAgent.includes("VR"))
            window.xrManager.xrSession.end();
            
        if (typeof url == "string")
            window.open(url);
        else if (typeof url == "number")
            window.open(Pointer_stringify(url));
    }
});