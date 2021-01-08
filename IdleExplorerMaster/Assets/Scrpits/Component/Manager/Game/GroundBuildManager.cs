using System;
using System.Collections.Generic;
using UnityEngine;

public class GroundBuildManager : BaseManager, IAreaTypeInfoView
{
    //模型合集
    protected Dictionary<string, GameObject> dicModel = new Dictionary<string, GameObject>();

    protected List<GroundHexagonsBean> listGroundHexagonsData = new List<GroundHexagonsBean>();

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
    /// 增加地面数据
    /// </summary>
    /// <param name="data"></param>
    public void AddGroundHexagonsData(GroundHexagonsBean data)
    {
        listGroundHexagonsData.Add(data);
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
