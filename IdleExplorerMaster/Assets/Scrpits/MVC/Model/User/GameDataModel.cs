/*
* FileName: GameData 
* Author: AppleCoffee 
* CreateTime: 2021-01-11-10:51:44 
*/

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class GameDataModel : BaseMVCModel
{
    protected GameDataService serviceGameData;

    public override void InitData()
    {
        serviceGameData = new GameDataService();
    }

    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <returns></returns>
    public List<GameDataBean> GetAllGameDataData()
    {
        List<GameDataBean> listData = serviceGameData.QueryAllData();
        return listData;
    }

    /// <summary>
    /// 获取游戏数据
    /// </summary>
    /// <returns></returns>
    public GameDataBean GetGameDataData()
    {
        GameDataBean data = serviceGameData.QueryData();
        if (data == null)
            data = new GameDataBean();
        return data;
    }

    /// <summary>
    /// 根据ID获取数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public List<GameDataBean> GetGameDataDataById(long id)
    {
        List<GameDataBean> listData = serviceGameData.QueryDataById(id);
        return listData;
    }

    /// <summary>
    /// 保存游戏数据
    /// </summary>
    /// <param name="data"></param>
    public void SetGameDataData(GameDataBean data)
    {
        serviceGameData.UpdateData(data);
    }

}