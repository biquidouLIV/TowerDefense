using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Clicker : MonoBehaviour
{

    [SerializeField] private int clickerGain = 1;
    [SerializeField] private int clickPerSec;

    [SerializeField] private int upgrade1Price = 100;
    [SerializeField] private int upgrade2Price = 100;

    [SerializeField] TMP_Text upgrade1Text;
    [SerializeField] TMP_Text upgrade2Text;
    


    private void Start()
    {
        StartCoroutine(GiveClick());
        upgrade1Text.text = upgrade1Price.ToString();
        upgrade2Text.text = upgrade2Price.ToString();

    }

    private IEnumerator GiveClick()
    {
        GameManager.instance.money += clickPerSec;
        yield return new WaitForSeconds(1);
        StartCoroutine(GiveClick());
    }

    public void OnClick()
    {
        GameManager.instance.money += clickerGain;
        
    }

    public void Upgrade1()
    {
        if (GameManager.instance.money < upgrade1Price)
        {
            return;
        }

        GameManager.instance.money -= upgrade1Price;
        
        clickerGain += 1;
        upgrade1Price = (int)(upgrade1Price * 1.5 * 1.5);
        upgrade1Text.text = upgrade1Price.ToString();
        
    }

    public void Upgrade2()
    {
        if (GameManager.instance.money < upgrade2Price)
        {
            return;
        }

        GameManager.instance.money -= upgrade2Price;
        
        clickPerSec += 1;
        upgrade2Price = (int)(upgrade2Price * 1.5 * 1.5);
        upgrade2Text.text = upgrade2Price.ToString();
    }
}
