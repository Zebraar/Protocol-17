using UnityEngine;

[CreateAssetMenu(menuName = "Protocol17/Card")]
public class CardSO : ScriptableObject
{
    public string cardName;
    public Sprite cardLogo;
    public int energyCost;
    public int energyAdd;
}
