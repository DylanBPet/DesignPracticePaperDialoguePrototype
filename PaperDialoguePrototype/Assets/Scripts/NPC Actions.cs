using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPCActions : MonoBehaviour
{
    [Header("Card Detection")]
    public SpriteRenderer cardDropoff;
    public HoveredCardEffects cardScript;

    [Header("Ui Handler")]
    public TextMeshProUGUI textUI;




    // Update is called once per frame
    void Update()
    {
        
        for (int i = 0; i < cardScript.cardsInHand.Count; i++)
        {
            if (cardDropoff.bounds.Contains(cardScript.cardsInHand[i].transform.position) && Mouse.current.leftButton.wasReleasedThisFrame)
            {
                if (i == 0)
                {
                    //turn on ui with message
                    ChangeText("Well go make yourself something.");
                }
                else if (i == 1)
                {
                    //turn on ui with message
                    ChangeText("What the fu-- ehem. This is um... cool..");
                }
                else if (i == 2)
                {
                    //turn on ui with message
                    ChangeText("What do you want me to do about that? Go tell your mother.");
                }
                else if (i == 3)
                {
                    //turn on ui with message
                    ChangeText("It was just a dream kid. Youll get over it.");
                }
            }
        }
    }

    public void ChangeText(string message)
    {
        textUI.text = message;
        cardScript.ResetCards();
    }

}
