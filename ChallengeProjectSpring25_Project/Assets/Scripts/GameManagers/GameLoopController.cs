using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoopController : MonoBehaviour
{
    public static GameLoopController Instance { get; private set; }
    public int maxWins;
    private int curWins;
    public TextMeshProUGUI curWinText;
    public TextMeshProUGUI maxWinText;
    public float displayTime;
    public GameObject fadeBackground;

    private Image fadeImage;

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

        if (fadeBackground != null)
        {
            fadeImage = fadeBackground.GetComponent<Image>();
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
        StartCoroutine(FadeIn());
        StartCoroutine(DisplayCurrentGameStage());
    }

    private IEnumerator DisplayCurrentGameStage()
    {
        if (FindFirstObjectByType<CardDrawingController>() != null)
        {
            curWinText.gameObject.SetActive(true);
            maxWinText.gameObject.SetActive(true);
            yield return new WaitForSeconds(displayTime);
            curWinText.gameObject.SetActive(false);
            maxWinText.gameObject.SetActive(false);
            FindFirstObjectByType<CardDrawingController>().StartDrawingProcess();
        }
    }

    private IEnumerator FadeIn()
    {
        Debug.Log("fade in called");
        if (fadeImage == null) yield break;

        fadeBackground.SetActive(true);
        Color color = fadeImage.color;
        color.a = 1;
        fadeImage.color = color;

        while (fadeImage.color.a > 0)
        {
            color.a -= Time.deltaTime;
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0;
        fadeImage.color = color;
        fadeBackground.SetActive(false);
    }

    public void FadeOutAndDo(System.Action onComplete = null)
    {
        StartCoroutine(FadeOutCoroutine(onComplete));
    }

    private IEnumerator FadeOutCoroutine(System.Action onComplete)
    {
        if (fadeImage == null) yield break;

        fadeBackground.SetActive(true);
        Color color = fadeImage.color;
        color.a = 0;
        fadeImage.color = color;

        while (fadeImage.color.a < 1)
        {
            color.a += Time.deltaTime;
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1;
        fadeImage.color = color;

        onComplete?.Invoke();
    }

    public void FadeOutAndLoadScene(string sceneName)
    {
        StartCoroutine(FadeOutThenLoad(sceneName));
    }

    private IEnumerator FadeOutThenLoad(string sceneName)
    {
        yield return FadeOutCoroutine(() => SceneManager.LoadScene(sceneName));
    }

    public void CountAWin()
    {
        curWins++;
        if (curWins == maxWins)
        {
            GameWin();
        }
    }

    public void CountALoss()
    {
        curWins--;
        if (curWins <= 0)
        {
            curWins = 0;
        }
    }

    private void GameWin()
    {
    }
}
