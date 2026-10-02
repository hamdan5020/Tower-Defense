using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitDisplay : MonoBehaviour
{

    [SerializeField] Unit unitToDisplay;
    [SerializeField] Color affordableColor;
    [SerializeField] Color notAffordableColor;

    private Sprite btnSprite;
    private Button childBtn;

    private TextMeshProUGUI costTMP;
    

    // Start is called before the first frame update
    void Start()
    {
        btnSprite = unitToDisplay.GetComponent<SpriteRenderer>().sprite;
        childBtn = GetComponentInChildren<Button>();
        childBtn.GetComponent<Image>().sprite = btnSprite;


        costTMP = GetComponentInChildren<TextMeshProUGUI>();

        costTMP.text = unitToDisplay.GetCost().ToString();
    }

    
    public void UpdateAfordability(int balance)
    {

        

        if (unitToDisplay.GetCost() > balance)
        {
            childBtn.enabled = false;
            costTMP.color = notAffordableColor;
            
        }else
        {
            childBtn.enabled = true;
            costTMP.color = affordableColor;
        }
    }
}
