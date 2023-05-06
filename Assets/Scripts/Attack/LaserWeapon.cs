using Fusion;
using UnityEngine;

[CreateAssetMenu(fileName = "New Laser Weapon", menuName = "Weapon/New Laser Weapon")]
public class LaserWeapon : Weapon
{
    public override void Shoot(NetworkRunner Runner, Vector3 spawnSpot, PlayerRef player)
    {
        Vector3 direction = new Vector3(1, 0, 0);
        Runner.Spawn(bulletPrefab, spawnSpot, Quaternion.identity, player,
            (runner, laser) => laser.GetComponent<Laser>().Init(direction));
    }
}
