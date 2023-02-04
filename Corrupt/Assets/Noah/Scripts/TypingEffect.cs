using System.Collections;
using UnityEngine;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    public float delay = 0.04f;
    private string fullText;
    private TMP_Text textComnponent;
    private bool isTyping = true;

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
        foreach (char letter in fullText)
        {
            if (!isTyping)
            {
                break;
            }

            textComnponent.text += letter;
            if (Input.GetKey(KeyCode.Return))
            {
                isTyping = false;
                textComnponent.text = fullText;
                yield break;
            }
            yield return new WaitForSeconds(delay);
        }
    }
}
