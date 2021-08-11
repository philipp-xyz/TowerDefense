using System;
using UnityEngine;

[Serializable]
public class TurretStats {
    
    // prefab of the turret
    public GameObject prefab;
    // the name of the turret;
    public string turretName;
    // build cost of the turret
    public int cost;
    // the description of the turret
    public string turretDescription;

    public int GetSellAmount() { return cost / 2; }
    
    public string GetTurretName() { return turretName; }
    
    public string GetTurretDescription() { return turretDescription; }

}
