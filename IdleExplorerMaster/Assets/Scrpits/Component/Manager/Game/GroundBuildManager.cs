using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBuildManager : BaseManager
{
    protected List<GroundHexagonsBean> listGroundHexagonsData = new List<GroundHexagonsBean>();

    public void AddGroundHexagonsData(GroundHexagonsBean data)
    {
        listGroundHexagonsData.Add(data);
    }
}
