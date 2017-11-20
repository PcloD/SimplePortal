using UnityEngine;

public class PortalSource : MonoBehaviour
{
    [SerializeField] private PortalTarget m_portalTarget;
    [SerializeField] private MeshRenderer m_portalMesh;

    private Transform m_transform;
    private Transform m_playerCamera;
    private Vector3 m_playerCameraDirection;

    private Vector3 m_zeroVector3 = Vector3.zero;
    private Vector3 m_oneVector3 = Vector3.one;
    private Matrix4x4 m_mirrorMatrix;
    private Matrix4x4 m_playerCameraMatrix;

    private void Awake()
    {
        m_transform = transform;

        m_mirrorMatrix = Matrix4x4.TRS(m_zeroVector3, Quaternion.AngleAxis(180, m_transform.up), m_oneVector3);
        m_playerCameraMatrix = m_mirrorMatrix * m_transform.worldToLocalMatrix;
    }


    private void Start()
    {
        InitCamera();
        InitProtal();
    }


    private void InitCamera()
    {
        m_playerCamera = SimpleMovement.Instance.transform;
    }


    private void InitProtal()
    {
        Material material = m_portalMesh.material;
        material.SetTexture("_MainTex", m_portalTarget.TargetRenderTexture);

        SimpleMovement.Instance.RegisterPortalSource(this);
    }


    public void UpdatePosition()
    {
        Vector3 playerCameraPosition = Vec4ToVec3(m_playerCameraMatrix * Vec3ToVec4(m_playerCamera.position));
        Quaternion playerCameraRotation = QuaternionFromMatrix(m_playerCameraMatrix) * m_playerCamera.rotation;

        m_portalTarget.CameraTransform.position = m_portalTarget.PortalTransform.TransformPoint(playerCameraPosition);
        m_portalTarget.CameraTransform.rotation = m_portalTarget.PortalTransform.rotation * playerCameraRotation;
    }


    private Vector4 Vec3ToVec4(Vector3 v)
    {
        return new Vector4(v.x, v.y, v.z, 1.0f);
    }


    private Vector3 Vec4ToVec3(Vector4 v)
    {
        return new Vector3(v.x, v.y, v.z);
    }


    private Quaternion QuaternionFromMatrix(Matrix4x4 m)
    {
        return Quaternion.LookRotation(m.GetColumn(2), m.GetColumn(1));
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Main Camera")
        {
            collider.transform.position = m_portalTarget.CameraTransform.position;
            collider.transform.rotation = m_portalTarget.CameraTransform.rotation;
        }
    }
}
