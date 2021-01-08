using UnityEditor;
using UnityEngine;
using DG.Tweening;

public class AreaType : BaseMonoBehaviour
{

    public virtual void ShowAreaType()
    {
        gameObject.SetActive(true);
        transform.DOKill();
        transform.localScale = Vector3.one;
        transform
            .DOScale(Vector3.zero, 1)
            .From()
            .OnComplete(() =>
            {
      
            });
    }

    public virtual void HideAreaType()
    {
        transform.DOKill();
        transform.localScale = Vector3.one;
        transform
            .DOScale(Vector3.zero, 1)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
    }
}