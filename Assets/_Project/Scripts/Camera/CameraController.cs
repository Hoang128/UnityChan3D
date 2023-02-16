using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum CameraMode
{
    NORMAL,
    HIGH_SPEED,
    JUMP
}

[System.Serializable]
public class CameraData
{
    public float distanceH;
    public float distanceV;
    public float lerpRatio;
    public Vector3 relativeLookPoint;
    public float fov;
}

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private PlayerController cameraTarget;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotateSpeed;
    [SerializeField]
    private float changeViewSpeed;
    [SerializeField]
    private CameraMode cameraMode;
    [SerializeField]
    private KVPList<CameraMode, CameraData> cameraSettings;
    private Camera camera;

    private CameraMode cameraModeAdd;

    public CameraMode CameraModeAdd { get => cameraModeAdd; set => cameraModeAdd = value; }
    public CameraMode CameraMode { get => cameraMode; set => cameraMode = value; }

    // Start is called before the first frame update
    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3 distance = (transform.position - GetLookPoint()).normalized * cameraSettings[cameraMode].distanceH;
        distance.y = cameraSettings[cameraMode].distanceV;
        Vector3 position = GetLookPoint() + distance;

        transform.position  = Vector3.Lerp(transform.position, position, cameraSettings[cameraMode].lerpRatio);
        transform.rotation = Quaternion.LookRotation(GetLookPoint() - transform.position);

        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, cameraSettings[cameraMode].fov, changeViewSpeed * Time.deltaTime);
    }

    public Vector3 GetLookPoint()
    {
        Vector3 lookPoint = cameraTarget.transform.position;
        lookPoint += cameraTarget.transform.forward * cameraSettings[cameraMode].relativeLookPoint.z;
        lookPoint += cameraTarget.transform.right * cameraSettings[cameraMode].relativeLookPoint.x;
        lookPoint += cameraTarget.transform.up * cameraSettings[cameraMode].relativeLookPoint.y;
        return lookPoint;
    }

#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        UnityEditor.Handles.DrawWireDisc(GetLookPoint(), Vector3.up, 0.1f);
        UnityEditor.Handles.DrawLine(GetLookPoint() + Vector3.down, GetLookPoint() + Vector3.up);
    }
#endif
}
