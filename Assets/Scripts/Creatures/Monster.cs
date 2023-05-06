using Fusion;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Monster : NetworkBehaviour
{
    [SerializeField] protected float moveSpeed = 3f;

    [Networked] protected TickTimer life { get; set; }

    //health
    [Networked(OnChanged = nameof(OnHealthChanged))] [SerializeField] protected int currentHealth { get; set; }
    [SerializeField] protected int maxHealth;

    [Networked(OnChanged = nameof(OnStateChanged))] protected NetworkBool isDead { get; set; }

    [SerializeField] protected List<Transform> pathPoints;
    protected int currentPathIndex = 0;

    public void Init(string pathString)
    {
        life = TickTimer.CreateFromSeconds(Runner, 1000f);
        pathPoints = GameObject.Find(pathString).GetComponentsInChildren<Transform>().ToList();
        pathPoints.RemoveAt(0);

        maxHealth = currentHealth;
    }

    protected abstract void ChangingState(Changed<Monster> changed);

    public static void OnStateChanged(Changed<Monster> changed)
    {
        changed.Behaviour.ChangingState(changed);
    }

    public static void OnHealthChanged(Changed<Monster> changed)
    {
        changed.Behaviour.ChangingHealth(changed);
    }

    protected abstract void ChangingHealth(Changed<Monster> changed);

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            isDead = true;
            return;
        }
    }
}
