using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerCircle : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float scaleUpDuration = 0.25f;
    [SerializeField] private float heightFactor = 0.4f;
    [SerializeField] private float scaleUpMargin = 0.5f;
    
    private float width = 5;
    private float lifeTime = 10f;

    private Vector3 originalScale;

    private void Start()
    {
        spriteRenderer = this.transform.GetComponent<SpriteRenderer>();
        Setup(20, Vector2.zero, 5);
        ShowDanger();
    }

    public void Setup(float width, Vector2 position, float lifeTime)
    {
        this.width = width;
        this.transform.position = position;
        this.transform.localScale = Vector3.zero;
        this.lifeTime = lifeTime;

        originalScale = new Vector3(width, width * heightFactor, 1);
    }

    public void ShowDanger()
    {
        StartCoroutine(KillDanger(lifeTime));

        this.transform.localScale = Vector3.zero;
        this.transform.DOScale(originalScale, scaleUpDuration)
            .OnComplete(()=> 
            {
                Vector3 targetScale = new Vector3(originalScale.x + scaleUpMargin, originalScale.y + scaleUpMargin, originalScale.z);
                this.transform.DOScale(targetScale, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
                spriteRenderer.DOFade(0.3f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
            });
    }

    private IEnumerator KillDanger(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.DOKill();
        spriteRenderer.DOKill();

        spriteRenderer.DOFade(0, 0.5f).OnComplete(() => Destroy(this.gameObject));
    }
}
