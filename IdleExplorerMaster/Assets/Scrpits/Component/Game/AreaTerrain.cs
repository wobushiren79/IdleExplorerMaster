using UnityEditor;
using UnityEngine;
using DG.Tweening;
using System;

public class AreaTerrain : BaseMonoBehaviour
{
    public Renderer terrainRenderer;

    /// <summary>
    /// 设置归属
    /// </summary>
    /// <param name="belongId"></param>
    /// <param name="belongColor"></param>
    public void SetBelong(long belongId ,Color belongColor)
    {
        if (belongId == 0)
        {
            terrainRenderer.material.color = Color.white;
        }
        else
        {
            terrainRenderer.material.color = belongColor;
        }
    }

    /// <summary>
    /// 翻转地形
    /// </summary>
    /// <param name="actionHalf"></param>
    /// <param name="actionComplete"></param>
    public void RolloverTerrain(Action actionStart, Action actionComplete,float delayTime = 0)
    {
        transform.DOKill();
        transform.localEulerAngles = Vector3.zero;
        transform.localPosition = Vector3.zero;
        transform.DOLocalMoveY(3, 1).SetLoops(2, LoopType.Yoyo);
        transform
            .DOLocalRotate(new Vector3(0, 0, 360), 1, RotateMode.FastBeyond360)
            .SetDelay(delayTime)
            .OnStart(() =>
            {
                actionStart?.Invoke();
            })
            .OnComplete(() =>
            {
                actionComplete?.Invoke();
            });
    }

}