using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroType : MonoBehaviour
{
    public float delay = 0.04f;
    private string fullText;
    private TMP_Text textComnponent;
    private bool isTyping = true;
    private bool hasSkipped = false;

    // Start is called before the first frame update
    void Start()
    {
        textComnponent = GetComponent<TMP_Text>();
        fullText = textComnponent.text;
        textComnponent.text = "";
        StartCoroutine(PrintText());        
    }

    private IEnumerator PrintText()
    {
        int currentIndex = 0;
        while (currentIndex < fullText.Length)
        {
            if (!isTyping)
            {
                break;
            }
            textComnponent.text += fullText[currentIndex];
            currentIndex++;
            yield return new WaitForSeconds(delay);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            {
                if (hasSkipped)
                {
                    Debug.Log("enter has skipped");
                    SceneManager.LoadScene("Levels");
                }
                else
                {
                    isTyping = false;
                    textComnponent.text = fullText;
                    hasSkipped = true;
                }        
            }
    }
}
