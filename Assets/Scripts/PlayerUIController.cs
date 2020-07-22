using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public TextMeshProUGUI HPText;
    public TextMeshProUGUI ShildText;
    public TextMeshProUGUI BulletsText;
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI MutagenText;
    public TextMeshProUGUI SanorinText;
    public TextMeshProUGUI MedText;
    public Image WeaponImagine;
    public Image HPImagine;
    public Image ShildImagine;
    public Image SteelArmsImage;
    public void SetHp(float MaxHp, float curentHP)
    {
        HPImagine.fillAmount = curentHP / MaxHp;
        HPText.text = $"{Mathematics.GetValueInPercent(MaxHp, curentHP)}%";
    }
    public void SetShild(float maxShild, float curentShild)
    {
        ShildImagine.fillAmount = curentShild / maxShild;
        ShildText.text = $"{Mathematics.GetValueInPercent(maxShild, curentShild)}%";
    }
    public void SetBullet(int bullets)
    {
        BulletsText.text = $"{bullets}";
       
    }
    public void SetSteelArmsImage(Sprite sprite)
    {

    }
    public void SetMoney(int money)
    {
        MoneyText.text = $"{money}";
    }
    public void SetMutagenCount(int count)
    {
        MutagenText.text = $"{count}";
    }
    public void SetSanorinCount(int count)
    {
        SanorinText.text = $"{count}";
    }
    public void SetMedicineCount(int count)
    {
        MedText.text = $"{count}";
    }

}
