using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct UserInfo
{
    public static readonly string UUID_NAME = "uuid";
    public static readonly string NAME = "name";

    public static string Uuid
    {
        get { return PlayerPrefs.GetString(UUID_NAME); }
        set { PlayerPrefs.SetString(UUID_NAME, value); }
    }

    public static string Name
    {
        get { return PlayerPrefs.GetString(NAME); }
        set { PlayerPrefs.SetString(NAME, value); }
    }

    public static string Language
    {
        get
        {
#if UNITY_ANDROID
			try
			{
				var locale = new AndroidJavaClass("java.util.Locale");
				var localeInst = locale.CallStatic<AndroidJavaObject>("getDefault");
				var name = localeInst.Call<string>("getLanguage");
				return name;
			}
			catch(System.Exception e)
			{
				return "Error";
			}
#else
            return "Not supported";
#endif
        }
    }

    /// <summary>
    /// api level
    /// </summary>
    /// <returns>api level</returns>
    public static int AndroidOSVer
    {
        get
        {
            var apiLevel = 0;
#if UNITY_EDITOR
#elif UNITY_ANDROID
			var cls = new AndroidJavaClass("android.os.Build$VERSION"); 
			apiLevel = cls.GetStatic<int>("SDK_INT");
#endif
            return apiLevel;
        }
    }

    public static float DeviceDiagonalSizeInInches()
    {
        float screenWidth = Screen.width / Screen.dpi;
        float screenHeight = Screen.height / Screen.dpi;
        float diagonalInches = Mathf.Sqrt(Mathf.Pow(screenWidth, 2) + Mathf.Pow(screenHeight, 2));
        return diagonalInches;
    }
}
