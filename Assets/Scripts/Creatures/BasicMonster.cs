using Fusion;
using UnityEngine;

public class BasicMonster : Monster
{
    public override void FixedUpdateNetwork()
    {
        if (life.Expired(Runner))
        {
            Runner.Despawn(Object);
        }
        else if (currentPathIndex < pathPoints.Count)
        {
            transform.position = Vector2.MoveTowards(transform.position, pathPoints[currentPathIndex].position, Time.deltaTime * moveSpeed);
            if (Vector3.Distance(transform.position, pathPoints[currentPathIndex].position) < .5f)
            {
                currentPathIndex++;

                if (currentPathIndex == pathPoints.Count)
                {
                    Runner.Despawn(Object);
                }
            }
        }
    }

    protected override void ChangingState(Changed<Monster> changed)
    {
        var killEffect = ObjectPooling.instance.GetObject2();
        killEffect.gameObject.SetActive(true);
        killEffect.transform.position = changed.Behaviour.transform.position;
        killEffect.Play();

        changed.Behaviour.Runner.Despawn(changed.Behaviour.Object, false);
    }

    protected override void ChangingHealth(Changed<Monster> changed)
    {
        //play hit effect
        var hitEffect = ObjectPooling.instance.GetObject1();
        if (!hitEffect) return;

        hitEffect.transform.position = changed.Behaviour.transform.position + new Vector3(-0.25f, 0, 0);
        hitEffect.gameObject.SetActive(true);
        hitEffect.Play();
    }
}
