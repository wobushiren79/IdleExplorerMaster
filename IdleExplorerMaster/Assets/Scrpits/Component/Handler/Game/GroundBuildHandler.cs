using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBuildHandler : BaseHandler<GroundBuildHandler, GroundBuildManager>
{
    public GroundHexagons modelForGroundHexagons;

    /// <summary>
    /// 创建六角地形
    /// </summary>
    /// <param name="hexagonsSize">地形大小</param>
    /// <param name="groundX">X轴范围</param>
    /// <param name="groundZ">Z轴范围</param>
    /// <param name="offsetX">X轴偏移角度</param>
    /// <param name="offsetZ">Z轴偏移角度</param>
    public void BuildGroundHexagons(float hexagonsSize, int groundX, int groundZ, float offsetX = 0, float offsetZ = 0)
    {
        float intervalX = hexagonsSize * 2 * 0.75f;
        float intervalZ = hexagonsSize * Mathf.Sqrt(3);

        for (int x = 0; x < groundX; x++)
        {
            for (int z = 0; z < groundZ; z++)
            {
                //偶数偏移
                float tempOffsetZ = (x % 2 == 0 ? intervalZ / 2f : 0);
                GroundHexagonsBean groundHexagonsData = new GroundHexagonsBean
                {
                    //设置偏移坐标
                    coordinatesForOffset = new Vector3(x, 0, z),
                    //设置世界坐标
                    coordinatesForWorld = new Vector3(intervalX * x + offsetX * x, 0, intervalZ * z + offsetZ * z + tempOffsetZ),
                    //设置地形为未探索
                    areaDiscoveryStatus = AreaDiscoveryStatusEnum.Unexplored,
                    //随机设置区域类型
                    areaType = (AreaTypeEnum)Random.Range(1, 4)
                };
                //随机设置模型名称
                AreaTypeInfoBean areaTypeInfo=  manager.GetRandomAreaTypeInfo(groundHexagonsData.areaType);
                groundHexagonsData.areaTypeModelName = areaTypeInfo.model_name;

                BuildItemGroundHexagons(groundHexagonsData);
                manager.AddGroundHexagonsData(groundHexagonsData);
            }
        }
        //扫描地形
        StartCoroutine(CoroutineForScanGround());
    }

    /// <summary>
    /// 创建单个六角地形
    /// </summary>
    /// <param name="groundHexagonsData"></param>
    public void BuildItemGroundHexagons(GroundHexagonsBean groundHexagonsData)
    {
        //创建六角实例
        GameObject objGroundHexagons = Instantiate(gameObject, modelForGroundHexagons.gameObject, groundHexagonsData.coordinatesForWorld);
        GroundHexagons itemGroundHexagons = objGroundHexagons.GetComponent<GroundHexagons>();
        //获取区域类型模型
        GameObject objModelAreaType = manager.GetAreaTypeModel(groundHexagonsData.areaType, groundHexagonsData.areaTypeModelName);
        AreaType areaType = BuildItemAreaType(itemGroundHexagons.areaTerrain.gameObject, objModelAreaType);
        //设置数据
        itemGroundHexagons.SetGroundHexagonsData(groundHexagonsData, areaType);
    }

    /// <summary>
    /// 创建区域类型
    /// </summary>
    /// <param name="objContent"></param>
    /// <param name="objModel"></param>
    /// <returns></returns>
    public AreaType BuildItemAreaType(GameObject objContent, GameObject objModel)
    {
        GameObject objAreaType = Instantiate(objContent, objModel);
        AreaType areaType = objAreaType.GetComponent<AreaType>();
        objAreaType.SetActive(false);
        return areaType;
    }

    /// <summary>
    /// 协程-扫描地形
    /// </summary>
    /// <returns></returns>
    IEnumerator CoroutineForScanGround()
    {
        foreach (Progress progress in AstarPath.active.ScanAsync())
        {
            Debug.Log("Scanning... " + progress.description + " - " + (progress.progress * 100).ToString("0") + "%");
            yield return null;
        }
    }
}
