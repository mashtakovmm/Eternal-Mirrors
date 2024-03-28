using System.Collections;
using UnityEngine;

public class Auitorifle : MonoBehaviour, IGun
{
    [Header("InputReader")]
    [SerializeField] private InputReader inputReader;
    [Header("Gun Data")]
    [SerializeField] private GunSO gunData;
    [Header("Shooting Data")]
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject bulletPrefab;
    [Header("Input Vars")]
    [SerializeField] private float rotateSpeed;
    // input data
    private Vector2 rawMousePos;


    //stuff from data SO
    public string _gunName;
    public float _reloadSpeed;
    public float _shootingSpeed;
    public int _damage;
    public int _maxMagSize;
    public float _bulletSpeed;


    // stuff for shooting logic
    private int currentMagSize;
    public int CurrentMagSize => currentMagSize;
    private bool isReloading;
    bool isShooting;

    private void Awake()
    {
        if (gunData != null)
        {
            _gunName = gunData.GunName;
            _reloadSpeed = gunData.ReloadSpeed;
            _shootingSpeed = gunData.ShootingSpeed;
            _damage = gunData.Damage;
            _maxMagSize = gunData.MagSize;
            _bulletSpeed = gunData.BulletSpeed;
        }
        else
        {
            Debug.LogError("Gun Data object is missing");
        }
    }

    void Start()
    {
        currentMagSize = _maxMagSize;
    }

    void Update()
    {
        Look();
        if (_shootingSpeed <= 0.1)
        {
            _shootingSpeed = 0.1f;
        }

        if (_reloadSpeed <= 0.1)
        {
            _reloadSpeed = 0.1f;
        }
    }

    private void OnEnable()
    {
        inputReader.ShootStartEvent += Shoot;
        inputReader.ShootStopEvent += StopShooting;
        inputReader.ReloadEvent += Reload;
        inputReader.LookEvent += HandleLookEvent;
    }

    private void OnDisable()
    {
        inputReader.ShootStartEvent -= Shoot;
        inputReader.ShootStopEvent -= StopShooting;
        inputReader.ReloadEvent -= Reload;
        inputReader.LookEvent -= HandleLookEvent;
    }

    private void Look()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(rawMousePos);
        mousePosition.z = 0;

        Vector2 direction = mousePosition - transform.position;

        float angle = Vector2.SignedAngle(Vector2.right, direction);

        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public void Shoot()
    {
        isShooting = true;
        StartCoroutine(ShootingCoroutine());
    }

    public void Reload()
    {
        if (currentMagSize != _maxMagSize && !isReloading)
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    private void StopShooting()
    {
        StopCoroutine(ShootingCoroutine());
        isShooting = false;

    }

    private IEnumerator ShootingCoroutine()
    {
        while (isShooting && currentMagSize > 0 && !isReloading)
        {
            yield return new WaitForSeconds(_shootingSpeed);
            Bullet bullet = bulletPrefab.GetComponent<Bullet>();

            bullet.speed = _bulletSpeed;
            bullet.damage = _damage;

            Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);

            currentMagSize--;


            if (currentMagSize <= 0)
            {
                Reload();
            }
        }
        isShooting = false;

    }

    private IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        yield return new WaitForSeconds(_reloadSpeed);
        currentMagSize = _maxMagSize;
        isReloading = false;
    }

    private void HandleLookEvent(Vector2 vector2)
    {
        rawMousePos = vector2;
    }
}
