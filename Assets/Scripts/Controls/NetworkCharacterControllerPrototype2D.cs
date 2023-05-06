using Fusion;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
[OrderBefore(typeof(NetworkTransform))]
[DisallowMultipleComponent]
// ReSharper disable once CheckNamespace
public class NetworkCharacterControllerPrototype2D : NetworkTransform
{
    [Header("Character Controller Settings")]
    public float speedMultiplier = 100f;

    public CharacterController2D Controller { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        CacheController();
    }

    public override void Spawned()
    {
        base.Spawned();
        CacheController();
    }

    private void CacheController()
    {
        if (Controller == null)
        {
            Controller = GetComponent<CharacterController2D>();

            Assert.Check(Controller != null, $"An object with {nameof(NetworkCharacterControllerPrototype)} must also have a {nameof(CharacterController)} component.");
        }
    }

    protected override void CopyFromBufferToEngine()
    {
        // Trick: CC must be disabled before resetting the transform state
        Controller.enabled = false;

        // Pull base (NetworkTransform) state from networked data buffer
        base.CopyFromBufferToEngine();

        // Re-enable CC
        Controller.enabled = true;
    }


    /// <summary>
    /// Basic implementation of a character controller's movement function based on an intended direction.
    /// <param name="direction">Intended movement direction, subject to movement query, acceleration and max speed values.</param>
    /// </summary>
    public virtual void Move(Vector2 movement)
    {
        var deltaTime = Runner.DeltaTime;

        Controller.Move(movement * deltaTime * speedMultiplier);
    }
}