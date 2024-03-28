using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("references")]
    [SerializeField] PlayerContorller player;
    [SerializeField] Auitorifle playerGun;
    [SerializeField] MoneyManager moneyManager;
    [Header("UI panels")]
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text moneyText;
    [SerializeField] TMP_Text damage;
    [SerializeField] TMP_Text reloadSpeed;
    [SerializeField] TMP_Text fireRate;
    [SerializeField] TMP_Text magSize;
    [SerializeField] TMP_Text ammo;
    [SerializeField] GameObject tip;
    [SerializeField] TMP_Text tipText;

    private void Update()
    {
        healthText.text = $"Health: {player.MaxHealth}/{player.Health}";
        moneyText.text = $"$ {moneyManager.Money}";
        damage.text = $"Damage: {playerGun._damage}";
        reloadSpeed.text = $"Reload Speed: {playerGun._reloadSpeed}";
        fireRate.text = $"Fire Rate: {playerGun._shootingSpeed}";
        magSize.text = $"Mag Size: {playerGun._maxMagSize}";
        ammo.text = $"{playerGun.CurrentMagSize} / {playerGun._maxMagSize}";
        if (player.currentTip != "")
        {
            tip.SetActive(true);
            tipText.text = $"{player.currentTip}";
        }
        else
        {
            tip.SetActive(false);
        }

    }
}
