using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    public AnimalSO buyReference;
    public Image buyIcon;

    private void Start()
    {
        buyIcon.sprite = buyReference.AnimalIcon;
    }

    public void Buy()
    {
        if (PointSystem.RemovePoints(buyReference.AnimalCost))
            Instantiate(GameManager.Instance.animalPrefab, new Vector3(Random.Range(-20f, 20f), 0f, Random.Range(-20f, 20f)), Quaternion.identity);
    }
}
