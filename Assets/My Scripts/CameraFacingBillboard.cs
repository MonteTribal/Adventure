using UnityEngine;
using System.Collections;

public class CameraFacingBillboard : MonoBehaviour
{
    private Camera m_Camera;

    void Start()
    {
        m_Camera = Camera.main;
    }
    
    void Update()
    {
        if (m_Camera)
        {
            transform.LookAt(m_Camera.transform);
        }
        else
        {
            m_Camera = Camera.main;
        }
    }
}