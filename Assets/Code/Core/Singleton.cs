using System;
using UnityEngine;

/// <summary>
/// Generic singleton.
/// </summary>
/// <typeparam name="T">Type of your class.</typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected void Awake()
    {
        Instance = CreateSingleton();
    }

    private static T CreateSingleton()
    {
        var ownerObject = new GameObject($"{typeof(T).Name} (singleton)");
        var instance = ownerObject.AddComponent<T>();
        DontDestroyOnLoad(ownerObject);
        return instance;
    }
}
