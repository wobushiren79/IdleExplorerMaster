using UnityEditor;
using UnityEngine;
using DG.Tweening;

public class GroundHexagons : BaseMonoBehaviour
{
    public AreaCover areaCover;
    public AreaType areaType;
    public AreaTerrain areaTerrain;

    protected  GroundHexagonsBean groundHexagonsData;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            areaType.ShowAreaType();
            areaTerrain.RolloverTerrain(null,null);
            ChangeDiscoveryStatus(AreaDiscoveryStatusEnum.Explore);
        }
        if (Input.GetMouseButtonDown(0))
        {
            ChangeDiscoveryStatus(AreaDiscoveryStatusEnum.Unexplored);
        }
    }

    /// <summary>
    /// 设置地形数据
    /// </summary>
    /// <param name="groundHexagonsData"></param>
    public void SetGroundHexagonsData(GroundHexagonsBean groundHexagonsData, AreaType areaType)
    {
        this.groundHexagonsData = groundHexagonsData;
        this.areaType = areaType;
    }

    /// <summary>
    /// 修改探索状态
    /// </summary>
    /// <param name="areaDiscoveryStatus"></param>
    public void ChangeDiscoveryStatus(AreaDiscoveryStatusEnum  areaDiscoveryStatus)
    {
        groundHexagonsData.areaDiscoveryStatus = areaDiscoveryStatus;
        switch (areaDiscoveryStatus)
        {
            case AreaDiscoveryStatusEnum.Unexplored:
                areaCover.HideArea();
                break;
            case AreaDiscoveryStatusEnum.Explore:
                areaCover.ShowArea();
                break;
        }
    }

    /// <summary>
    /// 展示区域类型
    /// </summary>
    public void ShowAreaType()
    {

    }

    /// <summary>
    /// 隐藏区域类型
    /// </summary>
    public void HideAreaType()
    {

    }
}