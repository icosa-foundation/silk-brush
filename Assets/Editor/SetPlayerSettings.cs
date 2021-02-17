using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class SetPlayerSettings
{
    static SetPlayerSettings()
    {
        PlayerSettings.WebGL.emscriptenArgs = "-s TOTAL_MEMORY=384MB";
    }
}
