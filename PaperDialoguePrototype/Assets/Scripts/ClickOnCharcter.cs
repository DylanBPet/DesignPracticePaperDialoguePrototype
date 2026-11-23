using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickOnCharcter : MonoBehaviour
{
    public GameObject[] squareArray;
    public GameObject cameraMoveTo;

    CameraMove movingCamScript;

    float t = 0;

    bool hasClicked = false;

    void Start()
    {
        movingCamScript = cameraMoveTo.GetComponent<CameraMove>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (hasClicked == false)
            {
                for (int i = 0; i < squareArray.Length; i++)
                {
                    SpriteRenderer squareSR = squareArray[i].GetComponent<SpriteRenderer>();
                    //if (squareSR.bounds.Contains(mousePos))
                    //{
                    StartCoroutine(OnClick(i));
                    //}
                }
                hasClicked = true;
            }
            
            
        }
    }

    public IEnumerator OnClick(int i)
    {
        SpriteRenderer gSquareSR = squareArray[i].GetComponent<SpriteRenderer>();
        Color alpha = gSquareSR.color;
        //make object center
        movingCamScript.CenterCameraStart(squareArray[i].transform.position);
        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            //make object larger
            squareArray[i].transform.localScale = Vector2.Lerp(Vector2.one, Vector2.one * 3, t);

            //make object fade?
            alpha.a -= Time.deltaTime / 1.2f;
            gSquareSR.color = alpha;

            yield return null;
        }
    }
}
