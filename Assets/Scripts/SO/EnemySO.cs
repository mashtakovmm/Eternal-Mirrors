using UnityEngine;

[CreateAssetMenu(fileName ="Enemy", menuName ="Gameplay/EnemyData")]
public class EnemySO : ScriptableObject
{
    [SerializeField] private int _health;
    [SerializeField] private int _score;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;

    public int Health => _health;
    public int Score => _score;
    public float Speed => _speed;
    public int Damage => _damage;
}
