using UnityEngine;
using UnityEngine.UI;

public class OutlineEffect : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
public Outline outline; // Referencia al componente Outline

    [Header("Color Settings")]
    public float glowSpeed = 2f; // Velocidad del brillo
    public Color minColor = new Color(0.8f, 0.8f, 0.8f, 1f); // Color más tenue
    public Color maxColor = Color.white; // Color más brillante

    [Header("Thickness Settings")]
    public float pulseSpeed = 2f; // Velocidad de la pulsación
    public float minThickness = 1f; // Grosor mínimo
    public float maxThickness = 5f; // Grosor máximo

    private float currentThickness; // Grosor actual
    private bool isIncreasingThickness = true; // Controla si el grosor está aumentando
    private bool isIncreasingColor = true; // Controla si el brillo está aumentando

    void Start()
    {
        if (outline == null)
            outline = GetComponent<Outline>();
    }

    void Update()
    {
        if (outline != null)
        {
            // Actualizar el color del outline
            if (isIncreasingColor)
                outline.effectColor = Color.Lerp(outline.effectColor, maxColor, glowSpeed * Time.deltaTime);
            else
                outline.effectColor = Color.Lerp(outline.effectColor, minColor, glowSpeed * Time.deltaTime);

            if (outline.effectColor == maxColor) isIncreasingColor = false;
            if (outline.effectColor == minColor) isIncreasingColor = true;

            // Actualizar el grosor del outline
            if (isIncreasingThickness)
                currentThickness = Mathf.Lerp(currentThickness, maxThickness, pulseSpeed * Time.deltaTime);
            else
                currentThickness = Mathf.Lerp(currentThickness, minThickness, pulseSpeed * Time.deltaTime);

            outline.effectDistance = new Vector2(currentThickness, currentThickness);

            if (Mathf.Abs(currentThickness - maxThickness) < 0.1f) isIncreasingThickness = false;
            if (Mathf.Abs(currentThickness - minThickness) < 0.1f) isIncreasingThickness = true;
        }
    }
}
