using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject startScreenPanel;
    public GameObject tutorialScreenPanel;

    [Header("Typewriter Settings")]
    public TMP_Text typewriterText;
    [TextArea(10, 20)]
    public string message = "The deadline is tomorrow. You are out of time.\nWork at your PC to progress, but watch your stress.\nSacrifice your belongings to stay sane.";
    public float typingSpeed = 0.04f;

    void Start()
    {
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        startScreenPanel.SetActive(true);
        tutorialScreenPanel.SetActive(false);
    }

    
    public void OnStartButtonClicked()
    {
        startScreenPanel.SetActive(false);
        tutorialScreenPanel.SetActive(true);
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        typewriterText.text = "";
        foreach (char letter in message.ToCharArray())
        {
            typewriterText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    
    public void OnContinueButtonClicked()
    {
        
        SceneManager.LoadScene(1);
    }
}