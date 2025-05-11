using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvas;
    [SerializeField] private CanvasGroup canvasText1;
    [SerializeField] private CanvasGroup canvasText2;
    [SerializeField] private float fadeoutTime;

    public void StartGameEnding()
    {
        StartCoroutine("Fadeout");
    }

    IEnumerator Fadeout()
    {
        float currentTime = 0;
        while (currentTime < fadeoutTime)
        {
            canvas.alpha = currentTime / fadeoutTime;
            currentTime += Time.deltaTime;
            yield return null;
        }

        currentTime = 0;
        while (currentTime < fadeoutTime / 2)
        {
            canvasText1.alpha = (currentTime / fadeoutTime) * 2;
            currentTime += Time.deltaTime;
            yield return null;
        }

        currentTime = 0;
        while (currentTime < fadeoutTime / 2)
        {
            canvasText2.alpha = (currentTime / fadeoutTime) * 2;
            currentTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(fadeoutTime);
        SceneManager.LoadScene("MainMenu");
    }
}
