using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float timeBetweenChars;

    public Image imageToFade;
    public float timeToFade = 2f;

    string[] intro_text = {
        "...",
        "Welcome, Sol.....",
        "You are but a flicker, a mere spark in existence. Yet within you lies potential. to weave the threads of the universe itself.",
        "In this realm we follow the triad of Soul, Body, and Spirit. Each part is a key to a whole.",
        "The Soul yearns for unity. The Body for form and the Spirit for purpose.",
        "On this journey you will find many alchemical powers to help you on your quest. But...",
        "You will also encounter many shadowy beings. These beings live to prevent you from becoming whole.",
        "Be carefull Sol.... Good luck."
    };
    private bool isTyping = false;
    int length;
    private int sentenceIndex = 1;

    void Start()
    {
        length = intro_text.Length;
        if(text == null)
        {
            Debug.LogWarning("No text object found on DialogueManager");
        }

        text.text = intro_text[0];
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(!isTyping)
            {
                if(sentenceIndex == length)
                {
                    StartCoroutine(FadeOut());
                } else {
                    StartCoroutine(TypeText());
                }

            }

        }
    }

    private IEnumerator TypeText()
    {
        // Clear the text before starting
        isTyping = true;
        text.text = "";
        foreach (char letter in intro_text[sentenceIndex])
        {
            text.text += letter;
            yield return new WaitForSeconds(timeBetweenChars);
        }
        isTyping = false;
        sentenceIndex++;
    }

    private IEnumerator FadeOut()
    {
        Color originalColor = imageToFade.color;
        float fadeSpeed = 1f / timeToFade;

        for (float t = 0; t < 1; t += Time.deltaTime * fadeSpeed)
        {
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(0, 1, t));
            imageToFade.color = newColor;
            yield return null;
        }

        // Ensure the image is fully opaque
        imageToFade.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);

        // Load the next scene in the build index
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
