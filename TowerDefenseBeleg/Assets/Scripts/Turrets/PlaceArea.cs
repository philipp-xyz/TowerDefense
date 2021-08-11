using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceArea : MonoBehaviour {
    
    private TurretBuildManager turretBuildManager;
    [HideInInspector] public TurretStats turretStats;
    
    [Header("Color Settings")]

    [Tooltip("The color the place area changes to on mouse hover.")]
    [SerializeField] private Color hoverColor;
    
    [Tooltip("The color the place area changes to on mouse hover if can't build on this place area.")]
    [SerializeField] private Color cantBuildColor;
    
    [Tooltip("The color the place are changes to on mouse hover if you don't have enough money.")]
    [SerializeField] private Color notEnoughMoneyColor;

    [Header("Attributes")]
    
    [Tooltip("The offset of the turret position on the place area.")]
    [SerializeField] private Vector3 positionOffset;
    
    private GameObject turret;
    public GameObject Turret { get => turret; set => turret = value; }
    
    public bool movingPlaceArea;
    
    private Renderer rend;
    private Color startColor;
    
    private void Start() {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        turretBuildManager = TurretBuildManager.instance;
    }
    
    // gets the build position
    public Vector3 GetBuildPosition() {
        return transform.position + positionOffset;
    }
    
    // building turret
    private void BuildTurret(TurretStats tstats) {
        if (GameManager.Money < tstats.cost) return;

        // decrease money by turret cost
        GameManager.Money -= tstats.cost;
        
        // instantiate turret prefab at build position
        GameObject _turret = Instantiate(tstats.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        // parent turret to place area
        turret.transform.parent = transform;
        turretStats = tstats;

        // display effect when turret is being build
        GameObject effect = Instantiate(turretBuildManager.BuildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, turretBuildManager.EffectLifetime);
    }
    
   
    // selling a turret
    public void SellTurret() {
        // increases money by sell amount
        GameManager.Money += turretStats.GetSellAmount();
        
        // display effect when turret is being sold
        GameObject effect = Instantiate(turretBuildManager.SellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, turretBuildManager.EffectLifetime);
        
        Destroy(turret);
        
        turretStats = null;
    }
    
    private void OnMouseEnter() {
        // checks if pointer is over UI element
        if (EventSystem.current.IsPointerOverGameObject()) return;
        
        if (!turretBuildManager.CanBuild) return;

        // changes color to cant build color
        if (turret != null) {
            rend.material.color = cantBuildColor;
            return;
        }

        // changes color to hover color and not enough money color
        rend.material.color = turretBuildManager.HasMoney ? hoverColor : notEnoughMoneyColor;
    }

    // Instantiate turret on mouse click at selected place area
    private void OnMouseDown() {
        // checks if mouse is over ui element
        if (EventSystem.current.IsPointerOverGameObject()) return;
        
        // deselecting place area
        TurretBuildManager.instance.DeselectPlaceArea();
        
        // select build area
        if (turret != null) {
            turretBuildManager.SelectPlaceArea(this);    
            return;
        }
        
        if (!turretBuildManager.CanBuild) return;
        
        BuildTurret(turretBuildManager.GetTurretToBuild());
    }
    
    // changes the color of the place area back to the original color
    private void OnMouseExit() {
        rend.material.color = startColor;
    }

}
