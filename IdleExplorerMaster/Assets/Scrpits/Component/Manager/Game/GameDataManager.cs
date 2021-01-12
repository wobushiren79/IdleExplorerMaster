using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameDataManager : BaseManager,IUserSceneDataView
{
    public UserSceneDataBean userSceneData;
    public UserSceneDataController userSceneDataController;

    private void Awake()
    {
        userSceneDataController = new UserSceneDataController(this,this);
    }

    public UserSceneDataBean CreateNewScene()
    {
        userSceneData = new UserSceneDataBean(20, 20, 1);
        return userSceneData;
    }

    public UserSceneDataBean GetSceneData()
    {
        return userSceneData;
    }

    /// <summary>
    /// 保存数据
    /// </summary>
    public void SaveSceneData()
    {
        if (userSceneData == null || CheckUtil.StringIsNull(userSceneData.GetPlayerBelongId()))
            return;
        userSceneDataController.SetUserSceneData(userSceneData);
    }

    /// <summary>
    /// 设置地面数据
    /// </summary>
    /// <param name="listData"></param>
    public void SetGroundData(List<GroundHexagonsBean> listData)
    {
        UserSceneDataBean userSceneData = GetSceneData();
        userSceneData.SetGroundData(listData);
    }

    #region 数据回调
    public void GetUserSceneDataSuccess<T>(T data, Action<T> action)
    {
    }

    public void GetUserSceneDataFail(string failMsg, Action action)
    {
    }
    #endregion
}