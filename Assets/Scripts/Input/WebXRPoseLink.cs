using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebXR;

public class WebXRPoseLink : MonoBehaviour
{
    public enum Hand
    {
        Left,
        Right
    }

    public Hand hand;
    private Transform controller;
    
    // Start is called before the first frame update
    void Start()
    {
        if (hand == Hand.Left)
            controller = GameObject.Find("handL").transform;
        else
            controller = GameObject.Find("handR").transform;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePose();
    }

    private void UpdatePose()
    {
        Vector3 adjustedPos = controller.localPosition;
        adjustedPos.z += .5f;
        transform.localPosition = adjustedPos;

        Vector3 adjustedRot = controller.rotation.eulerAngles;
        adjustedRot.x += 30;
        transform.rotation = Quaternion.Euler(adjustedRot);
    }
}
