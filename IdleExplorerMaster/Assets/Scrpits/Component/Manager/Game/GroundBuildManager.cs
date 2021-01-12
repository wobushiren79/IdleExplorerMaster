using System;
using System.Collections.Generic;
using UnityEngine;

public class GroundBuildManager : BaseManager, IAreaTypeInfoView
{
    //模型合集
    protected Dictionary<string, GameObject> dicModel = new Dictionary<string, GameObject>();
    //所有数据集合
    protected Dictionary<Vector3, GroundHexagons> dicGroundHexagons = new Dictionary<Vector3, GroundHexagons>();
    //势力数据
    protected Dictionary<string, List<GroundHexagons>> dicInfluence = new Dictionary<string, List<GroundHexagons>>();

    protected readonly string Path_AreaType_Base = "AreaType/Base";
    protected readonly string Path_AreaType_Null = "AreaType/Null";
    protected readonly string Path_AreaType_Building = "AreaType/Building";
    protected readonly string Path_AreaType_Gold = "AreaType/Gold";

    protected AreaTypeInfoController controllerForAreaType;

    protected List<AreaTypeInfoBean> listAreaTypeInfoForBase = new List<AreaTypeInfoBean>();
    protected List<AreaTypeInfoBean> listAreaTypeInfoForNull = new List<AreaTypeInfoBean>();
    protected List<AreaTypeInfoBean> listAreaTypeInfoForBuilding = new List<AreaTypeInfoBean>();
    protected List<AreaTypeInfoBean> listAreaTypeInfoForGold = new List<AreaTypeInfoBean>();

    private void Awake()
    {
        controllerForAreaType = new AreaTypeInfoController(this, this);

        Action<List<AreaTypeInfoBean>> actionGetAllData = (listData) =>
        {
            listAreaTypeInfoForBase.Clear();
            listAreaTypeInfoForNull.Clear();
            listAreaTypeInfoForBuilding.Clear();
            listAreaTypeInfoForGold.Clear();
            for (int i = 0; i < listData.Count; i++)
            {
                AreaTypeInfoBean itemData = listData[i];
                switch (itemData.GetAreaType())
                {
                    case AreaTypeEnum.Base:
                        listAreaTypeInfoForBase.Add(itemData);
                        break;
                    case AreaTypeEnum.Null:
                    case AreaTypeEnum.BaseInit:
                        listAreaTypeInfoForNull.Add(itemData);
                        break;
                    case AreaTypeEnum.Building:
                        listAreaTypeInfoForBuilding.Add(itemData);
                        break;
                    case AreaTypeEnum.Gold:
                        listAreaTypeInfoForGold.Add(itemData);
                        break;
                }
            }
        };
        controllerForAreaType.GetAllAreaTypeInfoData(actionGetAllData);
    }

    /// <summary>
    /// 获取所有地形数据
    /// </summary>
    /// <returns></returns>
    public List<GroundHexagons> GetListGroundHexagons()
    {
        List<GroundHexagons> listData = new List<GroundHexagons>();
        foreach (var item in dicGroundHexagons)
        {
            listData.Add(item.Value);
        }
        return listData;
    }

    /// <summary>
    /// 获取环绕四周的地形
    /// </summary>
    /// <param name="groundHexagons"></param>
    /// <returns></returns>
    public List<GroundHexagons> GetGroundHexagonsForAround(GroundHexagons groundHexagons)
    {
        List<GroundHexagons> listData = new List<GroundHexagons>();
        Vector3 offsetPosition = groundHexagons.groundHexagonsData.coordinatesForOffset;
        listData = GetGroundHexagons(listData, offsetPosition + new Vector3(1, 0, 0));
        listData = GetGroundHexagons(listData, offsetPosition + new Vector3(-1, 0, 0));
        listData = GetGroundHexagons(listData, offsetPosition + new Vector3(0, 0, 1));
        listData = GetGroundHexagons(listData, offsetPosition + new Vector3(0, 0, -1));
        if (offsetPosition.x % 2 == 0)
        {
            listData = GetGroundHexagons(listData, offsetPosition + new Vector3(1, 0, 1));
            listData = GetGroundHexagons(listData, offsetPosition + new Vector3(-1, 0, 1));
        }
        else
        {
            listData = GetGroundHexagons(listData, offsetPosition + new Vector3(-1, 0, -1));
            listData = GetGroundHexagons(listData, offsetPosition + new Vector3(1, 0, -1));
        }

        return listData;
    }

    /// <summary>
    /// 根据偏移坐标获取数据
    /// </summary>
    /// <param name="offsetPosition"></param>
    /// <returns></returns>
    public GroundHexagons GetGroundHexagons(Vector3 offsetPosition)
    {
        if (dicGroundHexagons.TryGetValue(offsetPosition, out GroundHexagons value))
        {
            return value;
        }
        return null;
    }

    protected List<GroundHexagons> GetGroundHexagons(List<GroundHexagons> listData, Vector3 offsetPosition)
    {
        if (listData == null)
            listData = new List<GroundHexagons>();
        GroundHexagons groundHexagonsData = GetGroundHexagons(offsetPosition);
        if (groundHexagonsData != null)
            listData.Add(groundHexagonsData);
        return listData;
    }

    /// <summary>
    /// 获取势力范围
    /// </summary>
    /// <param name="belongId"></param>
    /// <returns></returns>
    public List<GroundHexagons> GetInfluence(string belongId)
    {
        if (dicInfluence.TryGetValue(belongId, out List<GroundHexagons> listData))
        {
            return listData;
        }
        return null;
    }

    /// <summary>
    /// 增加地面数据
    /// </summary>
    /// <param name="data"></param>
    public void AddGroundHexagons(GroundHexagons data)
    {
        dicGroundHexagons.Add(data.groundHexagonsData.coordinatesForOffset, data);
    }

    /// <summary>
    /// 增加势力范围
    /// </summary>
    /// <param name="belongId"></param>
    /// <param name="groundHexagons"></param>
    public void AddInfluence(string belongId, GroundHexagons groundHexagons)
    {
        if (dicInfluence.TryGetValue(belongId, out List<GroundHexagons> listData))
        {
            listData.Add(groundHexagons);
        }
        else
        {
            dicInfluence.Add(belongId, new List<GroundHexagons>() { groundHexagons });
        }
    }

    /// <summary>
    /// 获取区域类型模型
    /// </summary>
    /// <param name="areaType"></param>
    /// <param name="modelName"></param>
    /// <returns></returns>
    public GameObject GetAreaTypeModel(AreaTypeEnum areaType, string modelName)
    {
        //根据不同的地块生成不同的资源
        if (dicModel.TryGetValue(modelName, out GameObject value))
        {
            return value;
        }
        string path = "";
        switch (areaType)
        {
            case AreaTypeEnum.Base:
                path = Path_AreaType_Base;
                break;
            case AreaTypeEnum.Null:
            case AreaTypeEnum.BaseInit:
                path = Path_AreaType_Null;
                break;
            case AreaTypeEnum.Building:
                path = Path_AreaType_Building;
                break;
            case AreaTypeEnum.Gold:
                path = Path_AreaType_Gold;
                break;
        }
        GameObject objModel = LoadResourcesUtil.SyncLoadData<GameObject>(path + "/" + modelName);
        dicModel.Add(modelName, objModel);
        return objModel;
    }

    /// <summary>
    /// 随机获取区域类型信息
    /// </summary>
    /// <param name="areaType"></param>
    /// <returns></returns>
    public AreaTypeInfoBean GetRandomAreaTypeInfo(AreaTypeEnum areaType)
    {
        List<AreaTypeInfoBean> listData = new List<AreaTypeInfoBean>();
        switch (areaType)
        {
            case AreaTypeEnum.Base:
                listData = listAreaTypeInfoForBase;
                break;
            case AreaTypeEnum.Null:
            case AreaTypeEnum.BaseInit:
                listData = listAreaTypeInfoForNull;
                break;
            case AreaTypeEnum.Building:
                listData = listAreaTypeInfoForBuilding;
                break;
            case AreaTypeEnum.Gold:
                listData = listAreaTypeInfoForGold;
                break;
        }
        return RandomUtil.GetRandomDataByList(listData);
    }


    #region 获取数据回调
    void IAreaTypeInfoView.GetAreaTypeInfoSuccess<T>(T data, Action<T> action)
    {
        action?.Invoke(data);
    }

    void IAreaTypeInfoView.GetAreaTypeInfoFail(string failMsg, Action action)
    {

    }
    #endregion
}
