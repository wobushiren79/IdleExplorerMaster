using UnityEditor;
using UnityEngine;
using System;

[Serializable]
public class GroundHexagonsBean : BaseBean
{
    //地块探索状态(仅对友方)
    private int m_AreaDiscoveryStatus = 0;
    public AreaDiscoveryStatusEnum areaDiscoveryStatus { get => EnumUtil.GetEnum<AreaDiscoveryStatusEnum>(m_AreaDiscoveryStatus); set => m_AreaDiscoveryStatus = (int)value; }

    //地块类型
    private int m_AreaType = 0;
    public AreaTypeEnum areaType { get => EnumUtil.GetEnum<AreaTypeEnum>(m_AreaType); set => m_AreaType = (int)value; }

    //地块类型名称
    private string m_AreaTypeModelName = "";
    public string areaTypeModelName { get => m_AreaTypeModelName; set => m_AreaTypeModelName = value; }

    //地面类型
    private int m_TerrainType = 0;
    public TerrainTypeEnum terrainType { get => EnumUtil.GetEnum<TerrainTypeEnum>(m_TerrainType); set => m_TerrainType = (int)value; }

    //地块归属人ID(0为未归属任何人)
    public int m_BelongId = 0;
    public int belongId { get => m_BelongId; set => m_BelongId = value; }

    //偏移坐标系
    private Vector3 m_CoordinatesForOffset;
    public Vector3 coordinatesForOffset { get => m_CoordinatesForOffset; set => m_CoordinatesForOffset = value; }

    //世界坐标系
    private Vector3 m_CoordinatesForWorld;
    public Vector3 coordinatesForWorld { get => m_CoordinatesForWorld; set => m_CoordinatesForWorld = value; }

}