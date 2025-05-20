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
        DisableAllEnemyDogs();
        StartCoroutine(Fadeout());
    }

    private void DisableAllEnemyDogs()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.name.StartsWith("Dog"))
            {
                // Stop Rigidbody2D movement
                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                if (rb != null)
                    rb.linearVelocity = Vector2.zero;

                // Disable AI/movement/attack scripts if they exist
                var enemyAI = obj.GetComponent<EnemyController>();
                if (enemyAI != null) enemyAI.enabled = false;

                var attack = obj.GetComponent<EnemyAttack>();
                if (attack != null) attack.enabled = false;
            }
        }
    }

    IEnumerator Fadeout()
    {
        float currentTime = 0;
        while (currentTime <= fadeoutTime)
        {
            canvas.alpha = currentTime / fadeoutTime;
            currentTime += Time.deltaTime;
            yield return null;
        }
        canvas.alpha = 1;

        currentTime = 0;
        while (currentTime <= fadeoutTime / 2)
        {
            canvasText1.alpha = (currentTime / fadeoutTime) * 2;
            currentTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1.5f);

        currentTime = 0;
        while (currentTime <= fadeoutTime / 2)
        {
            canvasText2.alpha = (currentTime / fadeoutTime) * 2;
            currentTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(fadeoutTime);
        SceneManager.LoadScene("MainMenu");
    }
}
