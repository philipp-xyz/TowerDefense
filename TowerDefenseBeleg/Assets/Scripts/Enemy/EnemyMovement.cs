using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour {
    
    private Transform target;
    private Enemy enemy;
    
    private int waypointIndex;
    
    private void Start() {
        enemy = GetComponent<Enemy>();
        // sets target to waypoint
        target = Waypoints.waypoints[0];
    }
    
    private void Update() {
        Vector3 direction = target.position - transform.position;
        // moves the enemy towards the direction relative to world space
        transform.Translate(direction.normalized * (enemy.EnemySpeed * Time.deltaTime), Space.World);

        // if the distance is between enemy and waypoint is less than 0.1 go to next waypoint
        if (Vector3.Distance(transform.position, target.position) <= 0.1f) GetNextWaypoint();
        // reset speed after slow
        enemy.EnemySpeed = enemy.StartSpeed; 
    }
    
    // get next waypoint
    private void GetNextWaypoint() {
        if (waypointIndex >= Waypoints.waypoints.Length - 1) {
            EndPath();
            return;
        }
        
        waypointIndex++;
        // sets target to new waypoint index
        target = Waypoints.waypoints[waypointIndex];
    }
    
    // Destroys enemy at the end of the path and reduces player lives and enemies alive
    private void EndPath() {
        GameManager.Lives--;
        EnemySpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
    
}
