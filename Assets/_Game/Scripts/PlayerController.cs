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

        private void Start()
        {
            boxCollider = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            if(Input.GetKey(KeyCode.Z))
            {
                anim.SetBool("dash", true);
                Dash();
            }
            else
            {
                anim.SetBool("dash", false);
                Stand();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                Shoot();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "HitCollider" || collision.gameObject.tag == "Enemy")
            {
                Debug.Log("Collider = " + collision.gameObject.name);
                LevelManager.Instance.KillCharacter(this.GetComponent<PlayableCharacter>());
                //LevelManager.Instance.CallAllCharactersDead();
            }
        }

        public void Shoot()
        {
            Instantiate(bullet, firePosition.position, Quaternion.identity);
        }

        void Dash()
        {
            boxCollider.size = crouchingColliderSize;
            boxCollider.offset = crouchingColliderOffset;
        }

        void Stand()
        {
            boxCollider.size = standingColliderSize;
            boxCollider.offset = standingColliderOffset;
        }
    }
}