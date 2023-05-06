using Fusion;
using UnityEngine;

[CreateAssetMenu(fileName = "New Duo Weapon", menuName = "Weapon/New Duo Weapon")]
public class DuoWeapon : Weapon
{
    public override void Shoot(NetworkRunner Runner, Vector3 spawnSpot, PlayerRef player)
    {
        Vector3 direction = Vector3.right;
        Runner.Spawn(bulletPrefab, spawnSpot + new Vector3(0, 0.25f, 0), Quaternion.identity, player,
            (runner, bullet) => bullet.GetComponent<Bullet>().Init(direction, WeaponDamage));

        Runner.Spawn(bulletPrefab, spawnSpot + new Vector3(0, -0.25f, 0), Quaternion.identity, player,
            (runner, bullet) => bullet.GetComponent<Bullet>().Init(direction, WeaponDamage));
    }
}
