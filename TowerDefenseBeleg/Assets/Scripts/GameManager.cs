using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    public static bool GameIsOver;
    
    public static int Money;
    public static int Lives;
    public static int Waves;
    
    public SceneController sceneController;
    
    [Header("UI")]
    
    [Tooltip("The reference to the Game Over UI element.")]
    [SerializeField] private GameObject gameOverUI;
    
    [Tooltip("The reference to the level won UI element.")]
    [SerializeField] private GameObject levelWonUI;
    
    [Tooltip("The reference to the pause menu ui.")]
    [SerializeField] private GameObject pauseMenuUI;
    
    [Header("Money")]
    
    [Tooltip("The amount of money the player starts with.")]
    [SerializeField] private int startMoney;
    public int StartMoney => startMoney;
    
    [Tooltip("The money ui text object.")]
    [SerializeField] private TMP_Text moneyText;
    
    [Header("Lives")]

    [Tooltip("The amount of lives the player starts with.")]
    [SerializeField] private int startLives;
    public int StartLives => startLives;
    
    [Tooltip("The lives ui text object.")]
    [SerializeField] private TMP_Text livesText;
    
    [Header("Waves")]
    
    [Tooltip("The amount of max waves.")]
    [SerializeField] private int waveAmount;
    
    [Tooltip("The waves ui text object.")]
    [SerializeField] private TMP_Text wavesText;

    private void Start() {
        GameIsOver = false;
        Money = startMoney;
        Lives = startLives;
        Waves = 0;
    }
    
    private void Update() {
        UpdateStats();
        if (GameIsOver) return;
        if (Lives <= 0) GameOver();
        if (Input.GetKeyDown(KeyCode.Escape) && levelWonUI && gameOverUI) TogglePauseMenu();
    }
    
    // ends game and enables game over screen
    private void GameOver() {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }
    
    // ends game and enables level won screen
    public void WinLevel() {
        GameIsOver = true;
        levelWonUI.SetActive(true);
    }
    
    // sets the timescale to 0 = pause or 1 = normal time
    public void TogglePauseMenu() {
        pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);
        Time.timeScale = pauseMenuUI.activeSelf ? 0f : 1f;
    }
    
    // back to main menu
    public void BackToMainMenu() {
        TogglePauseMenu();
        sceneController.MainMenu();
    }
    
    // restart the level
    public void RestartLevel() {
        TogglePauseMenu();
        sceneController.RestartGame();
    }

    // updating ui text
    private void UpdateStats() {
        livesText.text = Lives.ToString();
        wavesText.text = Waves + " | " + waveAmount;
        moneyText.text = Money.ToString();
    }
    
}
