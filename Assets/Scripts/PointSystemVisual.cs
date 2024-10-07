using TMPro;
using UnityEngine;

public class PointSystemVisual : MonoBehaviour
{
    public TMP_Text pointVisual;
    private void Start() => TickSystem.onTick += OnTick;
    private void OnTick(int tickIndex) => pointVisual.text = PointSystem.ToString();
}
