using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    Camera m_camera;
    [Header("Movement")]
    [SerializeField] Vector3 m_offset;
    [SerializeField] float m_smoothTime = 0.5f;
    Vector3 m_velocity;

    [SerializeField] float m_maxZoom = 10f;
    [SerializeField] float m_minZoom = 5f;
    [SerializeField] float m_zoomLimiter = 50f;

    [Header("Shake")]
    [SerializeField] float m_duration;
    [SerializeField] float m_strength;
    [SerializeField] int m_virbrato;
    [SerializeField] float m_randomness;
    public bool m_shaking;

    private void Start()
    {
        m_camera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (GameManager.instance.m_activePlayers.Count == 0 || m_shaking) return;
        MoveCamera();
        Zoom();
    }

    private void Update()
    {
        if (m_shaking) CameraShake();

    }

    void MoveCamera()
    {
        Vector3 m_centerPoint = GetCenterPoint();

        Vector3 newPosition = m_centerPoint + m_offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref m_velocity, m_smoothTime);
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(m_maxZoom, m_minZoom, GetGreatestDistance() / m_zoomLimiter);
        m_camera.orthographicSize = Mathf.Lerp(m_camera.orthographicSize, newZoom, Time.deltaTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(GameManager.instance.m_activePlayers[0].gameObject.transform.position, Vector3.zero);
        for (int i = 0; i < GameManager.instance.m_activePlayers.Count; i++)
        {
            bounds.Encapsulate(GameManager.instance.m_activePlayers[i].transform.position);
        }
        if(bounds.size.x > bounds.size.y)
        {
            return bounds.size.x;
        }
        else
        {
            return bounds.size.y;
        }        
    }

    private Vector3 GetCenterPoint()
    {
        if (GameManager.instance.m_activePlayers.Count == 1)
        {
            return GameManager.instance.m_activePlayers[0].gameObject.transform.position;
        }

        var bounds = new Bounds(GameManager.instance.m_activePlayers[0].gameObject.transform.position, Vector3.zero);
        for (int i = 0; i < GameManager.instance.m_activePlayers.Count; i++)
        {
            bounds.Encapsulate(GameManager.instance.m_activePlayers[i].transform.position);
        }

        return bounds.center;
    }

    public void CameraShake()
    {
        
        transform.DOShakePosition(m_duration, m_strength, m_virbrato, m_randomness, false).OnComplete(() => { m_shaking = false; });
        
    }

    
}
