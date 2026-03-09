using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerInteraction : MonoBehaviour
{
    public float interactRange = 10f;

    [Header("UI Elements")]
    public Slider progressSlider;
    public Slider stressSlider;
    public GameObject tutorialText;
    public TMP_Text warningText;

    private bool gameHasEnded = false;

    void Start()
    {
        if (warningText != null) warningText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (gameHasEnded) return;

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (tutorialText != null) tutorialText.SetActive(false);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, interactRange))
            {
                if (hit.collider.CompareTag("PC"))
                {
                    if (stressSlider != null && stressSlider.value >= 1f)
                    {
                        ShowMessage("BURNOUT! SACRIFICE AN OBJECT!", Color.red);
                    }
                    else
                    {
                        if (progressSlider != null) progressSlider.value += 0.05f;
                        if (stressSlider != null) stressSlider.value += 0.15f;

                        if (progressSlider != null && progressSlider.value >= 1f)
                        {
                            WinGame();
                            return;
                        }

                        if (stressSlider != null && stressSlider.value >= 1f)
                        {
                            ShowMessage("BURNOUT! SACRIFICE AN OBJECT!", Color.red);
                        }
                    }
                }
                else if (hit.collider.CompareTag("Sacrifice"))
                {
                    string itemName = hit.collider.gameObject.name.ToLower();
                    string consequenceText = "";

                    if (itemName.Contains("bed")) consequenceText = "LOST: REST.\nSleep is a memory.";
                    else if (itemName.Contains("window")) consequenceText = "LOST: TIME.\nThe sun is gone.";
                    else if (itemName.Contains("door")) consequenceText = "LOST: FREEDOM.\nYou belong to the room.";
                    else consequenceText = "LOST: " + hit.collider.gameObject.name.ToUpper() + ".\nA piece of sanity, gone.";

                    Destroy(hit.collider.gameObject);
                    if (stressSlider != null) stressSlider.value -= 0.80f;
                    ShowMessage(consequenceText, Color.gray);
                }
            }
        }
    }

    void WinGame()
    {
        gameHasEnded = true;

        
        GameObject player = GameObject.Find("Player");

        if (player != null)
        {
            
            MonoBehaviour[] allScripts = player.GetComponentsInChildren<MonoBehaviour>();
            foreach (MonoBehaviour script in allScripts)
            {
                script.enabled = false;
            }
        }

        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        
        StartCoroutine(TypeWinMessage("GAME OVER\n\nDreams come with a cost..."));
    }

    void ShowMessage(string message, Color textColor)
    {
        if (warningText != null)
        {
            warningText.text = message;
            warningText.color = textColor;
            warningText.gameObject.SetActive(true);
        }
    }

    IEnumerator TypeWinMessage(string message)
    {
        if (warningText != null)
        {
            warningText.text = "";
            warningText.color = Color.white;
            warningText.gameObject.SetActive(true);

            foreach (char letter in message.ToCharArray())
            {
                warningText.text += letter;
                yield return new WaitForSeconds(0.06f);
            }
        }
        
    }
}