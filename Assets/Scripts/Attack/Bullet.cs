using Fusion;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    private Vector3 direction;
    private int damage;

    [Networked] private TickTimer life { get; set; }

    [SerializeField] private BulletColliderHandler bulletColliderHandler;

    public void Init(Vector3 direction, int weaponDamage)
    {
        life = TickTimer.CreateFromSeconds(Runner, 5f);
        this.direction = direction;
        damage = weaponDamage;
    }

    public override void FixedUpdateNetwork()
    {
        if (life.Expired(Runner))
        {
            Runner.Despawn(Object);
        }
        else
        {
            transform.position += moveSpeed * direction * Runner.DeltaTime;
        }
    }

    public override void Spawned()
    {
        bulletColliderHandler.Init(Object.InputAuthority.PlayerId == 0);
    }

    public int GetBulletDamage()
    {
        return damage;
    }
}
