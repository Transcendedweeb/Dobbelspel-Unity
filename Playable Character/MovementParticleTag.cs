using UnityEngine;

public class MovementParticleTag : MonoBehaviour
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        BUp,
        BDown,
        BLeft,
        BRight,
    }

    public Direction direction;
}   