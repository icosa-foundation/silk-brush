using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebXR;


namespace TiltBrush
{
    public class ProfileLoader : MonoBehaviour
    {
        private static ProfileLoader _instance;
        
        public WebXRController controller;
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

        public bool TryGetProfile()
        {
            profiles = controller.GetProfiles();
            if (profiles != null)
            {
                profile = profiles[0];
                Debug.Log("Found profile: " + profile);
                profileFound = true;
                return true;
            }
            else
            {
                return false;
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
                    return ControllerStyle.OculusTouch;
                case "valve-index":
                    return ControllerStyle.Knuckles;
                default:
                    Debug.Log("Uh oh. It looks like your current profile (" + profile + ") isn't supported. Defaulting to WMR.");
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
                    Debug.Log("Uh oh. It looks like your current profile (" + profile + ") isn't supported. Defaulting to WMR.");
                    return VrHardware.Wmr;
            }
        }
    }

}
