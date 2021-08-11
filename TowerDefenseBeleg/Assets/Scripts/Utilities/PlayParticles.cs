using UnityEngine;

public class PlayParticles : MonoBehaviour {
    
    // playing particles on main menu

    [SerializeField] private ParticleSystem particles;

    private void Start() {
        InvokeRepeating(nameof(PlayParticle), 0f, 1.5f);
    }

    private void PlayParticle() {
        particles.Play();
    }
    
    public void StopParticles() {
        particles.Stop();
    }

}
