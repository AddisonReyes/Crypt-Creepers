using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int time = 30;
    public int difficulty = 1;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDownRutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CountDownRutine()
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
        }

        //GameOver
    }
}
