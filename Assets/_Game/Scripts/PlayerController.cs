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
        Vector2 standingColliderOffset = new Vector2(-0.15f, 0.05f);

        Vector2 crouchingColliderSize = new Vector2(1.15f, 0.75f);
        Vector2 crouchingColliderOffset = new Vector2(-0.1f, -0.15f);

        Vector2 deathColliderSize = new Vector2(1.15f, 0.5f);
        Vector2 deathColliderOffset = new Vector2(0f, -0.05f);

        //private bool isDashing = false;

        private void Start()
        {
            boxCollider = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            //if (Input.GetKey(KeyCode.Z))
            //{
            //    Dash();
            //}
            //else
            //{
            //    Stand();
            //}

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
                Death();
                MF.SoundManager.Instance.playSFX(MF.SoundClips.Kill);
                //LevelManager.Instance.CallAllCharactersDead();
            }

            if (collision.gameObject.tag == "Collectable")
            {
                LevelManager.Instance.CollectCoin();
                Destroy(collision.gameObject.transform.parent.gameObject);
                MF.SoundManager.Instance.playSFX(MF.SoundClips.Fire);
            }
        }

        public void Death()
        {
            anim.SetTrigger("isDead");
            boxCollider.size = deathColliderSize;
            boxCollider.offset = deathColliderOffset;
        }

        public void Shoot()
        {
            Instantiate(bullet, firePosition.position, Quaternion.identity);
        }

        public void Dash()
        {
            Debug.Log("Dash Called");
            anim.SetBool("dash", true);
            boxCollider.size = crouchingColliderSize;
            boxCollider.offset = crouchingColliderOffset;
        }

        public void Stand()
        {
            Debug.Log("Stand Called");
            anim.SetBool("dash", false);
            boxCollider.size = standingColliderSize;
            boxCollider.offset = standingColliderOffset;
        }

        public void Jump()
        {
            GetComponent<Jumper>().Jump();
        }
    }
}