using UnityEditor;
using UnityEngine;

public class CameraHandler : BaseHandler<CameraHandler, CameraManager>
{

    /// <summary>
    /// 初始化摄像头
    /// </summary>
    /// <param name="groundW"></param>
    /// <param name="groundH"></param>
    public void InitCamera()
    {
        UserGroundDataBean userGroundData = GroundBuildHandler.Instance.currentGroundData;
        float maxX = GroundBuildHandler.Instance.itemHexagonsX * userGroundData.groundX - GroundBuildHandler.Instance.itemHexagonsX;
        float maxZ = GroundBuildHandler.Instance.itemHexagonsZ * userGroundData.groundZ - GroundBuildHandler.Instance.itemHexagonsZ;
        manager.SetMaxBorder(maxX, 0, maxZ);
    }

    /// <summary>
    /// 移动摄像头
    /// </summary>
    /// <param name="deltaposition"></param>
    public void MoveCamera(Vector3 deltaposition)
    {
        Camera camera = manager.GetMainCamera();
        Vector3 movePosition = camera.transform.position + deltaposition;
        movePosition = Vector3.Lerp(camera.transform.position, movePosition, 0.3f);
        manager.SetCameraPosition(movePosition);
    }

    /// <summary>
    /// 移动摄像头到指定点
    /// </summary>
    /// <param name="position"></param>
    public void MoveCameraToPosition(Vector3 position)
    {
        manager.SetCameraPosition(position);
    }
    public void MoveCameraToPositionXZ(Vector3 position)
    {
        Camera camera = manager.GetMainCamera();
        manager.SetCameraPosition(new Vector3(position.x, camera.transform.position.y, position.z));
    }

    /// <summary>
    /// 缩放镜头
    /// </summary>
    /// <param name="scaleFactor"></param>
    public void ZoomCamera(float scaleFactor)
    {
        Camera camera = manager.GetMainCamera();
        manager.SetCameraOrthographicSize(camera.orthographicSize + scaleFactor);
    }

}