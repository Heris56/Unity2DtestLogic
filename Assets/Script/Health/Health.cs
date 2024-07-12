using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("Iframes")]
    [SerializeField] private float iframesDuration;
    [SerializeField] private int numberofFlashes;
    private SpriteRenderer spriterend;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriterend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
        
        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
            }
        }
    }

    public void addHealth(float health)
    {
        currentHealth = Mathf.Clamp(currentHealth + health, 0, startingHealth);
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
        // duration
        for (int i = 0; i < numberofFlashes; i++)
        {
            spriterend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iframesDuration / (numberofFlashes * 2));
            spriterend.color = Color.white;
            yield return new WaitForSeconds(iframesDuration / (numberofFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }
}
