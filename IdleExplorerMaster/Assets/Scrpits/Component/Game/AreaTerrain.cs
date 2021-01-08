using UnityEditor;
using UnityEngine;
using DG.Tweening;
using System;

public class AreaTerrain : BaseMonoBehaviour
{
    public void RolloverTerrain(Action actionHalf, Action actionComplete)
    {
        transform.DOKill();
        transform.localEulerAngles = Vector3.zero;
        transform.localPosition = Vector3.zero;
        transform.DOLocalMoveY(3, 1).SetLoops(2, LoopType.Yoyo);
        transform
            .DOLocalRotate(new Vector3(0, 0, 360), 1, RotateMode.FastBeyond360)
            .OnUpdate(() =>
            {
                if (transform.localEulerAngles.x == 180)
                {
                    actionHalf?.Invoke();
                }
            })
            .OnComplete(() =>
            {
                actionComplete?.Invoke();
            });
    }
}