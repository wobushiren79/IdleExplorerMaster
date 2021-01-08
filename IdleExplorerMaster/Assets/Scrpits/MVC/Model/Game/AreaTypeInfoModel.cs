/*
* FileName: AreaTypeInfo 
* Author: AppleCoffee 
* CreateTime: 2021-01-08-14:54:31 
*/

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class AreaTypeInfoModel : BaseMVCModel
{
    protected AreaTypeInfoService serviceAreaTypeInfo;

    public override void InitData()
    {
        serviceAreaTypeInfo = new AreaTypeInfoService();
    }

    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <returns></returns>
    public List<AreaTypeInfoBean> GetAllAreaTypeInfoData()
    {
        List<AreaTypeInfoBean> listData = serviceAreaTypeInfo.QueryAllData();
        return listData;
    }

    /// <summary>
    /// 获取游戏数据
    /// </summary>
    /// <returns></returns>
    public AreaTypeInfoBean GetAreaTypeInfoData()
    {
        AreaTypeInfoBean data = serviceAreaTypeInfo.QueryData();
        if (data == null)
            data = new AreaTypeInfoBean();
        return data;
    }

    /// <summary>
    /// 根据ID获取数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public List<AreaTypeInfoBean> GetAreaTypeInfoDataById(long id)
    {
        List<AreaTypeInfoBean> listData = serviceAreaTypeInfo.QueryDataById(id);
        return listData;
    }

    /// <summary>
    /// 保存游戏数据
    /// </summary>
    /// <param name="data"></param>
    public void SetAreaTypeInfoData(AreaTypeInfoBean data)
    {
        serviceAreaTypeInfo.UpdateData(data);
    }

}