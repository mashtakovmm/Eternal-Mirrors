using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Gameplay/GunData")]
public class GunSO : ScriptableObject
{
    [SerializeField] private string _gunName;
    [SerializeField] private float _reloadSpeed;
    [SerializeField] private float _shootingSpeed;
    [SerializeField] private int _damage;
    [SerializeField] private int _magSize;
    [SerializeField] private float _bulletSpeed;
    // [SerializeField] private bool _isBurst;

    public string GunName => _gunName;
    public float ReloadSpeed => _reloadSpeed;
    public float ShootingSpeed => _shootingSpeed;
    public int Damage => _damage;
    public int MagSize => _magSize;
    public float BulletSpeed => _bulletSpeed;
    // public bool IsBurst => _isBurst;

}