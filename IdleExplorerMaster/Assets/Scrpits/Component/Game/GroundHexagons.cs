using UnityEditor;
using UnityEngine;
using DG.Tweening;
using System;

public class GroundHexagons : BaseMonoBehaviour
{
    public AreaCover areaCover;
    public AreaType areaType;
    public AreaTerrain areaTerrain;

    public GroundHexagonsBean groundHexagonsData;
    


    /// <summary>
    /// 设置地形数据
    /// </summary>
    /// <param name="groundHexagonsData"></param>
    public void SetGroundHexagonsData(GroundHexagonsBean groundHexagonsData)
    {
        this.groundHexagonsData = groundHexagonsData;
        areaTerrain.SetBelong(0, Color.white);
    }

    /// <summary>
    /// 设置地图资源类型
    /// </summary>
    /// <param name="areaType"></param>
    public void SetAreaType( AreaType areaType)
    {
        this.areaType = areaType;
    }

    /// <summary>
    /// 修改探索状态
    /// </summary>
    /// <param name="areaDiscoveryStatus"></param>
    public void ChangeDiscoveryStatus(AreaDiscoveryStatusEnum  areaDiscoveryStatus,Action actionComplete = null)
    {
        groundHexagonsData.areaDiscoveryStatus = areaDiscoveryStatus;
        switch (areaDiscoveryStatus)
        {
            case AreaDiscoveryStatusEnum.Unexplored:
                areaCover.HideArea(actionComplete);
                break;
            case AreaDiscoveryStatusEnum.Explore:
                areaCover.ShowArea(actionComplete);
                break;
        }
    }

    /// <summary>
    /// 展示区域类型
    /// </summary>
    public void ShowAreaType()
    {
        if(areaType)
            areaType.ShowAreaType();
    }

    /// <summary>
    /// 隐藏区域类型
    /// </summary>
    public void HideAreaType()
    {
        if (areaType)
            areaType.HideAreaType();
    }
}