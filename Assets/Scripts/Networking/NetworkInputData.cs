using Fusion;
using UnityEngine;

public struct NetworkInputData : INetworkInput
{
    public bool isShootPressed;
    public bool isRightChangeWeaponPressed;
    public bool isLeftChangeWeaponPressed;

    public bool spawn1Pressed;
    public bool spawn2Pressed;
    public bool spawn3Pressed;

    public Vector2 movement;
}
