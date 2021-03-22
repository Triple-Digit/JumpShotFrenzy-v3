using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadows : MonoBehaviour
{
    Vector2 m_offset = new Vector2(-0.14f, -0.14f);
    SpriteRenderer m_shadowCaster, m_shadow;
    [SerializeField] Material m_shadowMaterial;
    [SerializeField] Color m_shadowColor;
    Transform m_casterTransform, m_shadowTransform;

    private void Start()
    {
        m_casterTransform = transform;
        m_shadowTransform = new GameObject().transform;
        m_shadowTransform.parent = m_casterTransform;
        m_shadowTransform.gameObject.name = "Shadow";
        m_shadowTransform.localRotation = Quaternion.identity;
        
        m_shadowCaster = GetComponent<SpriteRenderer>();
        m_shadow = m_shadowTransform.gameObject.AddComponent<SpriteRenderer>();

        m_shadow.material = m_shadowMaterial;
        m_shadow.color = m_shadowColor;
        m_shadow.sortingLayerName = m_shadowCaster.sortingLayerName;
        m_shadow.sortingOrder = m_shadowCaster.sortingOrder - 1;
        m_shadowTransform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void LateUpdate()
    {
        m_shadowTransform.position = new Vector2(m_casterTransform.position.x + m_offset.x, m_casterTransform.position.y + m_offset.y);

        m_shadow.sprite = m_shadowCaster.sprite;
    }
}
