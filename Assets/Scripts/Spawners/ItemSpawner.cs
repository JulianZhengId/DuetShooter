using Fusion;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Collectables regenItemPrefab;
    [SerializeField] private Collectables speedUpItemPrefab;
    private NetworkRunner networkRunner;

    private void Start()
    {
        networkRunner = GetComponent<NetworkRunner>();
    }

    public void SpawnRegenItem(Vector3 position)
    {
        if (networkRunner.IsServer)
        {
            networkRunner.Spawn(regenItemPrefab, position, Quaternion.identity, null, (runner, regenItem) =>
            {
                regenItem.GetComponent<Collectables>().Init();
            });
        }
    }

    public void SpawnSpeedUpItem(Vector3 position)
    {
        if (networkRunner.IsServer)
        {
            networkRunner.Spawn(speedUpItemPrefab, position, Quaternion.identity, null, (runner, speedUpItem) =>
            {
                speedUpItem.GetComponent<Collectables>().Init();
            });
        }
    }
}
