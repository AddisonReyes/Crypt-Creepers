using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int time = 30;
    public int difficulty = 1;
    public bool gameOver = false;
    
    [SerializeField] int score;

    public int Score
    {
        get => score;
        set
        {
            score = value;
            UIManager.Instance.UpdateUIScore(Score);

            if (score % 1000 == 0)
            {
                difficulty++;
            }
        }
    }

    public int Time
    {
        get => time;
        set
        {
            time = value;
            UIManager.Instance.UpdateUITime(Time);
        }
    }

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
        Time = time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CountDownRutine()
    {
        while (Time > 0)
        {
            yield return new WaitForSeconds(1);
            Time--;
        }

        gameOver = true;
        UIManager.Instance.ShowGameOverScreen();
    } 

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }
}
