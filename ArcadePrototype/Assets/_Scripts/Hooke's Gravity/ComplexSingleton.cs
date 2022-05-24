using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexSingleton<T> : MonoBehaviour where T : ComplexSingleton<T>
{
    private static T instance;

    public static T Instance { get => instance; }

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            instance = (T)this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
