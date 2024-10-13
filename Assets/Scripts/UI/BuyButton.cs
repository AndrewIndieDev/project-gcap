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
                if (buyAmount > 20)
                    buyAmount = 20;
                buyAmountVisual.text = buyAmount.ToString();
                buyReferenceName.text = $"{buyReference.AnimalName}\n({(buyReference.AnimalCost * (ulong)buyAmount).ToString("N0")})";
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                buyAmount--;
                if (buyAmount < 1)
                    buyAmount = 1;
                buyAmountVisual.text = buyAmount.ToString();
                buyReferenceName.text = $"{buyReference.AnimalName}\n({(buyReference.AnimalCost * (ulong)buyAmount).ToString("N0")})";
            }
        }
    }

    public void Buy()
    {
        for (int i = 0; i < buyAmount; i++)
        {
            if (PointSystem.RemovePoints(buyReference.AnimalCost))
            {
                GameObject go = Instantiate(GameManager.Instance.animalPrefab, CameraController.Instance.transform.position, Quaternion.identity);
                go.GetComponent<AIBase>().animal = buyReference;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
        buyReferenceName.text = $"{buyReference.AnimalName}\n({(buyReference.AnimalCost * (ulong)buyAmount).ToString("N0")})";
        mouseEnterFeedbacks.PlayFeedbacks();

        if (!CameraController.Instance)
            return;

        CameraController.Instance.enableMouseScrollInput = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
        buyReferenceName.text = "";
        mouseExitFeedbacks.PlayFeedbacks();

        if (!CameraController.Instance)
            return;

        CameraController.Instance.enableMouseScrollInput = true;
    }
}
