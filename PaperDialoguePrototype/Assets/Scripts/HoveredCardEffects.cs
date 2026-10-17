using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HoveredCardEffects : MonoBehaviour
{
    [Header("All Cards in Hand")]
    public List<GameObject> cardsInHand;
    

    List<Vector3> cardOriginalRotation = new List<Vector3>();
    List<Vector3> cardOriginalPosition = new List<Vector3>();

    int hoveredCardIndex = -1;

    Coroutine hoverRoutine;

    [Header("Rotation controls on card Hover")]
    public float rotationRange;
    public float rotationSpeedPercent;

    private void Start()
    {
        for (int i = 0; i < cardsInHand.Count; i++)
        {
            cardOriginalRotation.Add(cardsInHand[i].transform.eulerAngles);
            cardOriginalPosition.Add(cardsInHand[i].transform.position);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        int newHoveredCardIndex = -1;

        //checking which card is being hovered
        for (int i = 0; i < cardsInHand.Count; i++)
        {
            SpriteRenderer cardSr = cardsInHand[i].GetComponent<SpriteRenderer>();
            if (cardSr.bounds.Contains(mousePos))
            {
                newHoveredCardIndex = i;
                break;
            }
        }

        //hovering card effects logic
        if (newHoveredCardIndex != hoveredCardIndex)
        {
            if (hoverRoutine != null)
            {
                //reset everything
                StopCoroutine(hoverRoutine);
                hoverRoutine = null;
  
            }

            ResetCards();

            hoveredCardIndex = newHoveredCardIndex;

            if (hoveredCardIndex != -1)
            {
                SpriteRenderer cardSr = cardsInHand[hoveredCardIndex].GetComponent<SpriteRenderer>();
                Debug.Log("Card is rotatin");
                hoverRoutine = StartCoroutine(HoveringCardEffects(hoveredCardIndex, cardSr));
            }
        }
        
        //sorting order & drag card logic
        for (int i = 0; i < cardsInHand.Count; i++)
        {
            SpriteRenderer cardSr = cardsInHand[i].GetComponent<SpriteRenderer>();
            if (cardSr.bounds.Contains(mousePos))
            {
                //put card in front
                cardSr.sortingOrder = 4;
            }
            else if (i == 0)
            {
                cardSr.sortingOrder = 0;

            }
            else if (i == 1)
            {
                cardSr.sortingOrder = 1;

            }
            else if (i == 2)
            {
                cardSr.sortingOrder = 0;

            }
            else if (i == 3)
            {
                cardSr.sortingOrder = -1;
            }

            if (cardSr.bounds.Contains(mousePos) && Mouse.current.leftButton.isPressed)
            {
                cardsInHand[hoveredCardIndex].transform.position = mousePos;
                //NPC script handels the drop off
            }
        }
        
    }

    public IEnumerator HoveringCardEffects(int index, SpriteRenderer cardSR)
    {
        float cardOriginalRotation = cardsInHand[index].transform.eulerAngles.z;

        float rotationLerpt = 0.5f;

        bool lerpComplete = false;

        while (hoveredCardIndex == index)
        {
            //rotationLerpt = rotationLerpt / 2;
           
            //make it shake slightly
            if (lerpComplete == false)
            {
                rotationLerpt += Time.deltaTime;
                //make it shake slightly
                cardsInHand[index].transform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(cardOriginalRotation - rotationRange, cardOriginalRotation + rotationRange, rotationLerpt * rotationSpeedPercent));
                if (rotationLerpt >= 1.01f)
                {
                    rotationLerpt = 1;
                    lerpComplete = true;
                }
            }
            else if (lerpComplete == true)
            {
                rotationLerpt -= Time.deltaTime;
                //make it shake slightly
                cardsInHand[index].transform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(cardOriginalRotation - rotationRange, cardOriginalRotation + rotationRange, rotationLerpt * rotationSpeedPercent));
                if (rotationLerpt <= -0.01f)
                {
                    rotationLerpt = 0;
                    lerpComplete = false;
                }
            }
            yield return null;
        }
    }

    public void ResetCards()
    {
        for (int i = 0; i < cardsInHand.Count; i++)
        {
            cardsInHand[i].transform.eulerAngles = cardOriginalRotation[i];
            cardsInHand[i].transform.position = cardOriginalPosition[i];
        }
    }
}
