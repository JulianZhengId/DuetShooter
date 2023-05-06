using Fusion;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shooter : NetworkBehaviour
{
    //bullet
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform bulletSpawnSpot;
   
    //weapon
    [SerializeField] private Weapon[] weapons;
    
    [Networked(OnChanged = nameof(OnWeaponChanged))] public int weaponIndex { get; set; }
    [Networked(OnChanged= nameof(OnBulletAmountChanged))] private int currentBulletAmount { get; set; }
    [SerializeField] private TextMeshProUGUI bulletAmountText;
    [SerializeField] private Image weaponImage;
    private int currentWeaponIndex = 0;

    //shoot timer
    [Networked] private TickTimer delay { get; set; }
    private float timeBetweenShoot = 0.5f;
    private float shootSpeedMultiplier = 1f;

    private void Start()
    {
        timeBetweenShoot = weapons[weaponIndex].TimeBetweenShoot;
    }

    public void Shoot()
    {
        if (!Runner.IsServer || !delay.ExpiredOrNotRunning(Runner)) return;
        delay = TickTimer.CreateFromSeconds(Runner, timeBetweenShoot * shootSpeedMultiplier);
        weapons[weaponIndex].Shoot(Runner, bulletSpawnSpot.position, Object.InputAuthority);

        //decrease bullet except for basic weapon
        if (currentBulletAmount > -100)
        {
            weapons[weaponIndex].BulletAmount -= 1;
        }

        currentBulletAmount = weapons[weaponIndex].BulletAmount;

        if (currentBulletAmount <= 0 && currentBulletAmount > -100)
        {
            ChangeWeapon(1);
        }
    }

    public IEnumerator ChangeShootSpeed(float newShootSpeedMultiplier, float duration)
    {
        shootSpeedMultiplier = newShootSpeedMultiplier;
        yield return new WaitForSeconds(duration);
        shootSpeedMultiplier = 1f;
    }

    public void ChangeWeapon(int i)
    {
        if (!Runner.IsServer) return;
        currentWeaponIndex += i;
        if (currentWeaponIndex < 0) currentWeaponIndex = weapons.Length - 1;
        else if (currentWeaponIndex >= weapons.Length) currentWeaponIndex = 0;

        while (weapons[currentWeaponIndex].BulletAmount <= 0 && weapons[currentWeaponIndex].BulletAmount > -100)
        {
            currentWeaponIndex += i;
            if (currentWeaponIndex < 0) currentWeaponIndex = weapons.Length - 1;
            else if (currentWeaponIndex >= weapons.Length) currentWeaponIndex = 0;
        }
        weaponIndex = currentWeaponIndex;
    }

    public static void OnWeaponChanged(Changed<Shooter> changed)
    {
        //change weapon image
        changed.Behaviour.weaponImage.sprite = changed.Behaviour.weapons[changed.Behaviour.weaponIndex].WeaponImage;
        //change current bullet amount
        if (!changed.Behaviour.Runner.IsServer) return;
        changed.Behaviour.currentBulletAmount = changed.Behaviour.weapons[changed.Behaviour.weaponIndex].BulletAmount;
        changed.Behaviour.timeBetweenShoot = changed.Behaviour.weapons[changed.Behaviour.weaponIndex].TimeBetweenShoot;
    }

    public static void OnBulletAmountChanged(Changed<Shooter> changed)
    {
        if (changed.Behaviour.currentBulletAmount == -100)
        {
            changed.Behaviour.bulletAmountText.text = "\u221E";
        }
        else
        {
            changed.Behaviour.bulletAmountText.text = changed.Behaviour.currentBulletAmount.ToString();
        }
    }

    public override void Spawned()
    {
        string weaponUiToFind = transform.name == "Player Host(Clone)" ? "Weapon Image Player 1" : "Weapon Image Player 2";
        GameObject weaponUI = GameObject.Find(weaponUiToFind);
        weaponIndex = 0;
        weaponImage = weaponUI.GetComponent<Image>();
        bulletAmountText = weaponUI.GetComponentInChildren<TextMeshProUGUI>();
        currentBulletAmount = weapons[weaponIndex].BulletAmount; 
    }
}
