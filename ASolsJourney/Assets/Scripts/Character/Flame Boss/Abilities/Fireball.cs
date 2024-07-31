using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    public class Fireball : Ability
    {
        private Transform target;

        private Vector2 direction;
        private float speed = 8;

        private float lifeTime = 10;
        private float timer;

        private int damage = 20;

        private void Start()
        {
            timer = lifeTime;
        }

        public override void UseAbility(object[] args)
        {
            base.UseAbility(args);
            target = (Transform)args[0];
            direction = (target.position - transform.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        }

        private void Update()
        {
            if (target == null) { Debug.Log("No target assigned!"); return; };
            transform.position += (Vector3)direction * Time.deltaTime * speed;

            timer -= Time.deltaTime;
            if (timer < 0)
            {
                Destroy(gameObject);
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
            if (damagable != null && damagable.GetTeam() == Character.Team.Player)
            {
                damagable.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}