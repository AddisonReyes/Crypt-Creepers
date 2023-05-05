using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    float originalSpeed;
    float waterSpeed;
    Player player;
    [SerializeField] float speedReductionRatio = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        originalSpeed = player.speed;
        //waterSpeed = originalSpeed * speedReductionRatio;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player.speed == originalSpeed)
            {
                player.speed *= speedReductionRatio;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.speed = originalSpeed;
        }
    }
}
