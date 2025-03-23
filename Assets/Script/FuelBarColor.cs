using UnityEngine;
using UnityEngine.UI;

public class FuelBarColor : MonoBehaviour
{
    public SubmarineManager submarineManager;
    public Slider fuelSlider;
    public Image fillImage; 

    public Color greenColor = Color.green;
    public Color orangeColor = new Color(1f, 0.5f, 0f); 
    public Color redColor = Color.red;

    void Update()
    {
        if (submarineManager != null && fuelSlider != null && fillImage != null)
        {
            float fillValue = fuelSlider.value;

            if (fillValue > 0.66f) // Green for 67-100%
            {
                fillImage.color = greenColor;
            }
            else if (fillValue > 0.33f) // Orange for 34-66%
            {
                fillImage.color = orangeColor;
            }
            else // Red for 0-33%
            {
                fillImage.color = redColor;
            }
        }
    }
}