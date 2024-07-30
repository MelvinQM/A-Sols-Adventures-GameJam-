using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [Header("Player Components")]
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameController gc;
    [SerializeField] private CameraUtilities cameraUtils;

    private bool useFloatAnimation = true;
    [SerializeField] private Transform bodySprite;
    private float floatHight = 0.1f;
    private float floatSpeed = 0.7f;
    void Start()
    {
        //GameObject gameControllerObj = GameObject.FindGameObjectWithTag("GameController");
        // if (gc == null)
        // {
        //     Debug.LogError("No GameController found");
        // } else
        // {
        //     gc = gameControllerObj.GetComponent<GameController>();
        // }
        
        healthBar.SetMaxHealth(MaxHp);
        healthBar.SetHealth(CurHp);

        ExampleItem.OnExampleItemPickedUp += ExamplePickup;

        if (useFloatAnimation)
        {
            bodySprite.DOLocalMoveY(floatHight, floatSpeed).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        }
    }

    void ExamplePickup()
    {
        Debug.Log("Player picked up example object");
    }

    void Update()
    {
        healthBar.SetHealth(CurHp);
    }

    public override void Die()
    {
        Debug.Log("Player died");
        lifeState = LifeState.Death;
        playerController.PlayerDeath();

        if (gc != null)
        {
            gc.GameOver();
        }

        //base.Die(); Deleting player = bad idea
    }

    public override void TakeDamage(int damageToTake)
    {
        base.TakeDamage(damageToTake);

        // Shake camera when player is hit
        cameraUtils.ShakeCamera();
    }

    public override void Spawn()
    {
        // Do something
    }

    private void OnDestroy()
    {
        DOTween.Kill(bodySprite);
    }
}
