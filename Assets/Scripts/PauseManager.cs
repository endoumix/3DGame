using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    
    public static PauseManager Instance { get; private set; }

    [SerializeField] private GameObject pausePanel;
    public bool IsPaused { get; private set; } = false; // стоит ли игра на паузе


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (pausePanel != null) pausePanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        IsPaused = !IsPaused; // переключаем состояние паузы
        if (pausePanel != null) pausePanel.SetActive(IsPaused);

        Time.timeScale = IsPaused ? 0f : 1f;
        Cursor.lockState = IsPaused ? CursorLockMode.None : CursorLockMode.Locked; // если пауза — показываем курсор и разблок его, иначе блок
        Cursor.visible = IsPaused;
    }

    public void ResumeGame() => TogglePause();

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");// снимаем паузу перед загрузкой меню
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}