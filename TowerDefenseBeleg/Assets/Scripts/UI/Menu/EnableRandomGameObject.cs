using UnityEngine;
using Random = UnityEngine.Random;

public class EnableRandomGameObject : MonoBehaviour {

    [SerializeField] private GameObject[] turrets;

    // enable a random turret on main menu screen each time it is opened
    private void Start() {
        foreach (GameObject turret in turrets) {
            turret.SetActive(false);
        }
        turrets[Random.Range(0, turrets.Length)].SetActive(true);
    }
    
}
