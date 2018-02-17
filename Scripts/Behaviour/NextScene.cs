using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Momiji;

public class NextScene : MonoBehaviour
{

    public SceneInfo.SceneEnum to;
    public bool add = false;

    public void Next()
    {
        if (add)
        {
            TransSceneManager.LoadAddScene(to);
        }
        else
        {
            TransSceneManager.LoadScene(to);
        }
    }
}
