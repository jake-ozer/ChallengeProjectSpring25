using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoopController : MonoBehaviour
{
    public static GameLoopController Instance { get; private set; }
    public int maxWins;
    private int curWins;
    public TextMeshProUGUI curWinText;
    public TextMeshProUGUI maxWinText;
    public float displayTime;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    private void Update()
    {
        curWinText.text = "Current Victories: " + curWins.ToString();
        maxWinText.text = "Needed Victories: " + maxWins.ToString();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine("DisplayCurrentGameStage");
    }

    private IEnumerator DisplayCurrentGameStage()
    {
        curWinText.gameObject.SetActive(true);
        maxWinText.gameObject.SetActive(true);
        yield return new WaitForSeconds(displayTime);
        curWinText.gameObject.SetActive(false);
        maxWinText.gameObject.SetActive(false);
        FindFirstObjectByType<CardDrawingController>().StartDrawingProcess();
    }


    //called when a player wins a round
    public void CountAWin()
    {
        curWins++;
        if(curWins == maxWins)
        {
            GameWin();
        }
    }

    //called when a player loses a round
    public void CountALoss()
    {
        curWins--;
        if (curWins <= 0)
        {
            curWins = 0;
        }
    }

    //called when you win the game
    private void GameWin()
    {

    }
}
