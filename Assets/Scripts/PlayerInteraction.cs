using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public float interactRange = 10f;

    [Header("UI Bars")]
    public Slider progressSlider;
    public Slider stressSlider;

    void Update()
    {
        
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, interactRange))
            {
                if (hit.collider.CompareTag("Sacrifice"))
                {
                    
                    Destroy(hit.collider.gameObject);

                    
                    if (progressSlider != null) progressSlider.value += 0.2f;
                    if (stressSlider != null) stressSlider.value += 0.3f;

                    Debug.Log("Sacrificed! Progress & Stress increased.");
                }
            }
        }
    }
}