/*
* FileName: UserGroundData 
* Author: AppleCoffee 
* CreateTime: 2021-01-11-10:53:59 
*/

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UserSceneDataController : BaseMVCController<UserSceneDataModel, IUserSceneDataView>
{

    public UserSceneDataController(BaseMonoBehaviour content, IUserSceneDataView view) : base(content, view)
    {

    }

    public override void InitData()
    {

    }

    /// <summary>
    /// 保存用户数据
    /// </summary>
    /// <param name="userSceneData"></param>
    public void SetUserSceneData(UserSceneDataBean userSceneData)
    {
        GetModel().SetUserSceneDataData(userSceneData);
    }

    /// <summary>
    /// 获取数据
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public UserSceneDataBean GetUserSceneDataDataById(string groundId,Action<UserSceneDataBean> action)
    {
        UserSceneDataBean data = GetModel().GetUserSceneDataDataById(groundId);
        if (data == null) {
            GetView().GetUserSceneDataFail("没有数据",null);
            return null;
        }
        GetView().GetUserSceneDataSuccess<UserSceneDataBean>(data, action);
        return data;
    }

    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <param name="action"></param>
    public void GetAllUserSceneDataData(Action<List<UserSceneDataBean>> action)
    {
        List<UserSceneDataBean> listData = GetModel().GetAllUserSceneDataData();
        if (CheckUtil.ListIsNull(listData))
        {
            GetView().GetUserSceneDataFail("没有数据", null);
        }
        else
        {
            GetView().GetUserSceneDataSuccess<List<UserSceneDataBean>>(listData, action);
        }
    }

} 