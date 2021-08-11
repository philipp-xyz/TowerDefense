using TMPro;
using UnityEngine;
using UnityEngine.Serialization;


public class Shop : MonoBehaviour {
    
    private TurretBuildManager turretBuildManager;
    private TurretStats stats;

    private TMP_Text costText;

    [Header("Lives")] 
    
    [Tooltip("The cost of an additional live.")]
    [SerializeField] private int liveCost;

    [Tooltip("The amount of lives which will be added.")]
    [SerializeField] private int liveAmount;
    
    [FormerlySerializedAs("cubeTurret")]
    [Header("Turrets")]

    [SerializeField] private TurretStats minigunTurret;
    [SerializeField] private TurretStats cannonTurret; 
    [SerializeField] private TurretStats rocketLauncherTurret; 
    [SerializeField] private TurretStats laserTower; 
    [SerializeField] private TurretStats slowTower;
    
    private void Start() {
        turretBuildManager = TurretBuildManager.instance;
    }

    // buy more lives
    public void BuyLives() {
        if (GameManager.Money < liveCost) return;
        GameManager.Money -= liveCost;
        GameManager.Lives += liveAmount;
    }
    
    // hmm
    public void CheatMoney() {
        GameManager.Money += 200;
    }

    // select different turrets
    public void SelectCubeTurret() { turretBuildManager.SelectTurretToBuild(minigunTurret); }

    public void SelectCannon() { turretBuildManager.SelectTurretToBuild(cannonTurret); }

    public void SelectMissileLauncher() { turretBuildManager.SelectTurretToBuild(rocketLauncherTurret); }

    public void SelectLaserTower() { turretBuildManager.SelectTurretToBuild(laserTower); }
    
    public void SelectSlowTower() { turretBuildManager.SelectTurretToBuild(slowTower); }
    
    
}
