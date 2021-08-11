using UnityEngine;

public class CameraController : MonoBehaviour {

    public static bool BlockCameraMovement;
    
    [Header("Pan Settings")]
    
    [Tooltip("The pan speed of tha camera.")]
    [SerializeField] private float panSpeed = 30f;

    [Header("Scroll Settings")]
    
    [Tooltip("The scroll speed of the camera.")]
    [SerializeField] private float scrollSpeed = 5f;

    [Tooltip("Used to clamp the minimum y value for the scroll wheel.")]
    [SerializeField] private float minY = 10f;
    
    [Tooltip("Used to clamp the maximum y value for the scroll wheel.")]
    [SerializeField] private float maxY = 100f;

    private Vector3 startPosition;

    private void Start() {
        startPosition = transform.position;
        BlockCameraMovement = false;
    }

    private void Update() {
        // reset camera position if game is over
        if (GameManager.GameIsOver) {
            ResetCameraPosition();
            // disable script
            enabled = false;
        }
        
        MoveCamera();
        ScrollCamera();
    }
    
    // Moves the camera in the different directions
    private void MoveCamera() {
        // blocks camera movement
        if (Input.GetKeyDown(KeyCode.T)) BlockCameraMovement = !BlockCameraMovement;
        // resets camera position
        if (Input.GetKeyDown(KeyCode.Z)) ResetCameraPosition();
        
        if (BlockCameraMovement) return;
        // key inputs
        if (Input.GetKey(KeyCode.W)) transform.Translate(Vector3.forward * (panSpeed * Time.deltaTime), Space.World);
        if (Input.GetKey(KeyCode.S)) transform.Translate(Vector3.back * (panSpeed * Time.deltaTime), Space.World);
        if (Input.GetKey(KeyCode.D)) transform.Translate(Vector3.right * (panSpeed * Time.deltaTime), Space.World);
        if (Input.GetKey(KeyCode.A)) transform.Translate(Vector3.left * (panSpeed * Time.deltaTime), Space.World);
    }
    
    // Scrolling in and out with the scroll wheel
    private void ScrollCamera() {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 position = transform.position;
        position.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        // clamp between min and max scroll out
        position.y = Mathf.Clamp(position.y, minY, maxY);
        transform.position = position;
    }
    
    // resets the camera position
    private void ResetCameraPosition() {
        transform.position = startPosition;
    }
    
}
