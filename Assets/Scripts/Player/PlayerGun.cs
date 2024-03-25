using System.Collections;
using UnityEngine;

public class PlayerGun : MonoBehaviour, IGun
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
    private string _gunName;
    public string GunName => _gunName;
    private float _reloadSpeed;
    private float _shootingSpeed;
    private int _damage;
    private int _maxMagSize;
    private float _bulletSpeed;

    // stuff for shooting logic
    private int currentMagSize;
    private float nextShotTime = 0f;
    private bool isReloading;

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
        if (nextShotTime > 0)
        {
            nextShotTime -= Time.deltaTime;
        }
    }

    private void OnEnable()
    {
        inputReader.ShootEvent += Shoot;
        inputReader.ReloadEvent += Reload;
        inputReader.LookEvent += HandleLookEvent;
    }

    private void OnDisable()
    {
        inputReader.ShootEvent -= Shoot;
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
        if (nextShotTime <= 0 && currentMagSize > 0 && !isReloading)
        {
            Bullet bullet = bulletPrefab.GetComponent<Bullet>();

            bullet.speed = _bulletSpeed;
            bullet.damage = _damage;

            Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);

            currentMagSize--;
            nextShotTime = _shootingSpeed;
        }
        Debug.Log(currentMagSize);

        if (currentMagSize <= 0)
        {
            Reload();
        }
    }

    public void Reload()
    {
        if (currentMagSize != _maxMagSize && !isReloading) 
        {
            StartCoroutine(ReloadCoroutine());
        }
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
