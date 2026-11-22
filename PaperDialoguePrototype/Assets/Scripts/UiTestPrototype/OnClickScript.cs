using System.Collections;
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
    public GameObject paperChoiceCanvas;

    public GameObject backpack;
    public GameObject paper;

    Vector2 backpackOrigin;
    public float objectMoveSpeed;
    void Start()
    {
        backpackOrigin = backpack.transform.position;
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



    }

    public void StartBackpackAnim()
    {
        choiceCanvas.SetActive(false);
        //start backpack coroutine
        StartCoroutine(backpackEnum());
    }

    public void Reset()
    {
        firstClick = 1;
        textCanvas.SetActive(true);
        choiceCanvas.SetActive(false);
    }

    public IEnumerator backpackEnum()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * objectMoveSpeed;
            //move backpack onto screen
            backpack.transform.position = Vector2.Lerp(backpack.transform.position, Vector2.zero, t);
            
            yield return null;
        }
        Debug.Log("backpack in position");

        t = 0;

        //move it offscreen but leave paper
        while (t < 1)
        {
            paper.transform.position = Vector2.zero;
            backpack.transform.position = Vector2.Lerp(backpack.transform.position, backpackOrigin, t);
            t += Time.deltaTime * objectMoveSpeed;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        paper.SetActive(false);
        paperChoiceCanvas.SetActive(true);

    }

}
