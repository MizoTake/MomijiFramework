using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasReload : MonoBehaviour
{

    public bool auto = false;

    // Use this for initialization
    void Start()
    {
        if (auto) Execute();
    }

    public void Execute()
    {
        Canvas.ForceUpdateCanvases();
    }

}
