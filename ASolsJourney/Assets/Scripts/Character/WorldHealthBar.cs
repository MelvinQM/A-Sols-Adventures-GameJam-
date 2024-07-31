using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WorldHealthBar : MonoBehaviour
{
    //[SerializeField] private float healthBarWidth = 1;
    //[SerializeField] private float healthBarHeight = 0.1f;
    //[SerializeField] private float backMargin = 0.05f;

    [SerializeField] private Image bar;
    [SerializeField] private GameObject parent;

    public float MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
    private float maxHealth = 100;

    public float CurrentHealth { get { return currentHealth; }}
    private float currentHealth;

    private Tweener healthBarTweener;

    public void Setup(float maxHealth)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
    }

    public void HideHealthBar(bool enable)
    {
        parent.SetActive(enable);
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float currentFill = bar.fillAmount;
        if (healthBarTweener != null)
        {
            DOTween.Kill(healthBarTweener);
        }

        // Use dotween to animate the health bar
        healthBarTweener = DOTween.To(() => currentFill, x => currentFill = x, currentHealth / maxHealth, 0.2f).OnUpdate(() =>
        {
            bar.fillAmount = currentFill;
        });
    }

    private void OnDestroy()
    {
        // Stop all tweens when the object is destroyed
        if (healthBarTweener != null && healthBarTweener.IsActive())
        {
            DOTween.Kill(healthBarTweener);
        }
    }
}
