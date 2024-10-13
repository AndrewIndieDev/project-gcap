using UnityEngine;
using UnityEngine.UI;

public class UIHealthAndEnergy : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] public GameObject Canvas;
    [SerializeField] Health Health;
    [SerializeField] Energy Energy;

    [Header("Health Bar")]
    [SerializeField] GameObject healthGO;
    [SerializeField] Image healthBar;

    [Header("Energy Bar")]
    [SerializeField] GameObject energyGO;
    [SerializeField] Image energyBar;

    void Start()
    {
        TickSystem.onTick += OnTick;
        if(Camera.main)
            Canvas.transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
                         Camera.main.transform.rotation * Vector3.up);
        Canvas.SetActive(false);

        if(GetComponent<AIBase>().animal.AnimalFaction.factionName == "Plant")
            Canvas = null;
    }

    private void Update()
    {
        if(Canvas)
            Canvas.transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
                         Camera.main.transform.rotation * Vector3.up);
    }

    void OnTick(int tickIndex)
    {
        ManageEnergyBar();
        ManageHealthBar();

        //bool condition = healthGO.activeSelf || energyGO.activeSelf;
        //Canvas.SetActive(condition);
    }

    void ManageHealthBar()
    {
        if (!Health)
            return;

        healthBar.fillAmount = Health.CurrentHealth / Health.MaxHealth;

        //healthGO.SetActive(Health.CurrentHealth < Health.MaxHealth);
    }

    private void ManageEnergyBar()
    {
        if (!Energy)
            return;

        energyBar.fillAmount = Energy.CurrentEnergy / Energy.MaxEnergy;
        //energyGO.SetActive(Energy.CurrentEnergy < (Energy.MaxEnergy));
    }
}
