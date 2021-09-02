using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebXR;


namespace TiltBrush
{
    public class ProfileLoader : MonoBehaviour
    {
        private static ProfileLoader _instance;
        
        public WebXRController controllerL;
        public WebXRController controllerR;
        private VrSdk vrsdk;
        public bool profileFound = false;
        public string profile;
        private string[] profiles;

        public static ProfileLoader Instance
        {
            get { return _instance; }
        }

        private void Awake()
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("Profile loader is active");
        }

        public void TryGetProfile()
        {
            profiles = controllerR.GetProfiles();
            if (profiles != null)
            {
                profile = profiles[0];
                Debug.Log("Found profile: " + profile);
                profileFound = true;
            }
            else if (controllerR.isHandActive && !controllerR.isControllerActive)
            {
                profile = "hand";
                Debug.Log("Found profile: hand");
                profileFound = true;
            }
        }

        public ControllerStyle GetControllerStyle()
        {
            switch (profile)
            {
                case "htc-vive-cosmos":
                    return ControllerStyle.Cosmos;
                case "htc-vive":
                    return ControllerStyle.Vive;
                case "microsoft-mixed-reality":
                case "samsung-odyssey":
                case "windows-mixed-reality":
                    return ControllerStyle.Wmr;
                case "oculus-touch":
                case "oculus-touch-v2":
                case "oculus-touch-v3":
                case "generic-trigger-squeeze-thumbstick":
                    return ControllerStyle.OculusTouch;
                case "valve-index":
                    return ControllerStyle.Knuckles;
                case "hand":
                    return ControllerStyle.Hand;
                case null:
                    Debug.Log("Profile is null, assuming Oculus Link");
                    return ControllerStyle.OculusTouch;
                default:
                    Debug.Log("Uh oh. It looks like your current profile (" + profile + ") isn't supported. Defaulting to WMR style.");
                    return ControllerStyle.Wmr;
            }
        }

        public VrHardware GetVrHardware()
        {
            // This probably needs to be tested
            switch (profile)
            {
                case "htc-vive-cosmos":
                    return VrHardware.Vive;
                case "htc-vive":
                    return VrHardware.Vive;
                case "microsoft-mixed-reality":
                case "samsung-odyssey":
                case "windows-mixed-reality":
                    return VrHardware.Wmr;
                case "oculus-touch":
                case "oculus-touch-v2":
                    return VrHardware.Rift;
                case "oculus-touch-v3":
                    return VrHardware.Quest;
                case "valve-index":
                    return VrHardware.Vive;
                default:
                    Debug.Log("Uh oh. It looks like your current profile (" + profile + ") isn't supported. Defaulting to WMR hardware.");
                    return VrHardware.Wmr;
            }
        }
    }

}
