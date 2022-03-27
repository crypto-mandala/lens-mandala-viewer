using System;
using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(T);

                instance = (T)FindObjectOfType(t);
                if (instance == null)
                {
                    Debug.LogError($"There is no object of {t}");
                }
            }

            return instance;
        }
    }

    protected void Awake ()
    {
        if (this != Instance)
        {
            Destroy(this);
            Debug.LogError($"There is GameObject of {typeof(T)} in {Instance.gameObject.name}. Then destroy this.");
            
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

}