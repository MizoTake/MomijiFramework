using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureCallback : MonoBehaviour
{

    public void Click()
    {
        Debug.Log("Click");
    }

    public void LeftFlick()
    {
        Debug.Log("Left");
    }

    public void RightFlick()
    {
        Debug.Log("Right");
    }

    public void DownFlick()
    {
        Debug.Log("Down");
    }

    public void UpFlick()
    {
        Debug.Log("Up");
    }
}
