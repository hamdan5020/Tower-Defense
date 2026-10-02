using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit : MonoBehaviour
{

    [SerializeField] float damage = 1f;
    [SerializeField] float fireRate = 1f;
    [SerializeField] float range = 2f;
    [SerializeField] int cost = 10;
    [SerializeField] Projectile projectileInst;
    [SerializeField] Sprite attackSprite;
    [SerializeField] Sprite idleSprite;


    

    private Coroutine shootRoutine;
    private bool ispreview = false;
    private bool isShooting = false;
    private GameObject crntTarget;

    private List<GameObject> targetsList;





    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<CircleCollider2D>().radius = range/2;
        gameObject.GetComponent<SpriteRenderer>().sprite = idleSprite;
        targetsList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCurrentTarget();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Enemy" && !ispreview)
        {
            targetsList.Add(collision.gameObject);
            UpdateCurrentTarget();
        }
        

        if (collision.tag == "Enemy" && !ispreview && !isShooting)
        {
            
            
            shootRoutine = StartCoroutine(Shoot());
            gameObject.GetComponent<SpriteRenderer>().sprite = attackSprite;
            isShooting = true;
            
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "Enemy" && !ispreview)
        {
            targetsList.Remove(collision.gameObject);
            UpdateCurrentTarget();
        }


        if (collision.tag == "Enemy" && !ispreview && isShooting)
        {
            
            StopCoroutine(shootRoutine);
            gameObject.GetComponent<SpriteRenderer>().sprite = idleSprite;
            isShooting = false;
        }
    }

    IEnumerator Shoot()
    {
        while (true) {

            UpdateCurrentTarget();
            Projectile projectile = Instantiate(projectileInst);
            projectile.transform.position = transform.position;
            projectile.transform.parent = transform;
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(fireRate);
            
        }
        
    }

    void UpdateCurrentTarget()
    {
        if (targetsList.Count == 0)
        {
            crntTarget = null;
            return;
        }

        float closestDist = Mathf.Infinity;
        GameObject closestEnm = null;

        foreach (GameObject target in targetsList)
        {
            float dist = Vector2.Distance(transform.position, target.transform.position);

            if (dist < closestDist)
            {
                closestDist = dist;
                closestEnm = target;
            }
        }

        crntTarget = closestEnm;


    }

    public float GetRange()
    {
        return range;
    }

    public GameObject getTarget() {
        return crntTarget;
    }

    public float getDamage()
    {
        return damage;
    }

    public void SetIspreview(bool isprev)
    {
        ispreview = isprev;
    }

    public int GetCost()
    {
        return cost;
    }
}
