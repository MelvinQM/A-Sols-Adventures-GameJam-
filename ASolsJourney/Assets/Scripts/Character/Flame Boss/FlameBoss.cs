using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    public class FlameBoss : MonoBehaviour
    {
        private enum States { Spawn, Idle, Run, Chase, Attack }
        [SerializeField] private Vector2 SpawnPos;
        private float fallHeight = 40;
        
        [SerializeField] private EnemyStage[] stages;

        private void Start()
        {
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn() {

            // Show Danger zone with !
            // Move the camera to that position, player may still walk around
            Vector2 spawn = SpawnPos;
            spawn.y += fallHeight;
            this.transform.position = spawn;
            
            DangerManager.Instance.SpawnDangerZone(20, SpawnPos, 2.5f);
            yield return new WaitForSeconds(2);
            this.transform.DOMoveY(SpawnPos.y, 0.3f).OnComplete(()=> 
            {
                CameraUtilities.Instance.ShakeCamera(3, 0.5f);
            });

            // Show boss falling out of the sky and landing with an explosion
            // Show wave annoucement "Flame Boss has arrived!"
            
            // Start the boss fight
        }

        private void StartFight()
        {
            // Start the boss fight
        }

        private void Update()
        {

        }
    }

}