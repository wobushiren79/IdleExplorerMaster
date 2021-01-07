using UnityEngine;
using UnityEditor;

public class BaseHandler<T> : BaseObservable<IBaseObserver> where T : BaseManager
{
    protected T manager;

    protected virtual void Awake()
    {
        manager = CptUtil.AddCpt<T>(gameObject);
        AutoLinkHandler();
        AutoLinkManager();
    }

}