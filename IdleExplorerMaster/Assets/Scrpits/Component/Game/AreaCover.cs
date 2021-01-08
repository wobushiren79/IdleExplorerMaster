using UnityEditor;
using UnityEngine;
using DG.Tweening;
using System;

public class AreaCover : BaseMonoBehaviour
{
    public void ShowArea(Action actionComplete = null)
    {
        transform.DOKill();
        transform.localPosition = Vector3.zero;
        transform.DOLocalMoveX(5, 2).OnComplete(()=>{ gameObject.SetActive(false); actionComplete?.Invoke(); });
    }

    public void HideArea(Action actionComplete = null)
    {
        transform.DOKill();
        gameObject.SetActive(true);
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 1).OnComplete(() => { actionComplete?.Invoke(); });
    }
}