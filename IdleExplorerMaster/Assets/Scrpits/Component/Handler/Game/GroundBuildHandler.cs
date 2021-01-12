using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBuildHandler : BaseHandler<GroundBuildHandler, GroundBuildManager>
{
    public GroundHexagons modelForGroundHexagons;
    public UserGroundDataBean currentGroundData;

    public float itemHexagonsX = 0;
    public float itemHexagonsZ = 0;

    /// <summary>
    /// 初始化地形数据
    /// </summary>
    /// <param name="userGroundData"></param>
    public void InitGround(UserGroundDataBean userGroundData)
    {
        this.currentGroundData = userGroundData;
        BuildGroundHexagons(1, userGroundData.groundX, userGroundData.groundZ);
        InitAreaTypeForBase(null);
        InitAreaTypeForRes();
    }

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
        itemHexagonsX = hexagonsSize * 2 * 0.75f;
        itemHexagonsZ = hexagonsSize * Mathf.Sqrt(3);

        for (int x = 0; x < groundX; x++)
        {
            for (int z = 0; z < groundZ; z++)
            {
                //偶数偏移
                float tempOffsetZ = (x % 2 == 0 ? itemHexagonsZ / 2f : 0);
                GroundHexagonsBean groundHexagonsData = new GroundHexagonsBean
                {
                    //设置偏移坐标
                    coordinatesForOffset = new Vector3(x, 0, z),
                    //设置世界坐标
                    coordinatesForWorld = new Vector3(itemHexagonsX * x + offsetX * x, 0, itemHexagonsZ * z + offsetZ * z + tempOffsetZ),
                    //设置地形为未探索
                    areaDiscoveryStatus = AreaDiscoveryStatusEnum.Unexplored,
                    //设置地形类型为Null
                    areaType = AreaTypeEnum.Null,
                };
                BuildItemGroundHexagons(groundHexagonsData);
            }
        }
        //扫描地形
        StartCoroutine(CoroutineForScanGround());
    }

    /// <summary>
    /// 初始化基地
    /// </summary>
    /// <param name="enemyIds"></param>
    public void InitAreaTypeForBase(List<string> enemyIds)
    {
        List<GroundHexagons> listData = manager.GetListGroundHexagons();
        //首先修建友方建筑
        GroundHexagons playerBase = RandomUtil.GetRandomDataByList(listData);

        List<GroundHexagons> aroundData = manager.GetGroundHexagonsForAround(playerBase);
        for (int i = 0; i < aroundData.Count; i++)
        {
            GroundHexagons itemGroundHexagons = aroundData[i];
            //基地初始化
            SetAreaTypeForGroundHexagons(AreaTypeEnum.BaseInit, itemGroundHexagons);
            manager.AddInfluence(currentGroundData.playerId, itemGroundHexagons);
        }
        //设置区域类型
        SetAreaTypeForGroundHexagons(AreaTypeEnum.Base, playerBase);
        manager.AddInfluence(currentGroundData.playerId, playerBase);
    }

    /// <summary>
    /// 初始化区域资源
    /// </summary>
    public void InitAreaTypeForRes()
    {
        List<GroundHexagons> listData = manager.GetListGroundHexagons();
        for (int i = 0; i < listData.Count; i++)
        {
            GroundHexagons groundHexagons = listData[i];
            if (groundHexagons.groundHexagonsData.areaType == AreaTypeEnum.Null)
            {
                //随机设置区域类型
                AreaTypeEnum areaType = (AreaTypeEnum)Random.Range(1, 4);
                SetAreaTypeForGroundHexagons(areaType, groundHexagons);
            }
        }
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
        //设置数据
        itemGroundHexagons.SetGroundHexagonsData(groundHexagonsData);
        manager.AddGroundHexagons(itemGroundHexagons);
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
    /// 为地形设置区域类型
    /// </summary>
    /// <param name="groundHexagons"></param>
    protected void SetAreaTypeForGroundHexagons(AreaTypeEnum areaType, GroundHexagons groundHexagons)
    {
        groundHexagons.groundHexagonsData.areaType = areaType;
        //随机设置模型名称
        AreaTypeInfoBean areaTypeInfo = manager.GetRandomAreaTypeInfo(groundHexagons.groundHexagonsData.areaType);
        groundHexagons.groundHexagonsData.areaTypeModelName = areaTypeInfo.model_name;
        //获取区域类型模型
        GameObject objModelAreaType = manager.GetAreaTypeModel(groundHexagons.groundHexagonsData.areaType, groundHexagons.groundHexagonsData.areaTypeModelName);
        AreaType areaTypeCpt = BuildItemAreaType(groundHexagons.areaTerrain.gameObject, objModelAreaType);
        groundHexagons.SetAreaType(areaTypeCpt);
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
