using Fusion;
using UnityEngine;

public class MockSpawner : MonoBehaviour
{
    [SerializeField] private Monster monsterPrefab;
    [SerializeField] private Monster bossMonsterPrefab;

    public void Spawn1()
    {
        var networkRunner = GetComponent<Player>().Runner;
        if (networkRunner.IsServer)
        {
            networkRunner.Spawn(monsterPrefab, new Vector3(16, 2, 0), Quaternion.identity, null, (runner, monster) =>
            {
                monster.GetComponent<Monster>().Init("Path 0");
            });

            networkRunner.Spawn(monsterPrefab, new Vector3(17.5f, 2, 0), Quaternion.identity, null, (runner, monster) =>
            {
                monster.GetComponent<Monster>().Init("Path 0");
            });

            networkRunner.Spawn(monsterPrefab, new Vector3(19f, 2, 0), Quaternion.identity, null, (runner, monster) =>
            {
                monster.GetComponent<Monster>().Init("Path 0");
            });

            networkRunner.Spawn(monsterPrefab, new Vector3(20.5f, 2, 0), Quaternion.identity, null, (runner, monster) =>
            {
                monster.GetComponent<Monster>().Init("Path 0");
            });
        }
    }

    public void Spawn2()
    {
        var networkRunner = GetComponent<Player>().Runner;
        if (networkRunner.IsServer)
        {
            networkRunner.Spawn(monsterPrefab, new Vector3(16, -2, 0), Quaternion.identity, null, (runner, monster) =>
            {
                monster.GetComponent<Monster>().Init("Path 1");
            });

            networkRunner.Spawn(monsterPrefab, new Vector3(17.5f, -2, 0), Quaternion.identity, null, (runner, monster) =>
            {
                monster.GetComponent<Monster>().Init("Path 1");
            });

            networkRunner.Spawn(monsterPrefab, new Vector3(19f, -2, 0), Quaternion.identity, null, (runner, monster) =>
            {
                monster.GetComponent<Monster>().Init("Path 1");
            });

            networkRunner.Spawn(monsterPrefab, new Vector3(20.5f, -2, 0), Quaternion.identity, null, (runner, monster) =>
            {
                monster.GetComponent<Monster>().Init("Path 1");
            });
        }
    }

    public void Spawn3()
    {
        var networkRunner = GetComponent<Player>().Runner;
        if (networkRunner.IsServer)
        {
            networkRunner.Spawn(bossMonsterPrefab, new Vector3(16, 0, 0), Quaternion.identity, null, (runner, monster) =>
            {
                monster.GetComponent<Monster>().Init("Path 2");
            });
        }
    }
}
