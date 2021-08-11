using UnityEngine;

public class TurretBuildManager : MonoBehaviour {
    
    public static TurretBuildManager instance;

    [Header("UI")] 
    
    [Tooltip("The reference to the turret UI.")]
    [SerializeField] private TurretInterface turretInterface;

    [Header("Effects")]
    
    [Tooltip("The time after which the build effect will be destroyed.")]
    [SerializeField] private float effectLifetime = 2f;
    public float EffectLifetime => effectLifetime;
    
    [Tooltip("The effect which will be displayed when a turret is build.")]
    [SerializeField] private GameObject buildEffect;
    public GameObject BuildEffect => buildEffect;

    [Tooltip("The effect which will be displayed when a turret is sold.")] 
    [SerializeField] private GameObject sellEffect;
    public GameObject SellEffect => sellEffect;
    
    public bool CanBuild => turretToBuild != null;
    public bool HasMoney => GameManager.Money >= turretToBuild.cost;

    private TurretStats turretToBuild;
    public PlaceArea selectedPlaceArea;
    
    private void Awake() {
        // check if there are more than one turret build manager in the scene
        if (instance != null) Debug.LogError("More than one Turret Build Manager in scene!");
        instance = this;
    }

    // selecting a place area
    public void SelectPlaceArea(PlaceArea placeArea) {
        if (selectedPlaceArea == placeArea) {
            DeselectPlaceArea();
            return;
        }
        selectedPlaceArea = placeArea;
        turretInterface.ShowTurretInterface(selectedPlaceArea);
        turretToBuild = null;
    }
    
    // deselects the place area and hides turret interface
    public void DeselectPlaceArea() {
        selectedPlaceArea = null;
        turretInterface.HideTurretInterface();
    }
    
    // select the turret which will be build
    public void SelectTurretToBuild(TurretStats turret) {
        turretToBuild = turret;
        DeselectPlaceArea();
    }
    
    // gets the turret to build from the turret stats
    public TurretStats GetTurretToBuild() {
        return turretToBuild;
    }
     
}
