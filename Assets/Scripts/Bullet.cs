using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 6;
    [SerializeField] int bulletDamage = 5;
    [SerializeField] int health = 3;
    public bool powerShot = false;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 6);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(bulletDamage);

            if(!powerShot)
            {
                Destroy(gameObject);
            }

            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
