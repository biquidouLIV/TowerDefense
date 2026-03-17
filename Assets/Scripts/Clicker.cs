using UnityEngine;

public class Clicker : MonoBehaviour
{

    [SerializeField] private int clickerGain;
    
    public void OnClick()
    {
        GameManager.instance.money += clickerGain;
    }

    public void Upgrade1()
    {
        clickerGain += 1;
    }
    
    
}
