using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [SerializeField] private GameObject message, duck;
    [SerializeField] private GameObject pipes, source, gameOver;
    [SerializeField] private Text scoreText;
    private float interval = 1f;
    private bool started;
    private int score;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        started = false;
        InvokeRepeating("SpawnPipes", 0f, interval);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // message.SetActive(true);
            Destroy(message);
            duck.SetActive(true);
            started = true;
        }
    }

    private void SpawnPipes()
    {
        if (!started) return;

        Instantiate(
            pipes,                      // Instancia os pilares
            source.transform.position,  // Na posição do objeto source
            Quaternion.identity         // Com rotação padrão (sem rotação, identidade)
        );
    }

    public void IncreaseScore(int score)
    {
        this.score += score;
        scoreText.text = this.score.ToString();
    }

    public void GameOver()
    {
        return;
        gameOver.SetActive(true);
        Time.timeScale = 0;
    }

    public int GetScore()
    {
        return score;
    }
}