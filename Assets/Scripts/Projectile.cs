using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 10f;

    private GameObject target;
    private float damage;


    // Start is called before the first frame update
    void Start()
    {
        target = GetComponentInParent<Unit>().getTarget();
        damage = GetComponentInParent<Unit>().getDamage();

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        } else
        {
            GameObject.Destroy(gameObject);
        }

    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") 
        {
            collision.gameObject.GetComponent<Enemy>().Damage(damage);
            GameObject.Destroy(gameObject);
        }
    }
}
