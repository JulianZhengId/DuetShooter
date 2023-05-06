using Fusion;
using System;
using System.Collections;
using UnityEngine;

public class Vitality : NetworkBehaviour
{
    //health system
    [Networked(OnChanged = nameof(OnHealthChanged))] private byte currentHealth { get; set; }

    private const byte startingHealth = 3;
    private const byte maxHealth = 5;

    private bool isTakingDamage;

    [SerializeField] private GameObject healthUI;

    //invinciblity
    [Networked] private NetworkBool isInvincible { get; set; }
    [SerializeField] private SpriteRenderer sprite;
    private float invincibleDuration = 2f;

    //death
    [Networked] private NetworkBool isDead { get; set; }
    [SerializeField] private GameObject deathEffect;

    public override void Spawned()
    {
        isTakingDamage = false;
        currentHealth = startingHealth;
        isDead = false;
        string healthUiToFind = transform.name == "Player Host(Clone)" ? "Health Player 1" : "Health Player 2";
        string deathEffectToFind = transform.name == "Player Host(Clone)" ? "Death Effect P1" : "Death Effect P2";

        healthUI = GameObject.Find(healthUiToFind);
        deathEffect = GameObject.Find(deathEffectToFind);
    }

    public static void OnHealthChanged(Changed<Vitality> changed)
    {
        //change user interface
        changed.Behaviour.ChangeHealthUI(changed.Behaviour.healthUI, changed.Behaviour.currentHealth);
    }

    private void ChangeHealthUI(GameObject healthUI, byte health)
    {
        if (isTakingDamage)
        {
            healthUI.transform.GetChild(health).GetChild(0).gameObject.SetActive(true);
        }

        StartCoroutine(SetHealthUI(healthUI, health));
    }

    private IEnumerator SetHealthUI(GameObject healthUI, byte health)
    {
        yield return new WaitForSeconds(0.5f);
        healthUI.transform.GetChild(health).GetChild(0).gameObject.SetActive(false);

        for (int i = 0; i < maxHealth; i++)
        {
            if (i < currentHealth)
            {
                healthUI.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                healthUI.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void TakeDamage(byte damage)
    {
        if (isDead || isInvincible) return;

        currentHealth -= damage;
        isTakingDamage = true;

        if (currentHealth <= 0)
        {
            isDead = true;
            gameObject.transform.localScale = Vector3.zero;
            StartCoroutine(DisablePlayer());

            //play death effect
            deathEffect.transform.position = transform.position;
            deathEffect.GetComponent<ParticleSystem>().Play();
            return;
        }

        //make invincible and blinking
        isInvincible = true;
        StartCoroutine(StartBlinking());
        StartCoroutine(DisableInvincible());
    }

    private IEnumerator DisablePlayer()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    private IEnumerator DisableInvincible()
    {
        yield return new WaitForSeconds(invincibleDuration);
        isInvincible = false;
    }

    private IEnumerator StartBlinking()
    {
        while (isInvincible)
        {
            sprite.enabled = !sprite.enabled;
            yield return new WaitForSeconds(0.075f);
        }
        sprite.enabled = true;
    }

    public void AddHealth(byte amount)
    {
        if (isDead || currentHealth >= maxHealth) return;

        isTakingDamage = false;
        currentHealth += amount;
    }

    public bool getIsDead()
    {
        return isDead;
    }
}
