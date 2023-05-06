using Fusion;
using UnityEngine;

[CreateAssetMenu(fileName = "New Triplet Weapon", menuName = "Weapon/New Triplet Weapon")]
public class TripletWeapon : Weapon
{
    public override void Shoot(NetworkRunner Runner, Vector3 spawnSpot, PlayerRef player)
    {
        Vector3 direction1 = Vector3.right;
        Runner.Spawn(bulletPrefab, spawnSpot, Quaternion.identity, player,
            (runner, bullet) => bullet.GetComponent<Bullet>().Init(direction1, WeaponDamage));

        Vector3 direction2 = new Vector3(1, 0.5f, 0);
        Runner.Spawn(bulletPrefab, spawnSpot, Quaternion.identity, player,
            (runner, bullet) => bullet.GetComponent<Bullet>().Init(direction2, WeaponDamage));

        Vector3 direction3 = new Vector3(1, -0.5f, 0);
        Runner.Spawn(bulletPrefab, spawnSpot, Quaternion.identity, player,
            (runner, bullet) => bullet.GetComponent<Bullet>().Init(direction3, WeaponDamage));
    }
}
