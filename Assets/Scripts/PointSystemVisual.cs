using TMPro;
using UnityEngine;

public class PointSystemVisual : MonoBehaviour
{
    public TMP_Text pointVisual;
    private void Start() => TickSystem.onTick += OnTick;
    private void OnTick() => pointVisual.text = PointSystem.ToString();
}
