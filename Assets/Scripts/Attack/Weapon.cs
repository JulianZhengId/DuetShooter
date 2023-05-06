using Fusion;
using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    [SerializeField] private int bulletAmount;
    [SerializeField] private Sprite weaponImage;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] private float timeBetweenShoot;
    [SerializeField] private int weaponDamage;

    public int BulletAmount { get => bulletAmount; set => bulletAmount = value; }

    public Sprite WeaponImage { get => weaponImage; set => weaponImage = value; }

    public float TimeBetweenShoot { get => timeBetweenShoot; set => timeBetweenShoot = value; }

    public int WeaponDamage { get => weaponDamage; set => weaponDamage = value; }

    public abstract void Shoot(NetworkRunner Runner, Vector3 spawnSpot, PlayerRef player);
}
