using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int speed = 1;
    public int lifeTime = 1;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Collider = " + collision.gameObject.name);
            collision.gameObject.GetComponent<Animator>().SetBool("isDead", true);
            //Destroy(collision.gameObject, 1f);
            Destroy(this);
        }
    }
}
