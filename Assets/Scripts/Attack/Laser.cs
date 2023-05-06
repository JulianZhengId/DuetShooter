using UnityEngine;
using Fusion;

public class Laser : NetworkBehaviour
{
    [SerializeField] private LineRenderer laser;
    [SerializeField] private CapsuleCollider2D laserCollider;
    [Networked] private float rayDistance { get; set; }

    [SerializeField] private float moveSpeed = 3f;
    private Vector3 direction;

    [Networked] private TickTimer life { get; set; }

    [SerializeField] private BulletColliderHandler bulletColliderHandler;

    public void Init(Vector3 direction)
    {
        life = TickTimer.CreateFromSeconds(Runner, 5f);

        this.direction = direction;
        rayDistance = Random.Range(0.35f, 0.5f);
        laserCollider.offset = new Vector2(rayDistance / 2, 0);
        laserCollider.size = new Vector2(-rayDistance, 0.15f);
        Draw2DRay(Vector3.zero, Vector3.right * rayDistance);
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

    private void Draw2DRay(Vector3 startPos, Vector3 endPos)
    {
        laser.SetPosition(0, startPos); 
        laser.SetPosition(1, endPos);
    }
}
