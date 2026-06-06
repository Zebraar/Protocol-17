using UnityEngine;
using UnityEngine.UI;

public class CardItem : MonoBehaviour
{
    public Text cardNameText;
    public Button button;
    public Text cardCostText;
    public Image cardLogo;

    private EventChoice choice;
    private GameManager gameManager;

    public void Setup(EventChoice eventChoice, GameManager manager)
    {
        choice = eventChoice;
        gameManager = manager;
        
        cardNameText.text = choice.card.cardName;
        cardCostText.text = "Энергия: " + choice.card.energyCost.ToString();
        cardLogo.sprite = choice.card.cardLogo;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        gameManager.SelectChoice(choice);
    }
}