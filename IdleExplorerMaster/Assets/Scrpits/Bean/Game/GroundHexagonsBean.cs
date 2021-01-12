using UnityEditor;
using UnityEngine;
using System;

[Serializable]
public class GroundHexagonsBean : BaseBean
{
    //地块探索状态(仅对友方)
    public int areaDiscoveryStatus = 0;
    public AreaDiscoveryStatusEnum GetAreaDiscoveryStatus()
    {
        return ((AreaDiscoveryStatusEnum)areaDiscoveryStatus);
    }
    public void SetAreaDiscoveryStatus(AreaDiscoveryStatusEnum areaDiscoveryStatus) 
    {
        this.areaDiscoveryStatus = (int)areaDiscoveryStatus;
    }

    //地块类型
    public int areaType = 0;
    public AreaTypeEnum GetAreaType()
    {
        return ((AreaTypeEnum)areaType);
    }
    public void SetAreaType(AreaTypeEnum areaType)
    {
        this.areaType = (int)areaType;
    }

    //地块类型名称
    public string areaTypeModelName = "";

    //地面类型
    public int merrainType = 0;
    public TerrainTypeEnum GetTerrainType()
    {
        return ((TerrainTypeEnum)merrainType);
    }
    public void SetTerrainType(TerrainTypeEnum merrainType)
    {
        this.merrainType = (int)merrainType;
    }

    //地块归属人ID(0为未归属任何人)
    public int belongId = 0;

    //偏移坐标系
    public Vector3 coordinatesForOffset;

    //世界坐标系
    public Vector3 coordinatesForWorld;

}