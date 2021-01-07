using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null)
        {
            GameObject.Destroy(instance.gameObject);
        }
        instance = Init();
        if (ShouldNotDestroyOnLoad())
        {
            DontDestroyOnLoad(this);
        }
    }

    protected abstract T Init();

    protected virtual bool ShouldNotDestroyOnLoad()
    {
        return true;
    }
}
