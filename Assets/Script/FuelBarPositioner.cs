using UnityEngine;

public class FuelBarPositioner : MonoBehaviour
{
    public RectTransform fuelBar;  
    public Transform fuelBarPosition;

    void Update()
    {
        if (fuelBar != null && fuelBarPosition != null)
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(fuelBarPosition.position);
            fuelBar.position = screenPos;
        }
    }
}