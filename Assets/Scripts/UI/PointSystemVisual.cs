using TMPro;
using UnityEngine;

public class PointSystemVisual : MonoBehaviour
{
    public TMP_Text pointVisual;
    public void Start()
    {
        TickSystem.onTick += OnTick;
    }
    private void OnDestroy()
    {
        TickSystem.onTick -= OnTick;
    }
    private void OnTick(int tickIndex) => pointVisual.text = PointSystem.ToString();
}
