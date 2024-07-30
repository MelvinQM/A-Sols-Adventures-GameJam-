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

    private void Start()
    {
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

        // Use dotween to animate the health bar
        DOTween.To(() => currentFill, x => currentFill = x, currentHealth / maxHealth, 0.2f).OnUpdate(() =>
        {
            bar.fillAmount = currentFill;
        });
    }
}
