using System.Collections;
using System.Collections.Generic;
using Momiji;
using UnityEngine;

public class NextScene : MonoBehaviour
{

    public SceneInfo.SceneEnum to;
    public bool add = false;

    public void Next ()
    {
        if (add)
        {
            Momiji.TransSceneManager.LoadAddScene (to);
        }
        else
        {
            Momiji.TransSceneManager.LoadScene (to);
        }
    }
}