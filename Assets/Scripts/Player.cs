using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int health = 3;
    public float speed = 0.0f;
    [SerializeField] float fireRate = 1;
    
    bool invensibility = false;
    [SerializeField] int invensibilityTime = 3;

    [SerializeField] Transform aim;
    [SerializeField] new Camera camera;
    [SerializeField] Transform bulletPrefab;
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer spriteRenderer;
    
    bool powerShotEnabled = false;
    bool gunLoaded = true;

    float horizontal;
    float vertical;

    Vector3 moveDirection;
    Vector2 facingDirection;

    public int Health
    {
        get => health;
        set
        {
            health = value;
            UIManager.Instance.UpdateUIHealth(Health);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = health;
    }

    // Update is called once per frame
    void Update()
    {
        // Player Movement
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        moveDirection.x = horizontal;
        moveDirection.y = vertical;
        transform.position += moveDirection * Time.deltaTime * speed;

        // Aim Movement
        facingDirection = camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        aim.position = transform.position + (Vector3)facingDirection.normalized;
        
        // Bullet
        if (Input.GetMouseButton(0) && gunLoaded) 
        {
            gunLoaded = false;
            float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            Transform bulletClone = Instantiate(bulletPrefab, transform.position, targetRotation);

            if (powerShotEnabled)
            {
                bulletClone.GetComponent<Bullet>().powerShot = true;
            }

            StartCoroutine(ReloadGun());
        }

        anim.SetFloat("Speed", moveDirection.magnitude);
        if (aim.position.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else if (aim.position.x < transform.position.x)
        {
            spriteRenderer.flipX = false;
        }

    }

    public void TakeDamage()
    {
        if (invensibility)
        {
            return;            
        }

        Health--;
        invensibility = true;
        StartCoroutine(Invensibility());

        if (Health <= 0)
        {
            GameManager.Instance.gameOver = true;
            UIManager.Instance.ShowGameOverScreen();
        }
    }

    IEnumerator ReloadGun()
    {
        yield return new WaitForSeconds(1/fireRate);
        gunLoaded = true;
    }

    IEnumerator Invensibility()
    {
        yield return new WaitForSeconds(invensibilityTime);
        invensibility = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUp"))
        {
            switch(collision.GetComponent<PowerUp>().powerUpType)
            {
                case PowerUp.PowerUpType.FireRateIncrease:
                    fireRate++;
                    break;

                case PowerUp.PowerUpType.PowerShot:
                    powerShotEnabled = true;
                    break;

                default:
                    break;
            }

            Destroy(collision.gameObject, 0.1f);
        }
    }
}
