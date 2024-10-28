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
        public GameObject jeepPlayer;

        private BoxCollider2D boxCollider;

        Vector2 standingColliderSize = new Vector2(0.85f, 1.3f);
        Vector2 standingColliderOffset = new Vector2(-0.15f, 0.05f);

        Vector2 crouchingColliderSize = new Vector2(1.15f, 0.75f);
        Vector2 crouchingColliderOffset = new Vector2(-0.1f, -0.15f);

        Vector2 deathColliderSize = new Vector2(1.15f, 0.5f);
        Vector2 deathColliderOffset = new Vector2(0f, -0.05f);

        Vector2 jeepColliderSize = new Vector2(1.5f, 1.5f);
        Vector2 jeepColliderOffset = new Vector2(0f, 0.32f);

        //private bool isDashing = false;
        private bool isPlayerDamageOn = true;

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
                if (isPlayerDamageOn)
                {
                    LevelManager.Instance.KillCharacter(this.GetComponent<PlayableCharacter>());
                    Death();
                    //MF.SoundManager.Instance.playSFX(MF.SoundClips.Kill);
                    //LevelManager.Instance.CallAllCharactersDead();
                }
            }

            if (collision.gameObject.tag == "Collectable")
            {
                LevelManager.Instance.CollectCoin();
                Destroy(collision.gameObject.transform.parent.gameObject);
                //MF.SoundManager.Instance.playSFX(MF.SoundClips.Fire);
            }

            if(collision.gameObject.tag == "Jeep")
            {
                LevelManager.Instance.isJeepCollected = true;
                Destroy(collision.gameObject.transform.parent.gameObject);
                SetJeep();
                StartCoroutine(StartJeepTimer());
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
            if (!LevelManager.Instance.isJeepCollected)
            {
                anim.SetBool("dash", true);
                boxCollider.size = crouchingColliderSize;
                boxCollider.offset = crouchingColliderOffset;
            }
        }

        public void Stand()
        {
            Debug.Log("Stand Called");
            if (!LevelManager.Instance.isJeepCollected)
            {
                anim.SetBool("dash", false);
                boxCollider.size = standingColliderSize;
                boxCollider.offset = standingColliderOffset;
            }
        }

        public void Jump()
        {
            GetComponent<Jumper>().Jump();
        }

        public void SetJeep()
        {
            jeepPlayer.SetActive(true);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            boxCollider.size = jeepColliderSize;
            boxCollider.offset = jeepColliderOffset;
        }

        public void RemoveJeep()
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            jeepPlayer.SetActive(false);
            Stand();
            Invoke(nameof(ResetPlayerDamage), 2);
        }

        IEnumerator StartJeepTimer()
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(8, 16));
            isPlayerDamageOn = false;
            LevelManager.Instance.isJeepCollected = false;
            RemoveJeep();
        }

        public void ResetPlayerDamage()
        {
            isPlayerDamageOn = true;
        }
    }
}