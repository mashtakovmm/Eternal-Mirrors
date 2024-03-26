using UnityEngine;

[CreateAssetMenu(fileName = "Wave 0", menuName = "Gameplay/Wave")]
public class SpawnSettingsSO : ScriptableObject
{
    [SerializeField] private float _maxSpawnTime;
    [SerializeField] private float _minSpawnTime;
    [SerializeField] private int _waveWeight;

    public float MaxSpawnTime => _maxSpawnTime;
    public float MinSpawnTime => _minSpawnTime;
    public int WaveWeight => _waveWeight;
}
