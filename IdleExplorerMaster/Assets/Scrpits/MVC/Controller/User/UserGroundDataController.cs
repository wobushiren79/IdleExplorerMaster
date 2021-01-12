/*
* FileName: UserGroundData 
* Author: AppleCoffee 
* CreateTime: 2021-01-11-10:53:59 
*/

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UserGroundDataController : BaseMVCController<UserGroundDataModel, IUserGroundDataView>
{

    public UserGroundDataController(BaseMonoBehaviour content, IUserGroundDataView view) : base(content, view)
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
    public UserGroundDataBean GetUserGroundDataDataById(string groundId,Action<UserGroundDataBean> action)
    {
        UserGroundDataBean data = GetModel().GetUserGroundDataDataById(groundId);
        if (data == null) {
            GetView().GetUserGroundDataFail("没有数据",null);
            return null;
        }
        GetView().GetUserGroundDataSuccess<UserGroundDataBean>(data, action);
        return data;
    }

    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <param name="action"></param>
    public void GetAllUserGroundDataData(Action<List<UserGroundDataBean>> action)
    {
        List<UserGroundDataBean> listData = GetModel().GetAllUserGroundDataData();
        if (CheckUtil.ListIsNull(listData))
        {
            GetView().GetUserGroundDataFail("没有数据", null);
        }
        else
        {
            GetView().GetUserGroundDataSuccess<List<UserGroundDataBean>>(listData, action);
        }
    }

} 