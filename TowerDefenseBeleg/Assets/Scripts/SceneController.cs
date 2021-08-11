using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    [Header("Scenes")]
    
    public string mainMenu = "MainMenu";
    public string levelSelect = "LevelSelect";
    
    // close application
    public void QuitGame() { Application.Quit(); }
    
    public void RestartGame() {
        EnemySpawner.EnemiesAlive = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    // switch between different menus
    public void MainMenu() { SceneManager.LoadScene(mainMenu); }
    public void LevelSelect() { SceneManager.LoadScene(levelSelect); }

    // levels
    public void Level01() { SceneManager.LoadScene("Level01"); }
    public void Level02() { SceneManager.LoadScene("Level02"); }

}
