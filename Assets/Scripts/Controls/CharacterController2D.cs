using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public void Move(Vector2 movement)
    {
        transform.Translate(movement.x, movement.y, 0);
    }
}
