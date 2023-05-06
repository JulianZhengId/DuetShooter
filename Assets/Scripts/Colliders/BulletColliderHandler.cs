using Fusion;
using System.Collections;
using UnityEngine;

public class BulletColliderHandler : NetworkBehaviour
{
    private bool isClient;
    private Bullet bullet;

    public void Init(bool b)
    {
        isClient = b;
        bullet = transform.root.GetComponent<Bullet>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Untagged") return;
        else if (collision.tag == "Monster")
        {
            //destroy bullet
            Runner.Despawn(Object, false);

            //destroy object
            //Runner.Despawn(collision.transform.parent.gameObject.GetComponent<NetworkObject>());

            //add score to bullet owner
            //if (isClient) ClientScore.instance.AddPoints(10);
            //else HostScore.instance.AddPoints(10);

            //deal damage
            collision.transform.root.GetComponent<Monster>().TakeDamage(bullet ? bullet.GetBulletDamage() : 5);
        }
    }
}
