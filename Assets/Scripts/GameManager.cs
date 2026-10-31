using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("ѕанели")]
    public GameObject winPanel;
    public GameObject losePanel;

    [Header("«вуки")]
    public AudioSource winSound;
    public AudioSource loseSound;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;//загрузка сцен
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;//отписываемс€ что нет ошибок
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // когда загружаетс€ сцена Room Ч ищем панели
        if (scene.name == "Room")
        {
            // находим HUDCanvas 
            GameObject hud = GameObject.Find("HUDCanvas");

            if (hud != null)
            {
                // находим даже выключенные панели
                Transform win = hud.transform.Find("WinPanel");
                Transform lose = hud.transform.Find("LosePanel");

                if (win != null) winPanel = win.gameObject;//сохран€ем ссылку на панель
                if (lose != null) losePanel = lose.gameObject;
            }
        }
    }

    public void LoadGame() { Time.timeScale = 1f; SceneManager.LoadScene("Room"); }
    public void LoadMenu() { Time.timeScale = 1f; SceneManager.LoadScene("Menu"); }

    public void WinGame()
    {
        if (winPanel != null) winPanel.SetActive(true);
        if (winSound != null) winSound.Play();
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoseGame()
    {
        if (losePanel != null) losePanel.SetActive(true);
        if (loseSound != null) loseSound.Play();
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}