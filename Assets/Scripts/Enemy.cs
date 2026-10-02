using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Waypoint firstWaypoint;

    [SerializeField] float speed = 1f;

    [SerializeField] float maxHealth = 10f;

    [SerializeField] GameObject healthDisplay;

    [SerializeField] int coinValue = 10;

    [SerializeField] int scoreValue = 100;

    [SerializeField] bool isflyer = false;

    float crntHealth;

    Waypoint crntwaypoint;

    Animator animator;

    private LevelManager levelManager;

    
    
    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();

        crntwaypoint = firstWaypoint;


        if (!isflyer)
        {
            animator = GetComponent<Animator>();

            animator.speed = speed * 0.85f;
        }
        

        crntHealth = maxHealth;


        healthDisplay.GetComponent<Slider>().value = crntHealth / maxHealth;
        

    }

    

    // Update is called once per frame
    void Update()
    {
        MoveToWaypoint();

        
        

    }

    private void MoveToWaypoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, crntwaypoint.transform.position, speed * Time.deltaTime);

        if (transform.position == crntwaypoint.transform.position && crntwaypoint.GetComponent<Waypoint>().getNextWaypoint().Length != 0)
        {
            int nextChoice = Random.Range(0, (crntwaypoint.GetComponent<Waypoint>().getNextWaypoint().Length));


            crntwaypoint = crntwaypoint.GetComponent<Waypoint>().getNextWaypoint()[nextChoice];
            //Damage(4f);
            
        }
    }

    public void Damage(float damage)
    {

        // take damage value from enemy.
        crntHealth = crntHealth - damage;

        // update health display.
        healthDisplay.GetComponent<Slider>().value = crntHealth / maxHealth;


        // if enemy has 0 health remove it.
        if (crntHealth <= 0)
        {
            GameObject.Destroy(gameObject);
            levelManager.AddBalance(coinValue);
            levelManager.AddScore(scoreValue);
        }

    }

    public void SetEnemyWaypoint(Waypoint waypoint)
    {
        firstWaypoint = waypoint;
    }

    public void AddEnemyHealth(float newHealth)
    {
        maxHealth += newHealth;

    }

    public bool Isflyer()
    {
        return isflyer;
    }

    

}
