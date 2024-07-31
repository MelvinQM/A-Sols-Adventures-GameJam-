using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Boss
{
    public class FlameBoss : MonoBehaviour, IDamagable
    {
        public System.Action OnDeath;

        private enum States { Spawn, Fight, Dead}
        private States currentState = States.Spawn;

        [SerializeField] private Vector2 SpawnPos;
        private float fallHeight = 40;
        private float walkSpeed = 5f;

        // Not enough time to implement this
        //[SerializeField] private EnemyStage[] stages;

        [SerializeField] private Fireball fireballPrefab;
        [SerializeField] private Transform target;

        private float walkCooldown;
        private Vector2 walkCooldownVec = new Vector2(1, 5);

        private float shootCooldown;
        private Vector2 shootCooldownVec = new Vector2(0.5f, 2);

        [SerializeField] private Animator animator;
        [SerializeField] private Transform body;
        [SerializeField] private Transform head;

        private int health = 500;
        [SerializeField] private WorldHealthBar healthBar;

        private void Start()
        {
            walkCooldown = Random.Range(walkCooldownVec.x, walkCooldownVec.y);
            shootCooldown = Random.Range(shootCooldownVec.x, shootCooldownVec.y);
            healthBar.Setup(health);


            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        public void Spawn()
        {
            StartCoroutine(StartSpawn());
        }

        private IEnumerator StartSpawn() {
            currentState = States.Spawn;

            // TODO: Move the camera to that position, player may still walk around
            Vector2 spawn = SpawnPos;
            spawn.y += fallHeight;
            this.transform.position = spawn;
            
            DangerManager.Instance.SpawnDangerZone(20, SpawnPos, 2.5f);
            
            yield return new WaitForSeconds(2);
            this.transform.DOMoveY(SpawnPos.y, 0.3f).OnComplete(()=> 
            {
                CameraUtilities.Instance.ShakeCamera(3, 0.5f);
            });

            // TODO: Show boss falling out of the sky and landing with an explosion
            // TODO: Show wave annoucement "Flame Boss has arrived!"

            yield return new WaitForSeconds(2);
            
            // Start the boss fight
            StartFight();
        }

        private void StartFight()
        {
            currentState = States.Fight;
            WalkToNewPos();
        }
        
        private void WalkToNewPos()
        {
            DOTween.Kill(this.transform);
            
            Vector3 newPosition = TileManager.Instance.GetRandomPos();
            animator.Play("flame_spirit_walk");
            
            // Flip the sprite if needed
            Vector3 dir = newPosition - transform.position;
            float scaleX = dir.x > 0 ? -Mathf.Abs(body.localScale.x) : Mathf.Abs(body.localScale.x);
            body.localScale = new Vector3(scaleX, body.localScale.y, body.localScale.z);

            this.transform.DOMove(newPosition, walkSpeed).SetSpeedBased().SetEase(Ease.Linear).OnComplete(() =>
            {
                Debug.Log("Boss reached new position: " + newPosition);
                Idle();
            });
        }

        private void Idle()
        {
            animator.Play("flame_spirit_idle");
        }

        private void Shoot()
        {
            Fireball fireball = Instantiate(fireballPrefab, head.position, Quaternion.identity).GetComponent<Fireball>();
            fireball.UseAbility(new object[] { target });
        }

        private void Update()
        {
            if (currentState == States.Fight)
            {
                walkCooldown -= Time.deltaTime;
                shootCooldown -= Time.deltaTime;

                if (walkCooldown <= 0)
                {
                    WalkToNewPos();
                    walkCooldown = Random.Range(walkCooldownVec.x, walkCooldownVec.y);
                }

                if (shootCooldown <= 0)
                {
                    Shoot();
                    shootCooldown = Random.Range(shootCooldownVec.x, shootCooldownVec.y);
                }
            }
        }

        void IDamagable.TakeDamage(int damageToTake)
        {
            health -= damageToTake;
            healthBar.Damage(damageToTake);

            if (health <= 0)
            {
                StartCoroutine(StartDeath());
            }
        }

        private IEnumerator StartDeath()
        {
            currentState = States.Dead;
            DOTween.Kill(this.transform);

            animator.Play("flame_spirit_explode");
            yield return new WaitForSeconds(1.3f);

            OnDeath?.Invoke();
            Destroy(this.gameObject);
        }

        void IDamagable.Die()
        {
            Debug.Log("DIE");
            
        }

        Character.Team IDamagable.GetTeam()
        {
            return Character.Team.Enemy;
        }
    }

}