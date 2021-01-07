using UnityEngine;
using UnityEditor;

public class BaseHandler<T,M> : BaseSingletonMonoBehaviour<T> 
    where M : BaseManager
    where T : BaseMonoBehaviour
{
    protected M manager;

    protected virtual void Awake()
    {
        manager = Manager;
    }

    public M Manager
    {
        get
        {
            if (manager == null)
            {
                manager = CptUtil.AddCpt<M>(gameObject);
            }
            return manager; 
        }    
    }
}