using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class FootstepSystem : MonoBehaviour
{
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private AudioSource audioSource;

    [System.Serializable]
    public class TileSound
    {
        public List<TileBase> tiles;
        public AudioClip[] footstepClips;
    }

    [SerializeField] private TileSound[] tileSounds;

    [SerializeField] private float footstepInterval = 0.4f;

    private float footstepTimer;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rb.linearVelocity.magnitude > 0.1f)
        {
            footstepTimer -= Time.deltaTime;

            if (footstepTimer <= 0f)
            {
                PlayFootstep();
                footstepTimer = footstepInterval;
            }
        }
        else
        {
            footstepTimer = 0f;
        }
    }

    private void PlayFootstep()
    {
        Vector3Int gridPos = groundTilemap.WorldToCell(transform.position);
        TileBase currentTile = groundTilemap.GetTile(gridPos);

        foreach (var tileSound in tileSounds)
        {
            if (tileSound.tiles.Contains(currentTile))
            {
                if (tileSound.footstepClips.Length > 0)
                {
                    int rand = Random.Range(0, tileSound.footstepClips.Length);
                    audioSource.PlayOneShot(tileSound.footstepClips[rand]);
                }
                return;
            }
        }
    }
}
