/*
* FileName: GameData 
* Author: AppleCoffee 
* CreateTime: 2021-01-11-10:51:44 
*/

using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public class GameDataService : BaseDataStorage<GameDataBean>
{
    protected readonly string saveFileName;

    public GameDataService()
    {
        saveFileName = "GameData";
    }

    /// <summary>
    /// 查询所有数据
    /// </summary>
    /// <returns></returns>
    public List<GameDataBean> QueryAllData()
    {
        return null; 
    }

    /// <summary>
    /// 查询游戏配置数据
    /// </summary>
    /// <returns></returns>
    public GameDataBean QueryData()
    {
        return BaseLoadData(saveFileName);
    }
        
    /// <summary>
    /// 通过ID查询数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public List<GameDataBean> QueryDataById(long id)
    {
        return null;
    }

    /// <summary>
    /// 更新数据
    /// </summary>
    /// <param name="data"></param>
    public void UpdateData(GameDataBean data)
    {
        BaseSaveData(saveFileName, data);
    }
}