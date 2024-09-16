using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.InfiniteRunnerEngine
{
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

                LevelManager.Instance.KillEnemy();
    
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                collision.gameObject.GetComponent<Animator>().SetBool("isDead", true);
                collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                Destroy(collision.gameObject.transform.parent.gameObject, 1f);
                Destroy(this.gameObject);
            }
        }
    }
}
