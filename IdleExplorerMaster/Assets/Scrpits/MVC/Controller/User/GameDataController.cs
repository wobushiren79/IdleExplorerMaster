/*
* FileName: GameData 
* Author: AppleCoffee 
* CreateTime: 2021-01-11-10:51:44 
*/

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameDataController : BaseMVCController<GameDataModel, IGameDataView>
{

    public GameDataController(BaseMonoBehaviour content, IGameDataView view) : base(content, view)
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
    public GameDataBean GetGameDataData(Action<GameDataBean> action)
    {
        GameDataBean data = GetModel().GetGameDataData();
        if (data == null) {
            GetView().GetGameDataFail("没有数据",null);
            return null;
        }
        GetView().GetGameDataSuccess<GameDataBean>(null,action);
        return data;
    }

    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <param name="action"></param>
    public void GetAllGameDataData(Action<List<GameDataBean>> action)
    {
        List<GameDataBean> listData = GetModel().GetAllGameDataData();
        if (CheckUtil.ListIsNull(listData))
        {
            GetView().GetGameDataFail("没有数据", null);
        }
        else
        {
            GetView().GetGameDataSuccess<List<GameDataBean>>(listData, action);
        }
    }

    /// <summary>
    /// 根据ID获取数据
    /// </summary>
    /// <param name="action"></param>
    public void GetGameDataDataById(long id,Action<List<GameDataBean>> action)
    {
        List<GameDataBean> listData = GetModel().GetGameDataDataById(id);
        if (CheckUtil.ListIsNull(listData))
        {
            GetView().GetGameDataFail("没有数据", null);
        }
        else
        {
            GetView().GetGameDataSuccess<List<GameDataBean>>(listData, action);
        }
    }
} 