using Fusion;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private NetworkCharacterControllerPrototype2D cc;
    [SerializeField] private Shooter shooter;
    [SerializeField] private Vitality vitality;
    [SerializeField] private MonsterSpawner monsterSpawner;

    public override void FixedUpdateNetwork()
    {
        if (vitality.getIsDead()) return;

        if (GetInput(out NetworkInputData data))
        {
            cc.Move(data.movement);
            if (data.isShootPressed) shooter.Shoot();
            if (data.isRightChangeWeaponPressed) shooter.ChangeWeapon(1);
            if (data.isLeftChangeWeaponPressed) shooter.ChangeWeapon(-1);
            if (data.spawn1Pressed) monsterSpawner.Spawn1();
            if (data.spawn2Pressed) monsterSpawner.Spawn2();
            if (data.spawn3Pressed) monsterSpawner.Spawn3();
        } 
    }
}