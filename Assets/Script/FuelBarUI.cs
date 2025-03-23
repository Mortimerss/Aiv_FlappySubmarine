using UnityEngine;
using UnityEngine.UI;

public class FuelBarUI : MonoBehaviour
{
    public SubmarineManager submarineManager; 
    public Slider fuelSlider;

    void Update()
    {
        if (submarineManager != null && fuelSlider != null)
        {
            fuelSlider.value = submarineManager.fuel / submarineManager.maxFuel;
        }
    }
}