using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.InfiniteRunnerEngine
{
    public class PlayerController : MonoBehaviour
    {
        public Animator anim;
        public Transform firePosition;
        public GameObject bullet;

        private BoxCollider2D boxCollider;

        Vector2 standingColliderSize = new Vector2(0.85f, 1.3f);
        Vector2 standingColliderOffset = new Vector2(-0.15f, 0f);
        
        Vector2 crouchingColliderSize = new Vector2(1.15f, 0.75f);
        Vector2 crouchingColliderOffset = new Vector2(-0.1f, -0.15f);

        //private bool isDashing = false;

        private void Start()
        {
            boxCollider = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Z))
            {
                Dash();
            }
            else
            {
                Stand();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                Shoot();
            }

            if (Input.GetKey(KeyCode.Space))
            {
                LevelManager.Instance.PlayerJump();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "HitCollider" || collision.gameObject.tag == "Enemy")
            {
                LevelManager.Instance.KillCharacter(this.GetComponent<PlayableCharacter>());
                //LevelManager.Instance.CallAllCharactersDead();
            }
            
            if (collision.gameObject.tag == "Collectable")
            {
                LevelManager.Instance.CollectCoin();
                Destroy(collision.gameObject.transform.parent.gameObject);
            }
        }

        public void Shoot()
        {
            Instantiate(bullet, firePosition.position, Quaternion.identity);
        }

        public void Dash()
        {
            anim.SetBool("dash", true);
            boxCollider.size = crouchingColliderSize;
            boxCollider.offset = crouchingColliderOffset;
        }

        public void Stand()
        {
            anim.SetBool("dash", false);
            boxCollider.size = standingColliderSize;
            boxCollider.offset = standingColliderOffset;
        }
    }
}