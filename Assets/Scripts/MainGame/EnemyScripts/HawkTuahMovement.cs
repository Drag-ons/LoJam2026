using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class HawkTuahMovement : MonoBehaviour, IEnemyMovement
{
    public float progress = 0f;
    public float arcHeight;
    public float duration;
    public GameObject player;
    public bool isDoneArc = false;

    private float offCameraOffset = 10;
    private bool hasMoved = false;
    private Vector2 start;
    private Vector2 end;
    private EnemyMovement enemyMovement;
    private EnemyAnimation enemyAnimation;
    private bool spawnedTop;
    private bool spawnedRight;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        start = transform.position;

        Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        spawnedTop = screenPoint.y > Screen.height / 2f;
        spawnedRight = screenPoint.x > Screen.width / 2f;

        if (spawnedTop)
        {
            arcHeight = -arcHeight;
        }

        if (spawnedTop && spawnedRight)
        {
            end = Camera.main.ScreenToWorldPoint(new Vector3(-offCameraOffset, Screen.height + offCameraOffset, Camera.main.nearClipPlane));
        }
        else if (spawnedTop && !spawnedRight)
        {
            end = Camera.main.ScreenToWorldPoint(new Vector3(-offCameraOffset, -offCameraOffset, Camera.main.nearClipPlane));
        }
        else if (!spawnedTop && spawnedRight)
        {
            end = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + offCameraOffset, Screen.height + offCameraOffset, Camera.main.nearClipPlane));
        }
        else if (!spawnedTop && !spawnedRight)
        {
            end = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + offCameraOffset, -offCameraOffset, Camera.main.nearClipPlane));
        }

        enemyMovement = GetComponent<EnemyMovement>();
        enemyAnimation = GetComponent<EnemyAnimation>();
        enemyAnimation.OnSpawnEnd();
    }

    public void Move()
    {
        if (enemyMovement.canMove && !hasMoved)
        {
            StartCoroutine(FollowArc());
        }

        if (isDoneArc)
        {
            enemyMovement.rigidBody.linearVelocity = (transform.position - player.transform.position).normalized * Random.Range(enemyMovement.enemyStats.minimumSpeed, enemyMovement.enemyStats.maximumSpeed);
        }
    }

    public IEnumerator FollowArc()
    {
        hasMoved = true;
        float progress = 0f;
        while (progress < 1f)
        {
            Vector3 nextX = Vector3.Lerp(start, end, progress);

            float arc = arcHeight * (1f - Mathf.Pow(2 * progress - 1, 2));
            if ((spawnedTop && !spawnedRight) || (!spawnedTop && spawnedRight))
            {
                transform.position = new Vector3(nextX.x - arc, nextX.y);
            }
            else
            {
                transform.position = new Vector3(nextX.x, nextX.y + arc);
            }

            progress += Time.deltaTime / duration;
            yield return null;
        }
        transform.position = end;
        isDoneArc = true;
    }
}
