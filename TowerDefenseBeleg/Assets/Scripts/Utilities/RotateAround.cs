using System;
using UnityEngine;

public class RotateAround : MonoBehaviour {

    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject center;

    private void Update() {
        transform.RotateAround(center.transform.position, new Vector3(0, -1, 0), rotationSpeed * Time.deltaTime);
    }
    
}
