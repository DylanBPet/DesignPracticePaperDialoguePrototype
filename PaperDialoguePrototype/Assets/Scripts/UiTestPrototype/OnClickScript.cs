using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnClickScript : MonoBehaviour
{
    //text displayed and when clicking the mouse
    public TextMeshProUGUI textBox;
    int firstClick = 1;

    //switching the canvases 
    public GameObject textCanvas;
    public GameObject choiceCanvas;

    //answer yes or no
    bool? wantQuestions = null;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            firstClick++;
        }

        if (firstClick == 1)
        {
            //reset
            textBox.text = "Sample Text";
        }
        else if (firstClick == 2)
        {
            textBox.text = "what do you think?";
        }
        else if(firstClick == 3)
        {
            textCanvas.SetActive(false);
            choiceCanvas.SetActive(true);
        }

        //when they either shake their head no or reach for their paper
        if (wantQuestions == true)
        {
            wantQuestions = true;
        }
        else if (wantQuestions == false)
        {
            wantQuestions = null;
            firstClick = 1;
        }

    }
}
