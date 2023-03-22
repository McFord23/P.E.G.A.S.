using UnityEngine;

/**
 * Добавляет поведение синготона для классов, наследующих MonoBehavior
 */
public class SingletonBehaviour<T>: MonoBehaviour where T : SingletonBehaviour<T>
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        
        Instance = this as T;
    }
}
