using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour
    where T :MonoBehaviour
{
    static T m_Instance;

    public static T Instance { get => m_Instance; set => m_Instance = value; }

    protected virtual void Awake()
    {
        m_Instance = this as T;
    }
}
