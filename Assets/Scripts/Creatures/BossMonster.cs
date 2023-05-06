using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class BossMonster : Monster
{
    [SerializeField] private Image healthBossUI;

    public override void FixedUpdateNetwork()
    {
        transform.position = Vector2.MoveTowards(transform.position, pathPoints[currentPathIndex].position, Time.deltaTime * moveSpeed);
        if (Vector3.Distance(transform.position, pathPoints[currentPathIndex].position) < .25f)
        {
            currentPathIndex++;

            if (currentPathIndex == pathPoints.Count)
            {
                currentPathIndex = 0;
            }
        }
    }

    protected override void ChangingState(Changed<Monster> changed)
    {
        var killEffect = ObjectPooling.instance.GetObject2();
        killEffect.gameObject.SetActive(true);
        killEffect.gameObject.transform.localScale *= 3;
        killEffect.transform.position = changed.Behaviour.transform.position;
        killEffect.Play();

        changed.Behaviour.Runner.Despawn(changed.Behaviour.Object, false);
    }

    protected override void ChangingHealth(Changed<Monster> changed)
    {
        //play hit effect
        var hitEffect = ObjectPooling.instance.GetObject1();
        hitEffect.transform.position = changed.Behaviour.transform.position + new Vector3(-0.25f, 0, 0);
        hitEffect.gameObject.SetActive(true);
        hitEffect.Play();

        if (!healthBossUI) return;
        healthBossUI.fillAmount = currentHealth * 1f / maxHealth;
    }

    public override void Spawned()
    {
        healthBossUI = GameObject.Find("Boss Health").GetComponent<Image>();
        healthBossUI.gameObject.transform.localScale = Vector3.one;
    }
}
