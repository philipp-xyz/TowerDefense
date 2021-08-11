using UnityEngine;

public class Waypoints : MonoBehaviour {
    
    public static Transform[] waypoints;

    private void Awake() {
        // gets amount of waypoints from the child objects
        waypoints = new Transform[transform.childCount];
        
        for (int i = 0; i < waypoints.Length; i++) {
            waypoints[i] = transform.GetChild(i);
        }
        
    }
    
}
