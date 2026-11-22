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

    public Vector2 mousePos;

    bool paperClickable = false;
    void Start()
    {
        backpackOrigin = backpack.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

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

        SpriteRenderer paperSr = paper.GetComponent<SpriteRenderer>();
        if (paperSr.bounds.Contains(mousePos) && Mouse.current.leftButton.wasPressedThisFrame && paperClickable)
        {
            StartCoroutine(getPaperReady());
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
        Vector2 start = backpack.transform.position;
        while (t < 1)
        {
            t += Time.deltaTime * objectMoveSpeed;
            //move backpack onto screen
            backpack.transform.position = Vector2.Lerp(start, Vector2.zero, t);
            yield return null;
        }
        backpack.transform.position = Vector2.zero;
        Debug.Log("backpack in position");
        paperClickable = true;
        yield return null;
    }

    public IEnumerator getPaperReady()
    {
        paperClickable = false;
        float t = 0;
        //move it offscreen but leave paper
        while (t < 1)
        {
            paper.transform.position = Vector2.zero;
            backpack.transform.position = Vector2.Lerp(backpack.transform.position, backpackOrigin, t);
            t += Time.deltaTime * objectMoveSpeed;
            yield return null;
        }
        paper.SetActive(false);
        paperChoiceCanvas.SetActive(true);

    }

}
