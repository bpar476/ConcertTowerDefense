using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour
{
    private static T instance;

    public static T Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null)
        {
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            instance = Init();
            if (ShouldNotDestroyOnLoad())
            {
                DontDestroyOnLoad(this);
            }
        }
    }

    protected abstract T Init();

    protected virtual bool ShouldNotDestroyOnLoad()
    {
        return true;
    }
}
