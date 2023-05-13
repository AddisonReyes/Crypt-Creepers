using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 10;
    [SerializeField] float speed = 1;
    [SerializeField] int scorePoints = 100;
    [SerializeField] Animator anim;
    float auxX;
    float auxY;

    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().transform;
        GameObject[] spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint");
        int randomSpawnPoint = Random.Range(0, spawnPoint.Length);
        transform.position = spawnPoint[randomSpawnPoint].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = player.position - transform.position;
        transform.position += (Vector3)direction * Time.deltaTime * speed;

        auxX = transform.position[0];
        if (transform.position[0] > auxX)
        {
            anim.SetBool("Derecha", true);
            anim.SetBool("Izquierda", false);
            anim.SetBool("Abajo", false);
            anim.SetBool("Arriba", false);
        }
        else if (transform.position[0] < auxX)
        {
            anim.SetBool("Derecha", false);
            anim.SetBool("Izquierda", true);
            anim.SetBool("Abajo", false);
            anim.SetBool("Arriba", false);
        }

        auxY = transform.position[1];
        if (transform.position[1] > auxY)
        {
            anim.SetBool("Derecha", false);
            anim.SetBool("Izquierda", false);
            anim.SetBool("Abajo", false);
            anim.SetBool("Arriba", true);
        }
        else if (transform.position[1] < auxY)
        {
            anim.SetBool("Derecha", false);
            anim.SetBool("Izquierda", false);
            anim.SetBool("Abajo", true);
            anim.SetBool("Arriba", false);
        }

        if (health <= 0)
        {
            GameManager.Instance.Score += scorePoints;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
