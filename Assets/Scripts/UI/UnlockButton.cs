using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnlockButton : MonoBehaviour
{
    public AnimalSO animal;
    public Image backgroundImage;
    public Image animalImage;
    public TMP_Text unlockName;
    public TMP_Text unlockCost;
    public Color unlockedColour;
    public GameObject buyButtonPrefab;
    public Transform buyButtonParent;

    private bool unlocked;

    private void Start()
    {
        animalImage.sprite = animal.AnimalIcon;
        unlockCost.text = animal.UnlockCost.ToString("N0");
        unlockName.text = animal.AnimalName;
    }

    public void Unlock()
    {
        if (unlocked || PointSystem.Points < animal.UnlockCost)
            return;

        PointSystem.RemovePoints(animal.UnlockCost);
        backgroundImage.color = unlockedColour;
        GameObject go = Instantiate(buyButtonPrefab, buyButtonParent);
        go.GetComponent<BuyButton>().buyReference = animal;
        unlocked = true;
        unlockCost.text = "--- unlocked ---";
    }
}
