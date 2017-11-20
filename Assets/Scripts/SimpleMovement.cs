using UnityEngine;
using System.Collections.Generic;

public class SimpleMovement : MonoBehaviour
{
    public static SimpleMovement Instance
    {
        get { return m_instance; }
    }
    private static SimpleMovement m_instance;

    public float m_moveSpeed = 10f;
    public float m_rotateSpeed = 100f;

    private Transform m_transform;
    private Vector3 m_moveDirection;
    private Vector3 m_rotateDirection;
    private List<PortalSource> m_portalSources = new List<PortalSource>();

    private void Awake()
    {
        m_instance = this;
        m_transform = transform;
    }

    private void Update()
    {
        m_moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        m_moveDirection *= m_moveSpeed * Time.deltaTime;
        m_transform.Translate(m_moveDirection);

        UpdateRotateAxis(Time.deltaTime);
        m_rotateDirection = m_transform.up;
        m_rotateDirection *= m_rotateAxis * m_rotateSpeed * Time.deltaTime;
        m_transform.Rotate(m_rotateDirection);

        if (m_portalSources.Count > 0)
        {
            for (int i = 0; i < m_portalSources.Count; i++)
            {
                m_portalSources[i].UpdatePosition();
            }
        }
    }


    private float m_rotateAxis = 0;
    private void UpdateRotateAxis(float delta)
    {
        delta *= 10;

        if (Input.GetKey(KeyCode.J))
        {
            m_rotateAxis -= delta;

            if (m_rotateAxis < -1)
            {
                m_rotateAxis = -1;
            }
        }
        else if (Input.GetKey(KeyCode.L))
        {
            m_rotateAxis += delta;

            if (m_rotateAxis > 1)
            {
                m_rotateAxis = 1;
            }
        }
        else
        {
            m_rotateAxis = 0;
        }
    }


    public void RegisterPortalSource(PortalSource portalSource)
    {
        m_portalSources.Add(portalSource);
    }
}
