using UnityEngine;

public class PathController : MonoBehaviour {

    private LineRenderer _pathRenderer;

    private Transform points;

    private void Awake() { _pathRenderer = GetComponent<LineRenderer>(); }

    // Sets up the line for the line renderer
    public void SetUpLine(Transform[] points) { _pathRenderer.positionCount = points.Length; }

    // Updates the position of the points and sets up line
    private void Update() {
        for (int i = 0; i < PathRenderer.points.Length; i++) {
            _pathRenderer.SetPosition(i, PathRenderer.points[i].position);
            SetUpLine(PathRenderer.points);
        }
    }
    
}
