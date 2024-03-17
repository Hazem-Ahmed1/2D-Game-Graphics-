using UnityEngine;

[CreateAssetMenu(fileName = "New NPC Attributes", menuName = "NPC Attributes")]
public class NPCAttributes : ScriptableObject
{
    public int healthPoints;
    public float lookRange;
    public float atkRange;
    public float speed;
    public float damage;
}