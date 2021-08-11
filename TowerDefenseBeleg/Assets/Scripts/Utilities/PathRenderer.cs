using UnityEngine;

public class PathRenderer : MonoBehaviour {

    public static Transform[] points;
    private PathController path;

    private void Awake() {
        // gets amount of path points from child objects
        points = new Transform[transform.childCount];
        
        for (int i = 0; i < points.Length; i++) {
            points[i] = transform.GetChild(i);
        }
    }
    
}
