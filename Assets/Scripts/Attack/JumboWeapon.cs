using Fusion;
using UnityEngine;

[CreateAssetMenu(fileName = "New Jumbo Weapon", menuName = "Weapon/New Jumbo Weapon")]
public class JumboWeapon : Weapon
{
    public override void Shoot(NetworkRunner Runner, Vector3 spawnSpot, PlayerRef player)
    {
        Vector3 direction = Vector3.right;
        Runner.Spawn(bulletPrefab, spawnSpot + new Vector3(0.5f, 0, 0), Quaternion.identity, player,
            (runner, bullet) => bullet.GetComponent<Bullet>().Init(direction, WeaponDamage)); ;
    }
}
