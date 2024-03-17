using UnityEngine;

[CreateAssetMenu(fileName = "New NPC Attributes", menuName = "NPC Attributes")]
public class NPCAttributes : ScriptableObject
{
    public int _healthPoints;
    public float _lookRange;
    public float _atkRange;
    public float _speed;
    public float _damage;
}