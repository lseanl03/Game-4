using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    private static bool _applicationIsQuitting;

    public static T Instance
    {
        get
        {
            if (_applicationIsQuitting)
                return null;

            if (_instance == null)
            {
                // Need to create a new GameObject to attach the singleton to.
                var singletonObject = new GameObject();
                _instance = singletonObject.AddComponent<T>();
                singletonObject.name = typeof(T).ToString() + " (Singleton)";

            }

            return _instance;
        }
    }

    public static bool IsInstanceValid()
    {
        return (_instance != null);
    }

    //MUST OVERRIDE AWAKE AT CHILD CLASS
    public virtual void Awake()
    {
        if (_instance != null)
        {
            Debug.LogWarning("Already has intsance of " + typeof(T));
            GameObject.Destroy(this);
            return;
        }

        if (_instance == null)
        {
            Debug.Log("a");
            _instance = (T)(MonoBehaviour)this;
            DontDestroyOnLoad(this);
        }

        if (_instance == null)
        {
            Debug.LogError("Awake xong van NULL " + typeof(T));
        }
        //Debug.LogError("Awake of " + typeof(T));
    }

    protected virtual void OnDestroy()
    {
        //self destroy?
        if (_instance == this)
        {
            _instance = null;
            //Debug.LogError ("OnDestroy " + typeof(T));
        }
    }


    private void OnApplicationQuit()
    {
        _applicationIsQuitting = true;
    }
}
