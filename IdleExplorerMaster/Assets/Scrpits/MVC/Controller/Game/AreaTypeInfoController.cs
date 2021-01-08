/*
* FileName: AreaTypeInfo 
* Author: AppleCoffee 
* CreateTime: 2021-01-08-14:54:31 
*/

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AreaTypeInfoController : BaseMVCController<AreaTypeInfoModel, IAreaTypeInfoView>
{

    public AreaTypeInfoController(BaseMonoBehaviour content, IAreaTypeInfoView view) : base(content, view)
    {

    }

    public override void InitData()
    {

    }

    /// <summary>
    /// 获取数据
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public AreaTypeInfoBean GetAreaTypeInfoData(Action<AreaTypeInfoBean> action)
    {
        AreaTypeInfoBean data = GetModel().GetAreaTypeInfoData();
        if (data == null) {
            GetView().GetAreaTypeInfoFail("没有数据",null);
            return null;
        }
        GetView().GetAreaTypeInfoSuccess<AreaTypeInfoBean>(null,action);
        return data;
    }

    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <param name="action"></param>
    public void GetAllAreaTypeInfoData(Action<List<AreaTypeInfoBean>> action)
    {
        List<AreaTypeInfoBean> listData = GetModel().GetAllAreaTypeInfoData();
        if (CheckUtil.ListIsNull(listData))
        {
            GetView().GetAreaTypeInfoFail("没有数据", null);
        }
        else
        {
            GetView().GetAreaTypeInfoSuccess<List<AreaTypeInfoBean>>(listData, action);
        }
    }

    /// <summary>
    /// 根据ID获取数据
    /// </summary>
    /// <param name="action"></param>
    public void GetAreaTypeInfoDataById(long id,Action<List<AreaTypeInfoBean>> action)
    {
        List<AreaTypeInfoBean> listData = GetModel().GetAreaTypeInfoDataById(id);
        if (CheckUtil.ListIsNull(listData))
        {
            GetView().GetAreaTypeInfoFail("没有数据", null);
        }
        else
        {
            GetView().GetAreaTypeInfoSuccess<List<AreaTypeInfoBean>>(listData, action);
        }
    }
} 