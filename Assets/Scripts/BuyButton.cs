using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public AnimalSO buyReference;
    public Image buyIcon;
    public TMP_Text buyAmountVisual;
    public TMP_Text buyReferenceName;
    public MMF_Player mouseEnterFeedbacks;
    public MMF_Player mouseExitFeedbacks;

    private bool mouseOver;
    private int buyAmount = 1;

    private void Start()
    {
        buyIcon.sprite = buyReference.AnimalIcon;
    }

    private void Update()
    {
        if (mouseOver)
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                buyAmount++;
                buyAmountVisual.text = buyAmount.ToString("N0");
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                buyAmount--;
                buyAmountVisual.text = buyAmount.ToString("N0");
            }
        }
    }

    public void Buy()
    {
        for (int i = 0; i < buyAmount; i++)
        {
            if (PointSystem.RemovePoints(buyReference.AnimalCost))
            {
                GameObject go = Instantiate(GameManager.Instance.animalPrefab, new Vector3(Random.Range(-20f, 20f), 0f, Random.Range(-20f, 20f)), Quaternion.identity);
                go.GetComponent<AIBase>().animal = buyReference;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
        buyReferenceName.text = buyReference.AnimalName;
        mouseEnterFeedbacks.PlayFeedbacks();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
        buyReferenceName.text = "";
        mouseExitFeedbacks.PlayFeedbacks();
    }
}
