using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebXR;

namespace TiltBrush
{
    public class WebXRControllerInfo : ControllerInfo
    {
        private bool m_IsValid = true;
        public WebXRController controller;

        public WebXRControllerInfo(BaseControllerBehavior behavior, bool isLeftHand) : base(behavior)
        {
            if (isLeftHand)
                controller = GameObject.Find("handL").GetComponent<WebXRController>();
            else
                controller = GameObject.Find("handR").GetComponent<WebXRController>();
        }

        public WebXRController.ButtonTypes MapVrInput(VrInput input)
        {
            switch (input)
            {
                case VrInput.Directional:
                case VrInput.Thumbstick:
                case VrInput.Touchpad:
                    return WebXRController.ButtonTypes.Thumbstick | WebXRController.ButtonTypes.Touchpad;

                case VrInput.Trigger:
                    return WebXRController.ButtonTypes.Trigger;

                case VrInput.Grip:
                    return WebXRController.ButtonTypes.Grip;

                case VrInput.Button01:
                case VrInput.Button04:
                case VrInput.Button06:
                    // Pad_Left, Pad_Down, Full pad, (X,A)
                    return WebXRController.ButtonTypes.ButtonA;

                case VrInput.Button02:
                case VrInput.Button03:
                case VrInput.Button05:
                    // Pad_Right, Pad_Up, Application button, (Y,B)
                    return WebXRController.ButtonTypes.ButtonB;

                case VrInput.Any:
                    return WebXRController.ButtonTypes.Trigger
                         | WebXRController.ButtonTypes.Grip
                         | WebXRController.ButtonTypes.Thumbstick
                         | WebXRController.ButtonTypes.Touchpad
                         | WebXRController.ButtonTypes.ButtonA
                         | WebXRController.ButtonTypes.ButtonB
                         ;
                default:
                    Debug.Log("This shouldn't have happened! Bad input enum " + input.ToString());
                    throw new System.NotImplementedException(); // Ask De-Panther about adding a WebXRController.ButtonTypes.None
            }
        }

        public override bool IsTrackedObjectValid { get { return m_IsValid; } set { m_IsValid = value; } }

        public override float GetGripValue()
        {
            return controller.GetAxis(WebXRController.AxisTypes.Grip);
        }

        public override Vector2 GetPadValue()
        {
            return controller.GetAxis2D(WebXRController.Axis2DTypes.Touchpad);
        }

        public override Vector2 GetPadValueDelta()
        {
            return Vector2.zero;
        }

        public override float GetScrollXDelta()
        {
            return 0;
        }

        public override float GetScrollYDelta()
        {
            return 0;
        }

        public override Vector2 GetThumbStickValue()
        {
            return controller.GetAxis2D(WebXRController.Axis2DTypes.Thumbstick);
        }

        public override float GetTriggerRatio()
        {
            // If this is actually meaningfully different I'll fix it later
            return controller.GetAxis(WebXRController.AxisTypes.Trigger);
        }

        public override float GetTriggerValue()
        {
            return controller.GetAxis(WebXRController.AxisTypes.Trigger);
        }

        public override bool GetVrInput(VrInput input)
        {
            if (!m_IsValid) { return false; }

            return controller.GetButton(MapVrInput(input));
        }

        public override bool GetVrInputDown(VrInput input)
        {
            if (!m_IsValid) { return false; }

            return controller.GetButtonDown(MapVrInput(input));
        }

        public override bool GetVrInputTouch(VrInput input)
        {
            // Are capacitive sensors in the spec? I don't think so, for now at least.
            return false;
        }

        public override void TriggerControllerHaptics(float seconds)
        {
            // Also not in the spec.
            return;
        }
    }

}
