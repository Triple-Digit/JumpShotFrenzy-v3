using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    [HideInInspector] public bool m_canControll;
    
    [Header("FXs")]
    [SerializeField] ParticleSystem m_dust, m_muzzleFlash;

    [Header("Movement Variables")]
    [SerializeField] GameObject m_spriteHolder;
    [SerializeField] [Range(0,0.999f)] float horizontalDamping;
    bool m_facingRight = true;
    float m_direction = 1f;
    float m_input;
    Rigidbody2D m_body;

    [Header("Jump Variables")]
    [SerializeField] float m_jumpForce = 3f;
    [SerializeField] Transform m_groundCheckPosition;
    [SerializeField] float m_groundCheckRadius = 0.4f;
    [SerializeField] LayerMask m_groundLayers;
    [SerializeField] float m_coyoteTime;
    [SerializeField] int m_extraJump = 0;
    float m_coyoteTimer;
    bool m_grounded;
    [SerializeField] int m_jumpCount = 0;
    [SerializeField] float m_extraJumpTimer = 0;
    
    [Header("Front Flip Variables")]
    [SerializeField] bool m_flipping = false;
    [SerializeField] float m_rotationAngle = 0f;
    [SerializeField] float m_rotationSpeed = 1f;
    [SerializeField] float m_resetOrientationSpeed = 4f;

    [Header("Shooting Variables")]
    [SerializeField] Transform m_shootingPoint;
    [SerializeField] GameObject m_bullet;
    [SerializeField] int m_ammo = 5;
    [SerializeField] int m_currentAmmo = 1;
    public float m_fireRate = 0.25f;
    float m_firerTimer;
    bool m_isShooting;
    [SerializeField] float m_multiShotTimer = 0f;

    private void Awake()
    {        
        m_body = GetComponent<Rigidbody2D>();
        m_canControll = true;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }   

    private void FixedUpdate()
    {
        FrontFlip();        
    }

    private void Update()
    {
        MovePlayer();
        CheckIfGrounded();
        ExtraJumpTimer();
        MultiShotTimer();
        FireRate();
        Shooting();
    }

    public void Move(InputAction.CallbackContext context)
    {
        m_input = context.ReadValue<Vector2>().x;
    }

    void MovePlayer()
    {
        if (!m_canControll)
        {
            m_body.velocity = new Vector2(0, 0);
        }
        else
        {
            float horizontalVelocity = m_body.velocity.x;
            horizontalVelocity += m_input;
            horizontalVelocity *= Mathf.Pow(1f - horizontalDamping, Time.deltaTime * 10f);
            m_body.velocity = new Vector2(horizontalVelocity, m_body.velocity.y);
        }

        if (m_input > 0 && !m_facingRight) ChangeFacingDirection();
        else if (m_input < 0 && m_facingRight) ChangeFacingDirection();
    }

    void ChangeFacingDirection()
    {
        if (m_grounded)
        {
            m_facingRight = !m_facingRight;
            m_direction *= -1f;
            PlayParticle(m_dust);
        }
        m_spriteHolder.transform.localScale = new Vector3(m_direction, 1f, 1f);
        
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(m_canControll)
        {
            if(m_coyoteTimer > 0)
            {                
                if (context.started)
                {
                    m_body.velocity = new Vector2(m_body.velocity.x, m_jumpForce);
                    m_coyoteTimer = -1f;
                    PlayParticle(m_dust);
                }
                if (context.canceled)
                {
                    m_body.velocity = new Vector2(m_body.velocity.x, m_body.velocity.y * 0.5f);
                    m_coyoteTimer = -1f;                    
                }                
            }
            else if(m_coyoteTimer <= 0 && m_jumpCount > 0)
            {
                if (context.started)
                {
                    m_body.velocity = new Vector2(m_body.velocity.x, m_jumpForce);
                    PlayParticle(m_dust);
                    m_jumpCount--;
                }
                if (context.canceled)
                {
                    m_body.velocity = new Vector2(m_body.velocity.x, m_body.velocity.y * 0.5f);                    
                    m_jumpCount--;
                }
            }
        }        
    }

    public void ExtraJump(float duration)
    {
        m_extraJumpTimer += duration;
    }

    void ExtraJumpTimer()
    {
        if (m_extraJumpTimer >= 0f)
        {
            m_extraJumpTimer -= Time.deltaTime;
            m_extraJump = 2;
        }
        else
        {
            m_extraJump = 0;
        }
    }

    void FrontFlip()
    {
        if (!m_canControll) return;
        m_flipping = !m_grounded;
        m_spriteHolder.gameObject.transform.rotation = Quaternion.Euler(0, 0, m_rotationAngle);

        if (m_flipping)
        {
            m_rotationAngle -= Time.deltaTime * 360 * m_rotationSpeed * m_direction;
        }

        else
        {
            if (m_rotationAngle < -1)
            {
                if (m_rotationAngle > -100)
                {
                    m_rotationAngle += Time.deltaTime * 360 * m_resetOrientationSpeed * m_direction;
                }
                if (m_rotationAngle < -90)
                {
                    m_rotationAngle -= Time.deltaTime * 360 * m_resetOrientationSpeed * m_direction;
                }
            }
            else
            {
                m_rotationAngle = 0;
            }
        }

        if (m_rotationAngle <= -360f)
        {
            m_rotationAngle = 0;
        }
    }

    void CheckIfGrounded()
    {
        m_grounded = Physics2D.OverlapCircle(m_groundCheckPosition.position, m_groundCheckRadius, m_groundLayers);
        if (m_grounded)
        {
            m_currentAmmo = m_ammo;
            m_coyoteTimer = m_coyoteTime;
            m_jumpCount = m_extraJump;
        }
        else
        {
            m_coyoteTimer -= Time.deltaTime;
        }        
    }

    private void OnDrawGizmosSelected()
    {
        if (m_groundCheckPosition == null) return;
        Gizmos.DrawWireSphere(m_groundCheckPosition.position, m_groundCheckRadius);
    }

    

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed) m_isShooting = true;
        if (context.canceled) m_isShooting = false;
    }

    void FireRate()
    {
        if (m_firerTimer > 0)
        {
            m_firerTimer -= Time.deltaTime;
        }
    }    

    void Shooting()
    {        
        if (!m_grounded && m_isShooting)
        {
            if(m_currentAmmo > 0 && m_firerTimer <= 0)
            {
                m_firerTimer = m_fireRate;
                m_currentAmmo--;
                if (!m_facingRight)
                {
                    GameObject bullet = Instantiate(m_bullet, new Vector2(m_shootingPoint.transform.position.x, m_shootingPoint.transform.position.y), m_spriteHolder.transform.rotation);
                    bullet.GetComponent<Bullet>().ChangeDirection();
                }
                else
                {
                    GameObject bullet = Instantiate(m_bullet, new Vector2(m_shootingPoint.transform.position.x, m_shootingPoint.transform.position.y), m_spriteHolder.transform.rotation);
                }
                PlayParticle(m_muzzleFlash);
            }            
        }
    }

    public void MultiShot(float duration)
    {
        m_multiShotTimer += duration;
    }

    void MultiShotTimer()
    {
        if (m_multiShotTimer >= 0f)
        {
            m_multiShotTimer -= Time.deltaTime;
            m_ammo = 3;
        }
        else
        {
            m_ammo = 1;
        }
    }

    void PlayParticle(ParticleSystem particle)
    {
        particle.Play();
    }
}
