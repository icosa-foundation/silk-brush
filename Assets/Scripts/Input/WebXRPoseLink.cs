using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebXR;

public class WebXRPoseLink : MonoBehaviour
{
    
    public bool isLeft;
    private Transform controller;
    
    // Start is called before the first frame update
    void Start()
    {
        if (isLeft)
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
        transform.position = controller.position;
        transform.rotation = controller.rotation;
    }
}
