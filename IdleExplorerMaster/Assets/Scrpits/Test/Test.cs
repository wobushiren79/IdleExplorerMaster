using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Test : BaseMonoBehaviour
{


    private void Start()
    {
        GroundBuildHandler.Instance.BuildGroundHexagons(1, 10, 10);
    }


}
