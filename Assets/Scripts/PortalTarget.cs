using UnityEngine;

public class PortalTarget : MonoBehaviour
{
    private const int PORTAL_SIZE = 4;

    public Transform CameraTransform { get { return m_cameraTransform; } }
    public Transform PortalTransform { get { return m_portalTransform; } }
    public RenderTexture TargetRenderTexture { get { return m_renderTexture; } }

    private Camera m_camera;
    private Transform m_cameraTransform;
    private Transform m_portalTransform;
    private RenderTexture m_renderTexture;

    private void Awake()
    {
        m_portalTransform = transform;

        m_renderTexture = RenderTexture.GetTemporary(Screen.width / PORTAL_SIZE, Screen.height / PORTAL_SIZE);

        m_camera = GetComponentInChildren<Camera>();
        m_camera.targetTexture = m_renderTexture;
        m_camera.enabled = true;

        m_cameraTransform = m_camera.transform;
    }
}
