using UnityEngine;
using System.Collections;

namespace Momiji
{
    /// <summary>
    /// シングルトンインスタンス
    /// </summary>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        void Awake()
        {
            if (_instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }
            _instance = GetComponent<T>();
            OnAwake();
        }

        protected virtual void OnAwake()
        {
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));
                    if (_instance == null)
                    {
                        _instance = CreateInstance();
                        Debug.Log("Instance create. " + typeof(T));
                    }
                }
                return _instance;
            }
        }

        public static T CreateInstance()
        {

            if (_instance != null)
            {
                Debug.LogWarning("Instance is already existed! " + typeof(T));
                return _instance;
            }

            GameObject g = new GameObject("_Singleton " + typeof(T));
            _instance = g.AddComponent<T>();
            return _instance;
        }
    }
}