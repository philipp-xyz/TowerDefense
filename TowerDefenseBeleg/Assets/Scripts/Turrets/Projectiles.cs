using UnityEngine;

public class Projectiles : MonoBehaviour {
    
    private Transform target;

    [Header("Setup Fields")]
    
    [Tooltip("The prefab of the particle effect applied on impact.")]
    [SerializeField] private GameObject impactEffect;

    [Tooltip("The time after which the effect will be destroyed.")]
    [SerializeField] private float destroyEffectTime = 3f;

    [Header("Attributes")]
    
    [Tooltip("The speed at which the projectile will be fired.")]
    [SerializeField] private float speed = 50f;
    
    [Tooltip("The explosion radius of the projectile.")]
    [SerializeField] private float explosionRadius;

    [Tooltip("The damage of the projectile.")] 
    public int projectileDamage = 10;
    
    // sets the target which the projectile will follow
    public void FollowTarget(Transform _target) {
        target = _target;
    }
    
    private void Update() {
        // destroy target if null
        if (target == null) {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // hit the target
        if (direction.magnitude <= distanceThisFrame) {
            HitTarget();
            return;
        } 
        
        // move projectile
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        // projectile look at target
        transform.LookAt(target);

    }
    
    // hit the target
    private void HitTarget() {
        GameObject effectInstance = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, destroyEffectTime);

        // make an explosion
        if (explosionRadius > 0f) Explode();
        else Damage(target);
        
        Destroy(gameObject);
    }
    
    // explode projectiles
    private void Explode() {
        // hit multiple enemies
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var collider in colliders) {
            if (collider.CompareTag("Enemy")) Damage(collider.transform);
        }
    }

    // damage enemy
    private void Damage(Transform enemy) {
        var e = enemy.GetComponent<Enemy>();
        if (e != null) {
            e.TakeDamage(projectileDamage);
        }
    }

}