using Fusion;
using UnityEngine;

public class PlayerColliderHandler : NetworkBehaviour
{
    private Vitality healthManager;
    private Shooter shooter;

    private void Awake()
    {
        healthManager = transform.root.GetComponent<Vitality>();
        shooter = transform.root.GetComponent<Shooter>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Untagged")
        {
            if (collision.tag == "Monster")
            {
                healthManager.TakeDamage(1);
            }
            else
            {
                Runner.Despawn(collision.transform.parent.gameObject.GetComponent<NetworkObject>());
                if (collision.tag == "SpeedUpItem")
                {
                    StartCoroutine(shooter.ChangeShootSpeed(0.5f, 15f));
                }
                else if (collision.tag == "RegenItem")
                {
                    healthManager.AddHealth(1);
                    Debug.Log(transform.root.name + " get HP");
                }
            }
        }
    }
}
