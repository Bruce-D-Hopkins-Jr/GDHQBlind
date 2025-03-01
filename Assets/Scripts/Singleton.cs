using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{

    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            
                Debug.LogWarning("This Component + " + typeof(T).ToString() + "is NULL.");
                return _instance;
        }


    }

    void Awake()
    {
        _instance = this as T;
        Init();
    }

    public virtual void Init()
    {
        // optional override
    }
}
