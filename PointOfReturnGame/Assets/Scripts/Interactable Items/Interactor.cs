using TMPro;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionRadius;
    [SerializeField] private LayerMask interactionMask;

    private Collider2D collider;
    [SerializeField] private TextMeshProUGUI prompt;
    void Update()
    {
        collider = Physics2D.OverlapCircle(interactionPoint.position, interactionRadius, interactionMask);
        if (collider != null)
        {
            var interactable = collider.GetComponent<IInteractable>();
            prompt.gameObject.SetActive(true);
            prompt.SetText(interactable.InteractionPrompt);
            if (interactable != null && Input.GetKeyDown(KeyCode.F))
            {
                interactable.Interact(this);
            }
        }
        else
        {
            prompt.gameObject.SetActive(false);
        }
    }

    // Show pick-up radius
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(interactionPoint.position, interactionRadius);
    //}
}
