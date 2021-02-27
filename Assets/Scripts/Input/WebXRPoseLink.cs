using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebXR;

public class WebXRPoseLink : MonoBehaviour
{    
    public bool isLeft;
    private WebXRController controller;

    void Start()
    {
        if (isLeft)
            controller = GameObject.Find("handL").GetComponent<WebXRController>();
        else
            controller = GameObject.Find("handR").GetComponent<WebXRController>();

        WebXRManager.OnHandUpdate += UpdatePoseHand;
    }

    void Update()
    {
        if (!controller.isHandActive)
            UpdatePose();
    }

    private void UpdatePose()
    {
        transform.position = controller.transform.position;
        transform.rotation = controller.transform.rotation;
    }

    private void UpdatePoseHand(WebXRHandData handData)
    {
        if (controller.isHandActive && !controller.isControllerActive && handData.hand == (int)controller.hand)
        {
            transform.localPosition = handData.joints[9].position;
            transform.localRotation = handData.joints[8].rotation;
        }
    }

}
