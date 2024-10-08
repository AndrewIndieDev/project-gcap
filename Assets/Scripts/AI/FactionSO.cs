using UnityEngine;

[CreateAssetMenu(fileName = "EmptyFaction", menuName = "AI/Faction")]
public class FactionSO : ScriptableObject
{
    public string factionName;
    public FactionSO[] preyFactions;
    public FactionSO[] predatorFactions;
}
