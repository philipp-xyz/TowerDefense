using TMPro;
using UnityEngine;


public class TurretInterface : MonoBehaviour {

    private PlaceArea target;

    [Header("UI")] 
    
    [Tooltip("The canvas which will be disabled and enabled on clicking a turret.")]
    [SerializeField] private GameObject interfaceCanvas;
    
    [Tooltip("The text element of the turret name.")]
    [SerializeField] private TMP_Text turretNameText;
    
    [Tooltip("The text element of the turret description.")]
    [SerializeField] private TMP_Text turretDescriptionText;
    
    [Header("Sell")] 
    
    [Tooltip("The text element for the sell amount.")]
    [SerializeField] private TMP_Text sellValue;

    [Header("Range Indicator")] 
    
    [SerializeField] private GameObject minigunTurretRange;
    [SerializeField] private GameObject cannonRange;
    [SerializeField] private GameObject rocketLauncherRange;
    [SerializeField] private GameObject laserTurretRange;
    [SerializeField] private GameObject slowTurretRange;
    
    private void Start() {
        interfaceCanvas.SetActive(false);
    }

    // show ui and update text
    public void ShowTurretInterface(PlaceArea _target) {
        target = _target;
        sellValue.text = "$" + target.turretStats.GetSellAmount();
        turretNameText.text = target.turretStats.GetTurretName();
        turretDescriptionText.text = target.turretStats.GetTurretDescription();
        if (!target.movingPlaceArea) ShowRange(_target);
        ShowInterface();
    }

    // show range indicator for turrets
    private void ShowRange(PlaceArea _target) {
        target = _target;
        transform.position = target.GetBuildPosition();

        // activates range indicator for turrets
        switch (target.turretStats.turretName) {
            case "Minigun Turret":
                minigunTurretRange.SetActive(true);
                break;
            case "Cannon Turret":
                cannonRange.SetActive(true);
                break;
            case "Rocket Launcher":
                rocketLauncherRange.SetActive(true);
                break;
            case "Laser Tower":
                laserTurretRange.SetActive(true);
                break;
            case "Slow Tower":
                slowTurretRange.SetActive(true);
                break;
        }
        
    }

    // enable canvas of turret interface
    private void ShowInterface() {
        interfaceCanvas.SetActive(true);
    }
    
    // hide canvas of turret interface and range indicator
    public void HideTurretInterface() {
        interfaceCanvas.SetActive(false);
        HideRange();
    }

    // disables all range indicators
    private void HideRange() {
        minigunTurretRange.SetActive(false);
        cannonRange.SetActive(false);
        rocketLauncherRange.SetActive(false);
        laserTurretRange.SetActive(false);
        slowTurretRange.SetActive(false);
    }
    
    // selling a turret
    public void Sell() {
        target.SellTurret();
        TurretBuildManager.instance.DeselectPlaceArea();
    }

}

        
