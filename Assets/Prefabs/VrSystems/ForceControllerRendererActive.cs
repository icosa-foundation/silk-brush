using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceControllerRendererActive : MonoBehaviour
{
    void Start()
    {
        foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
        {
            // All of these are off for some reason. Why? Haven't figured it out yet.
            renderer.enabled = true;
        }
    }

}
