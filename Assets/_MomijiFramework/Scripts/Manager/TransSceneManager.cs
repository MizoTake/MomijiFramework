using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine.SceneManagement;

public class TransSceneManager : Singleton<TransSceneManager>
{
    private bool _isSceneLoading = false;
    private bool _isSceneUnloading = false;
    private string _loadingSceneName;
    private string _unloadingSceneName;

    protected override void OnAwake()
    {
        base.OnAwake();
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// シーンを読み込みます
    /// </summary>
    /// <returns>The scene.</returns>
    /// <param name="sceneName">Scene name.</param>
    /// <param name="onComplete">On complete.</param>
    public static void LoadScene(SceneInfo.SceneEnum sceneEnum, Action onComplete = null)
    {
        Instance.StartCoroutine(LoadSceneAsync(sceneEnum, onComplete));
    }

    /// <summary>
    /// 追加でSceneを読み込みます
    /// </summary>
    /// <param name="sceneName">Scene name</param>
    /// <param name="onComplete">On complete</param>
    public static void LoadAddScene(SceneInfo.SceneEnum sceneEnum, Action onComplete = null)
    {
        Instance.StartCoroutine(LoadSceneAsync(sceneEnum, onComplete, LoadSceneMode.Additive));
    }

    /// <summary>
    /// ロードしているSceneをアンロードします
    /// </summary>
    /// <param name="sceneName">Scene name</param>
    public static void UnloadScene(SceneInfo.SceneEnum sceneEnum, Action onComplete = null)
    {
        Instance.StartCoroutine(UnloadSceneAsync(sceneEnum, onComplete));
    }

    /// <summary>
    /// 指定したSceneに強制リロードします
    /// </summary>
    /// <param name="sceneEnum"></param>
    public static void ReloadScene(SceneInfo.SceneEnum sceneEnum)
    {
        SceneManager.LoadScene(sceneEnum.ToString());
    }

    /// <summary>
    /// シーンを非同期的に読み込みます
    /// </summary>
    /// <returns>The scene.</returns>
    /// <param name="sceneName">Scene name.</param>
    /// <param name="onComplete">On complete.</param>
    private static IEnumerator LoadSceneAsync(SceneInfo.SceneEnum sceneEnum, Action onComplete = null, LoadSceneMode mode = LoadSceneMode.Single)
    {
        var sceneName = sceneEnum.ToString();
        if (Instance._isSceneLoading || sceneName == Instance._loadingSceneName)
        {
            yield break;
        }

        // invalid
        if (!SceneInfo.SceneNames.Contains(sceneName))
        {
            Debug.LogError("not registed Scene");
            yield break;
        }

        // scene loaded
        UnityEngine.SceneManagement.Scene scene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName);
        if (scene.isLoaded)
        {
            Debug.LogError("already loadScene");
            yield break;
        }

        Instance._isSceneLoading = true;
        // loading
        Instance._loadingSceneName = sceneName;
        AsyncOperation load = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, mode);
        while (!load.isDone)
        {
            yield return 0;
        }
        Instance._isSceneLoading = false;

        // callback
        if (onComplete != null)
        {
            onComplete();
        }

        yield return 0;
    }

    /// <summary>
    /// シーンを非同期でアンロードします
    /// </summary>
    /// <param name="sceneName">Scene name</param>
    /// <param name="onComplete">On Complete</param>
    /// <returns></returns>
    private static IEnumerator UnloadSceneAsync(SceneInfo.SceneEnum sceneEnum, Action onComplete = null)
    {
        var sceneName = sceneEnum.ToString();

        if (Instance._isSceneUnloading || sceneName == Instance._unloadingSceneName)
        {
            yield break;
        }

        if (!SceneInfo.SceneNames.Contains(sceneName))
        {
            Debug.LogError("not registed Scene");
            yield break;
        }

        Instance._isSceneUnloading = true;
        AsyncOperation unload = SceneManager.UnloadSceneAsync(sceneName);
        while (!unload.isDone)
        {
            yield return 0;
        }
        Instance._isSceneUnloading = false;

        // callback
        if (onComplete != null)
        {
            onComplete();
        }

        yield return 0;
    }
}