using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    
    [Header("Effects")] 
        
    [Tooltip("The death effect of the enemy.")]
    [SerializeField] private GameObject deathEffect;
    
    [Tooltip("The time after which the effect will be destroyed.")]
    [SerializeField] private float destroyEffectTime = 3f;
    
    [Header("Attributes")]

    [Tooltip("The amount of health the enemy has at start.")]
    [SerializeField] private float startHealth = 100;
    public float StartHealth => startHealth;
    private float health;
    
    [Tooltip("The start speed of the enemy.")]
    [SerializeField] private float startSpeed = 10f;
    public float StartSpeed => startSpeed;
    public float EnemySpeed { get => speed; set => speed = value; }
    private float speed;
    
    [Header("Health Bar")] 
    
    [Tooltip("The image of the health bar.")]
    [SerializeField] private Image healthBar;
    
    [Header("Money")]
    
    [Tooltip("The amount of money the enemy is worth.")]
    [SerializeField] private int enemyValue = 10;

    private bool isDead;

    private void Start() {
        speed = startSpeed;
        health = startHealth;
    }
    
    // reduces health of the enemy
    public void TakeDamage(float amount) {
        health -= amount;
        // sets fill amount of health bar
        healthBar.fillAmount = health / startHealth;
        
        if (health <= 0 && !isDead) Die();
    }
    
    // slows the speed of the enemy
    public void Slow(float amount) {
        speed = startSpeed * (1f - amount);
    }
    
    // destroys the enemy and increases the money by the amount the enemy is worth
    private void Die() {
        isDead = true;
        
        GameManager.Money += enemyValue;
        
        // instantiates particle effect at position of the enemy
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, destroyEffectTime);
        
        EnemySpawner.EnemiesAlive--;

        Destroy(gameObject);
    }
    

}
