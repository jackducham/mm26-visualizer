using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static bool _destroyed = false;
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_destroyed)
            {
                Debug.LogWarningFormat("[Singleton] Singleton was already destroyed. Returning null");
                return null;
            }
            if (!_instance)
            {
                // Awake() will be called immediately after component is added
                new GameObject(typeof(T).ToString()).AddComponent<T>();
                // Prevent object destruction on scene change
                DontDestroyOnLoad(_instance);
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null && !_destroyed)
        {
            _instance = this as T;
        }
        else if (_instance != this)
        {
            // Prevent from creating another instance
            Destroy(this);
        }
    }

    protected virtual void OnDestroy()
    {
        if (_instance != this)
        {
            return;
        }
        _destroyed = true;
        _instance = null;
    }


}
