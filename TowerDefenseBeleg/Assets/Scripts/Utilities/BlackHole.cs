using UnityEngine;

public class BlackHole : MonoBehaviour {

    [SerializeField] private ParticleSystem particles;

    private void Start() {
        StartBlackHole();
    }

    // enable / disable particles of black hole
    public void StartBlackHole() { particles.Play(); }
    
    public void StopBlackHole() { particles.Stop(); }
    
}
