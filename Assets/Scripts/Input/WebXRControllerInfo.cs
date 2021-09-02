﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebXR;

namespace TiltBrush
{
    public class WebXRControllerInfo : ControllerInfo
    {
        private bool m_IsValid = true;
        public WebXRController controller;

        public override bool IsTrackedObjectValid { get { return m_IsValid; } set { m_IsValid = value; } }

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

                case VrInput.Any: // Adjust this later
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

        public bool MapVrTouch(VrInput input)
        {
            switch(input)
            {
                case VrInput.Directional:
                case VrInput.Thumbstick:
                    return controller.GetAxis2D(WebXRController.Axis2DTypes.Thumbstick) != Vector2.zero;
                case VrInput.Touchpad:
                    return controller.GetAxis2D(WebXRController.Axis2DTypes.Touchpad) != Vector2.zero;

                case VrInput.Button01:
                case VrInput.Button04:
                case VrInput.Button06:
                    // Pad_Left, Pad_Down, Full pad, (X,A)
                    return controller.GetButton(WebXRController.ButtonTypes.ButtonA);

                case VrInput.Button02:
                case VrInput.Button03:
                case VrInput.Button05:
                    // Pad_Right, Pad_Up, Application button, (Y,B)
                    return controller.GetButton(WebXRController.ButtonTypes.ButtonB);

                case VrInput.Any: // Adjust this later
                    return true;

                default:
                    Debug.Log("This shouldn't have happened! Bad input enum " + input.ToString());
                    throw new System.NotImplementedException(); // Ask De-Panther about adding a WebXRController.ButtonTypes.None
            }
        }

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
            return controller.GetAxis2D(WebXRController.Axis2DTypes.Thumbstick).x;
        }

        public override float GetScrollYDelta()
        {
            return controller.GetAxis2D(WebXRController.Axis2DTypes.Thumbstick).y;
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
            // Touch sensors not in spec, use value of buttons != 0 for now
            return MapVrTouch(input);
        }

        public override void TriggerControllerHaptics(float seconds)
        {
            // Oculus browser has haptics support, look into it
            return;
        }
    }

}
