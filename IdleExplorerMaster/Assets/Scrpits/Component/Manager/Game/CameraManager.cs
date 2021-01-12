using UnityEditor;
using UnityEngine;

public class CameraManager : BaseManager
{
    protected Camera mainCamera;

    protected float maxXMove;
    protected float maxYMove;
    protected float maxZMove;

    protected float offsetMove = 3;

    protected float maxOrthographicSize = 10f;
    protected float minOrthographicSize = 3f;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    /// <summary>
    /// 获取主摄像头
    /// </summary>
    /// <returns></returns>
    public Camera GetMainCamera()
    {
        return mainCamera;
    }

    /// <summary>
    /// 设置最大边界
    /// </summary>
    /// <param name="maxX"></param>
    /// <param name="maxY"></param>
    /// <param name="maxZ"></param>
    public void SetMaxBorder(float maxX, float maxY, float maxZ)
    {
        this.maxXMove = maxX;
        this.maxYMove = maxY;
        this.maxZMove = maxZ;
        SetCameraPosition(mainCamera.transform.position);
        SetCameraOrthographicSize(mainCamera.orthographicSize);
    }

    /// <summary>
    /// 设置摄像头坐标
    /// </summary>
    /// <param name="position"></param>
    public void SetCameraPosition(Vector3 position)
    {
        //边界设置
        if (position.x > (maxXMove + offsetMove))
        {
            position.x = maxXMove + offsetMove;
        }
        if (position.x < 0 - offsetMove)
        {
            position.x = 0 - offsetMove;
        }
        if (position.z > (maxZMove + offsetMove))
        {
            position.z = maxZMove + offsetMove;
        }
        if (position.z < 0 - offsetMove)
        {
            position.z = 0 - offsetMove;
        }
        mainCamera.transform.position = position;
    }

    /// <summary>
    /// 设置镜头显示大小
    /// </summary>
    /// <param name="size"></param>
    public void SetCameraOrthographicSize(float size)
    {
        if (size > maxOrthographicSize)
        {
            size = maxOrthographicSize;
        }
        else if (size < minOrthographicSize)
        {
            size = minOrthographicSize;
        }
        mainCamera.orthographicSize = size;
    }
}