using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameLauncher : BaseMonoBehaviour
{

    private void Awake()
    {
        UserGroundDataBean userGroundData = new UserGroundDataBean(20, 20);
        GroundBuildHandler.Instance.InitGround(userGroundData);
        CameraHandler.Instance.InitCamera();
    }

    private void Start()
    {
        UserGroundDataBean userGroundData = GroundBuildHandler.Instance.currentGroundData;
        //显示友方区域
        List<GroundHexagons> listPlayerData = GroundBuildHandler.Instance.manager.GetInfluence(userGroundData.playerId);
        for (int i = 0; i < listPlayerData.Count; i++)
        {
            GroundHexagons itemGroundHexagons = listPlayerData[i];

            if (itemGroundHexagons.groundHexagonsData.areaType == AreaTypeEnum.Base)
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