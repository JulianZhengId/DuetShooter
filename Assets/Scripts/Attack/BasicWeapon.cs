using Fusion;
using UnityEngine;

[CreateAssetMenu(fileName = "New Basic Weapon", menuName = "Weapon/New Basic Weapon")]
public class BasicWeapon : Weapon
{
    public override void Shoot(NetworkRunner Runner, Vector3 spawnSpot, PlayerRef player)
    {
        Vector3 direction = Vector3.right;
        Runner.Spawn(bulletPrefab, spawnSpot, Quaternion.identity, player,
            (runner, bullet) => bullet.GetComponent<Bullet>().Init(direction, WeaponDamage));
    }
}
