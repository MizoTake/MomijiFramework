using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Momiji;

public class NextScene : MonoBehaviour
{

    public SceneInfo.SceneEnum to;

    public void Next()
    {
        TransSceneManager.LoadScene(to);
    }
}
