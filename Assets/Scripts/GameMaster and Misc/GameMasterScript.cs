using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameMasterScript : MonoBehaviour
{
    public static GameMasterScript Instance { get; private set; }

    [Header("GUI")]
    public GUIScript gui;

    [Header("Music")]
    public Music music;
    public AudioSource backgroundMusic;

    [Header("Game Management")]
    public GameObject Player;
    public GameObject SpiderRoomHandler;
    public List<Transform> checkpoints = new List<Transform>();
    public int currentCheckpoint = 0;

    [Header("End Sequence")]
    public EndSequenceTrigger SequenceHandler;

    [Header("End Screen")]
    public GameObject endScreen;
    public TextMeshProUGUI TimeTaken;
    public TextMeshProUGUI ClocksText;
    public TextMeshProUGUI ClockNumber;
    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI GoldNumber;
    public TextMeshProUGUI DeathsText;
    public TextMeshProUGUI DeathsNumber;
    public TextMeshProUGUI FinalText;
    public TextMeshProUGUI FinalNumber;
    public TextMeshProUGUI NewBestTimeText;

    [Header("Game Stats")]
    private float timer = 0f;
    private int collectables = 0;
    private int goldCollectables = 0;
    private int deaths = 0;

    private Coroutine timerCoroutine;
    private bool ifEnd = false;

    public float Timer
    {
        get { return timer; }
    }

    public void Collect()
    {
        collectables++;
        Debug.Log("Collectables: " + collectables);
    }

    public void CollectGold()
    {
        goldCollectables++;
        Debug.Log("Gold Collectables: " + goldCollectables);
    }

    public void Die()
    {
        Debug.Log("Player has died");
        Music.Instance.StopSound();
        SequenceHandler.ResetSequence();
        deaths++;
        Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Player.transform.position = checkpoints[currentCheckpoint].position - Vector3.up;
        HandleSpiderRoom();
        Player.GetComponent<PlayerStats>().ResetHealth();
    }

    public void ShowText(string text)
    {
        gui.WriteText(text);
    }

    public void showEndScreen()
    {
        ifEnd = true;
        Music.Instance.StopSound();
        gui.Deactivate();
        SequenceHandler.StopSequence();
        StopCoroutine(timerCoroutine);
        Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Player.GetComponent<Rigidbody>().isKinematic = true;
        StartCoroutine(EndScreenCoroutine());
    }

    private IEnumerator EndScreenCoroutine()
    {
        endScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
        TimeTaken.text = FormatTimer(timer);
        TimeTaken.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        ClocksText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        ClockNumber.text = collectables.ToString();
        ClockNumber.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        GoldText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        GoldNumber.text = goldCollectables.ToString();
        GoldNumber.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        DeathsText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        DeathsNumber.text = deaths.ToString();
        DeathsNumber.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        FinalText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        float finalTimer = timer - collectables - goldCollectables * 5 + deaths * 10;
        FinalNumber.text = FormatTimer(finalTimer);
        FinalNumber.gameObject.SetActive(true);
        if (PlayerPrefs.HasKey("BestTime"))
        {
            if (finalTimer < PlayerPrefs.GetFloat("BestTime"))
            {
                PlayerPrefs.SetFloat("BestTime", finalTimer);
                NewBestTimeText.gameObject.SetActive(true);
            }
        }
        else
        {
            PlayerPrefs.SetFloat("BestTime", finalTimer);
            NewBestTimeText.gameObject.SetActive(true);
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        timerCoroutine = StartCoroutine(StartTimer());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            currentCheckpoint++;
            Debug.Log("Checkpoint: " + currentCheckpoint);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            currentCheckpoint--;
            Debug.Log("Checkpoint: " + currentCheckpoint);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Teleport();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            showEndScreen();
        }

        if(Input.GetKeyDown(KeyCode.R) && ifEnd)
        {
            backgroundMusic.Stop();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            backgroundMusic.Play();
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Deleted all player prefs");
            PlayerPrefs.DeleteAll();
        }
    }

    private void Teleport()
    {
        Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Player.transform.position = checkpoints[currentCheckpoint].position;
    }

    private void HandleSpiderRoom()
    {
        if (SpiderRoomHandler != null)
        {
            SpiderRoomHandler.GetComponent<SpiderRoomHandler>().Reset();
        }
    }

    private IEnumerator StartTimer()
    {
        while (true)
        {
            yield return null;
            timer += Time.deltaTime;
        }
    }

    string FormatTimer(float timer)
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        int milliseconds = Mathf.FloorToInt((timer * 1000) % 1000);
        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}
