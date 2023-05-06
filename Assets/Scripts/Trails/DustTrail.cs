using UnityEngine;

public class DustTrail : MonoBehaviour
{
    private float timer;
    [SerializeField] private float timeBetweenSpawn;
    [SerializeField] private Transform spawnSpot;

    private void Awake()
    {
        timer = timeBetweenSpawn;
    }

    void Update()
    {
        if (timer <= 0)
        {
            float randomRotation = Random.Range(-360, 360);
            GameObject trail = ObjectPooling.instance.GetObject0();
            trail.transform.position = spawnSpot.position;
            trail.transform.localRotation = Quaternion.Euler(0, 0, randomRotation);
            trail.SetActive(true);
            timer = timeBetweenSpawn;
        }
        else timer -= Time.deltaTime;

    }
}
