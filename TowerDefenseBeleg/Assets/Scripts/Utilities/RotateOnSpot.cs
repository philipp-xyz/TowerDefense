using UnityEngine;

public class RotateOnSpot : MonoBehaviour {
    
    // making a an object rotate on spot

    [SerializeField] private float rotationSpeed = 10f;
    
    private void Update() {
        transform.Rotate(new Vector3(0f, 0f, rotationSpeed) * Time.deltaTime);
    }
    
}
