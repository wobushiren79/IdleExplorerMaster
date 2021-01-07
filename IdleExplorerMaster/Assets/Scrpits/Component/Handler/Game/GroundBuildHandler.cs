using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBuildHandler : BaseHandler<GroundBuildHandler, GroundBuildManager>
{
    public GroundHexagons modelForGroundHexagons;

    /// <summary>
    /// 创建六角地形
    /// </summary>
    /// <param name="hexagonsSize">地形大小</param>
    /// <param name="groundX">X轴范围</param>
    /// <param name="groundZ">Z轴范围</param>
    /// <param name="offsetX">X轴偏移角度</param>
    /// <param name="offsetZ">Z轴偏移角度</param>
    public void BuildGroundHexagons(float hexagonsSize, int groundX, int groundZ, float offsetX = 0, float offsetZ = 0)
    {
        float intervalX = hexagonsSize * 2 * 0.75f;
        float intervalZ = hexagonsSize * Mathf.Sqrt(3);

        for (int x = 0; x < groundX; x++)
        {
            for (int z = 0; z < groundZ; z++)
            {
                //偶数偏移
                float tempOffsetZ = (x % 2 == 0 ? intervalZ / 2f : 0);
                GroundHexagonsBean groundHexagonsData = new GroundHexagonsBean
                {
                    //设置偏移坐标
                    coordinatesForOffset = new Vector3(x, 0, z),
                    //设置世界坐标
                    coordinatesForWorld = new Vector3(intervalX * x + offsetX * x, 0, intervalZ * z + offsetZ * z + tempOffsetZ)
                };
                BuildItemGroundHexagons(groundHexagonsData);
                manager.AddGroundHexagonsData(groundHexagonsData);
            }
        }

        //扫描地形
        StartCoroutine(CoroutineForScanGround());
    }

    /// <summary>
    /// 创建单个六角地形
    /// </summary>
    /// <param name="groundHexagonsData"></param>
    public void BuildItemGroundHexagons(GroundHexagonsBean groundHexagonsData)
    {
        GameObject objGroundHexagons = Instantiate(gameObject, modelForGroundHexagons.gameObject, groundHexagonsData.coordinatesForWorld);
        GroundHexagons itemGroundHexagons = objGroundHexagons.GetComponent<GroundHexagons>();
        itemGroundHexagons.SetGroundHexagonsData(groundHexagonsData);
    }

    /// <summary>
    /// 协程-扫描地形
    /// </summary>
    /// <returns></returns>
    IEnumerator CoroutineForScanGround()
    {
        foreach (Progress progress in AstarPath.active.ScanAsync())
        {
            Debug.Log("Scanning... " + progress.description + " - " + (progress.progress * 100).ToString("0") + "%");
            yield return null;
        }
    }
}
