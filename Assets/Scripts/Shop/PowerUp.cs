using UnityEngine;

public class PowerUp : MonoBehaviour, IInteractable
{
    [SerializeField] int price;
    MoneyManager money;
    PlayerContorller player;
    Auitorifle gun;
    SpriteRenderer spriteRenderer;
    Color defaultColor;

    [SerializeField] private PowerUpType powerUpType;

    private enum PowerUpType
    {
        Damage,
        Health,
        MagSize,
        ShootingSpeed,
        ReloadSpeed
    }
    private string powerupName;
    private float value;

    void Start()
    {
        money = FindObjectOfType<MoneyManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
        player = FindAnyObjectByType<PlayerContorller>();
        gun = FindAnyObjectByType<Auitorifle>();

        switch (powerUpType)
        {
            case PowerUpType.Damage:
                value = 1;
                powerupName = "Damage";
                break;
            case PowerUpType.Health:
                value = 10;
                powerupName = "Health";
                break;
            case PowerUpType.MagSize:
                value = 2;
                powerupName = "Mag Size";
                break;
            case PowerUpType.ShootingSpeed:
                value = -0.04f;
                powerupName = "Shooting Speed";
                break;
            case PowerUpType.ReloadSpeed:
                value = -0.3f;
                powerupName = "Reload Speed";
                break;
            default:
                Debug.LogWarning("Unknown power-up type.");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Highligh()
    {
        spriteRenderer.color = new Color(0.5f, 0.7f, 1f, 1f);
    }

    public void RemoveHighligh()
    {
        spriteRenderer.color = defaultColor;
    }

    public void Interact(GameObject obj)
    {
        Debug.Log("buy");
        if (money.Money >= price)
        {
            ApplyStats(powerUpType);
            money.SubMoney(price);
            Destroy(gameObject);
        }

    }

    private void ApplyStats(PowerUpType powerUpType)
    {
        switch (powerUpType)
        {
            case PowerUpType.Damage:
                value = 1;
                gun._damage += (int)value;
                powerupName = "Damage";
                break;
            case PowerUpType.Health:
                value = 10;
                player._maxHealth += (int)value;
                powerupName = "Health";
                break;
            case PowerUpType.MagSize:
                value = 2;
                gun._maxMagSize += (int)value;
                powerupName = "Mag Size";
                break;
            case PowerUpType.ShootingSpeed:
                value = -0.04f;
                gun._shootingSpeed += value;
                powerupName = "Shooting Speed";
                break;
            case PowerUpType.ReloadSpeed:
                value = -0.3f;
                gun._reloadSpeed += value;
                powerupName = "Reload Speed";
                break;
            default:
                Debug.LogWarning("Unknown power-up type.");
                break;
        }

    }

    public string GetTip()
    {
        return $"{powerupName}: {value} for ${price}";
    }
}
