using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameLauncher : BaseMonoBehaviour
{

    private void Awake()
    {
        UserSceneDataBean userGroundData = GameDataHandler.Instance.manager.CreateNewScene();
        GroundBuildHandler.Instance.InitGround(userGroundData);
        CameraHandler.Instance.InitCamera();
        GameDataHandler.Instance.manager.SaveSceneData();

    }

    private void Start()
    {
        UserSceneDataBean userGroundData = GroundBuildHandler.Instance.currentGroundData;
        //显示友方区域
        List<GroundHexagons> listPlayerData = GroundBuildHandler.Instance.manager.GetInfluence(userGroundData.GetPlayerBelongId());
        for (int i = 0; i < listPlayerData.Count; i++)
        {
            GroundHexagons itemGroundHexagons = listPlayerData[i];

            if (itemGroundHexagons.groundHexagonsData.GetAreaType() == AreaTypeEnum.Base)
            {
                Vector3 basePosition = itemGroundHexagons.groundHexagonsData.coordinatesForWorld;
                CameraHandler.Instance.MoveCameraToPositionXZ(basePosition);
            }

            Action actionRolloverTerrainStart = () =>
            {
                itemGroundHexagons.areaType.ShowAreaType();
            };

            Action actionChangeDiscoveryComplete = () =>
            {
                itemGroundHexagons.areaTerrain.RolloverTerrain(actionRolloverTerrainStart, null);
            };

            itemGroundHexagons.ChangeDiscoveryStatus(AreaDiscoveryStatusEnum.Explore, actionChangeDiscoveryComplete);
        }

        UIHandler.Instance.manager.OpenUI(UIEnum.GameStart);
    }

}