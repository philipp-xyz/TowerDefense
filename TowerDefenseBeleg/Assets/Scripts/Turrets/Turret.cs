using UnityEngine;

public class Turret : MonoBehaviour {
    
    private Transform target;
    private Enemy enemy;

    [Header("Setup Fields")]

    [Tooltip("The tag of the enemy.")]
    [SerializeField] private string enemyTag = "Enemy";

    [Tooltip("The part of the turret which will be rotated towards the enemy.")]
    [SerializeField] private Transform partToRotate;
    
    [Tooltip("The position where the projectile will be fired from.")]
    [SerializeField] private Transform firePoint;

    [Tooltip("The speed the turret takes to turns to the next enemy.")]
    [SerializeField] private float turnSpeed = 10f;

    [Header("Turret Range")] 
    
    /*[Tooltip("The game object of the range indicator")]
    [SerializeField] private GameObject rangeIndicator;*/
    
    [Tooltip("The range of the turret.")]
    [SerializeField] private float range = 15f;
    public float TurretRange => range;
    
    [Header("Use Projectile")]

    [Tooltip("The prefab of the projectile the turret will shoot.")]
    [SerializeField] private GameObject projectilePrefab;

    [Tooltip("The fire rate of the turret.")]
    [SerializeField] private float fireRate = 1f;
    public float FireRate => fireRate;

    private float fireCountdown;
    
    [Header("Slow Tower")]
    
    [Tooltip("Is this turret an AOE slow tower?")]
    [SerializeField] private bool turretIsSlowTower;

    [Tooltip("The effect of the slow tower.")]
    [SerializeField] private ParticleSystem slowTowerEffect;
    
    [Tooltip("The damage over time the slow tower will do.")] 
    [SerializeField] private int slowTowerDamageOverTime = 5;
    
    [Header("Slow")]

    [Tooltip("Apply slow to the enemy.")]
    [SerializeField] private bool applySlow;
    
    [Tooltip("The amount the enemy will be slowed (in percent).")]
    [Range(0, 1)]
    [SerializeField] private float slowAmount;

    [Header("Laser")] 
    
    [Tooltip("The ability to use the almighty laser.")]
    [SerializeField] private bool useLaser;

    [Tooltip("The damage over time the laser will do.")] 
    [SerializeField] private int laserDamageOverTime = 10;

    [Tooltip("The amount the enemy will be slowed (in percent).")]
    [Range(0, 1)]
    [SerializeField] private float laserSlowAmount;

    [Tooltip("The line renderer for the laser.")]
    [SerializeField] private LineRenderer lineRenderer;

    [Tooltip("The particle effect which will be displayed when the laser hits something.")]
    [SerializeField] private ParticleSystem laserImpactParticles;

    [Tooltip("The particle effect which will be displayed at the barrel.")]
    [SerializeField] private ParticleSystem laserParticles;

    [Tooltip("The light which will be displayed on impact.")]
    [SerializeField] private Light impactLight;
    
    private void Start() {
        // invokes update target every 0.5 seconds
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
    }
    
    private void Update() {
        if (target == null) {
            if (!useLaser) return;
            if (!lineRenderer.enabled) return;
            DisableLaserStuff();
            return;
        }

        LockTarget();
        // slow enemy
        if (applySlow) SlowEnemy();
        if (turretIsSlowTower) SlowTurret();
        // shoot laser
        if (useLaser) Laser();
        else {
            if (fireCountdown <= 0f) {
                // shoot projectile
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
    }
    
    // lock on target
    private void LockTarget() {
        Vector3 direction = target.position - transform.position;
        // look towards direction
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        // rotates the turret head towards look rotation
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    
    // shoot projectiles
    private void Shoot() {
        GameObject projectileObject = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectiles projectile = projectileObject.GetComponent<Projectiles>();
        // follow target
        if (projectile != null) projectile.FollowTarget(target);
    }
    
    // the laser
    private void Laser() {
        enemy.TakeDamage(laserDamageOverTime * Time.deltaTime);
        enemy.Slow(laserSlowAmount);

        // line renderer and particles
        if (!lineRenderer.enabled) {
            lineRenderer.enabled = true;
            laserImpactParticles.Play();
            laserParticles.Play();
            impactLight.enabled = true;
        }
        
        // positions for the line renderer
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        // laser particles
        Vector3 direction = firePoint.position - target.position;
        laserImpactParticles.transform.position = target.position + direction.normalized * 0.5f ;
        laserImpactParticles.transform.rotation = Quaternion.LookRotation(direction);
    }

    // laser disable line renderer and particles
    private void DisableLaserStuff() {
        lineRenderer.enabled = false;
        laserImpactParticles.Stop();
        laserParticles.Stop();
        impactLight.enabled = false;
    }
    
    // slow a single enemy
    private void SlowEnemy() {
        enemy.Slow(slowAmount);
    }

    // slow turret - slowing multiple enemies
    private void SlowTurret() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (var e in colliders) {
            if (e.CompareTag("Enemy")) SlowAoe(e.transform);
        }
    }

    // aoe slow and apply damage over time
    private void SlowAoe(Transform enemy) {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null) {
            e.Slow(slowAmount);
            e.TakeDamage(slowTowerDamageOverTime * Time.deltaTime);
        }
    }
    
    // start stop slow effect particles
    private void StartSlowEffect() { slowTowerEffect.Play(); }
    private void StopSlowEffect() { slowTowerEffect.Stop(); }
    
    // updating target and calculate closest enemy
    private void UpdateTarget() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        
        // calculate distance to closest enemy
        foreach (GameObject enemy in enemies) {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance) {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        // target enemy in range
        if (nearestEnemy != null && shortestDistance <= range) {
            target = nearestEnemy.transform;
            enemy = nearestEnemy.GetComponent<Enemy>();
            if (turretIsSlowTower) {
                StartSlowEffect();
            }
        } else {
            target = null;
            StopSlowEffect();
        }
    }
    
    /*private void OnMouseDown() {
        rangeIndicator.SetActive(!rangeIndicator.activeSelf);
        TurretBuildManager.instance.OpenUi(TurretBuildManager.instance.selectedPlaceArea);
    }*/

    /*public void ShowTurretRange() {
        rangeIndicator.SetActive(!rangeIndicator.activeSelf);
    }*/

}
