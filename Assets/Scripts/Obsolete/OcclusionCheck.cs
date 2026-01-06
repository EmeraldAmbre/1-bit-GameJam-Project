using UnityEngine;

public class OcclusionCheck : MonoBehaviour
{
    public Transform player;
    public LayerMask wallLayer;

    SpriteRenderer sr;
    bool isInVision = false;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
    }

    void Update()
    {
        if (isInVision)
        {
            CheckOcclusion();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collision detected");
        if (!other.CompareTag("PlayerVision")) return;

        isInVision = true;
        CheckOcclusion();

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("PlayerVision")) return;

        isInVision = false;
        sr.enabled = false;
    }

    void CheckOcclusion()
    {
        Debug.Log("occlusionCheck");
        Vector2 dir = (Vector2)transform.position - (Vector2)player.position;
        float dist = dir.magnitude;

        RaycastHit2D hit = Physics2D.Raycast(
            player.position,
            dir.normalized,
            dist,
            wallLayer
        );

        if (hit.collider == null)
        {
            sr.enabled = false;
            return;
        }

        // Visible seulement si le premier hit est cet objet
        sr.enabled = (hit.collider.gameObject == gameObject);
        Debug.Log(hit.collider.gameObject.name);

    }
}
