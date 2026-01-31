using UnityEngine;
using UnityEngine.UI;

public class AnswerMovement : MonoBehaviour
{
    private Vector2 smoothVelocity;

    public RectTransform buttonRect;
    public float speedX = 200f;
    public float speedY = 150f;

    [Header("Hectic Movement")]
    public float jitterStrength = 20f;
    public float jitterSpeed = 1.5f;

    [Header("Movement Smoothing")]
    public float movementSmoothTime = 1.5f; // higher = slower
    public float maxSpeed = 100f;            // absolute cap (pixels/sec)

    private Vector2 velocity;

    [Header("Scaling")]
    public float scaleChangeInterval = 1.2f;
    public Vector2 scaleRange = new Vector2(0.8f, 1.3f);
    public float scaleLerpSpeed = 2f;

    [Header("Rotation")]
    public float rotationSpeed = 20f;

    private Vector2 direction;
    private RectTransform canvasRect;

    private float scaleTimer;
    private Vector3 targetScale;

    private float noiseOffsetX;
    private float noiseOffsetY;


    public void RandomValues(int multiply)
    {
        rotationSpeed+=Random.Range(-10f, 15f*multiply+1);
        speedX+=Random.Range(-23f, 80f*multiply+3);
        speedY+=Random.Range(-19f, 52f * multiply + 3);
        jitterStrength+=Random.Range(-2f, 5f * multiply + 1);
        maxSpeed+=Random.Range(-20f, 80f * multiply + 1);
        scaleRange+=new Vector2(Random.Range(-0.2f * multiply + 1, 0.2f ), Random.Range(-0.2f * multiply + 1, 0.2f ));
    }
    void Start()
    {
        canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        direction = new Vector2(Random.value > 0.5f ? 1 : -1,
                                Random.value > 0.5f ? 1 : -1);

        targetScale = buttonRect.localScale;

        noiseOffsetX = Random.Range(0f, 100f);
        noiseOffsetY = Random.Range(0f, 100f);
    }

    void Update()
    {
        if (!buttonRect || !canvasRect) return;
        // Desired direction-based velocity (very small on purpose)
        Vector2 targetVelocity = new Vector2(
        direction.x * speedX,
        direction.y * speedY
    );

        velocity = Vector2.SmoothDamp(
            velocity,
            targetVelocity,
            ref smoothVelocity,
            movementSmoothTime,
            maxSpeed
        );

        Vector2 pos = buttonRect.anchoredPosition;
        pos += velocity * Time.deltaTime;

        // Subtle jitter
        float jitterX = (Mathf.PerlinNoise(Time.time * jitterSpeed, noiseOffsetX) - 0.5f) * jitterStrength * 0.2f;
        float jitterY = (Mathf.PerlinNoise(Time.time * jitterSpeed, noiseOffsetY) - 0.5f) * jitterStrength * 0.2f;
        pos += new Vector2(jitterX, jitterY);

        buttonRect.anchoredPosition = pos;


        scaleTimer += Time.deltaTime;
        if (scaleTimer >= scaleChangeInterval)
        {
            scaleTimer = 0f;
            float s = Random.Range(scaleRange.x, scaleRange.y);
            targetScale = Vector3.one * s;
        }

        buttonRect.localScale = Vector3.Lerp(
            buttonRect.localScale,
            targetScale,
            Time.deltaTime * scaleLerpSpeed
        );

        buttonRect.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

        ClampToCanvas();
    }

    void ClampToCanvas()
    {
        Vector2 pos = buttonRect.anchoredPosition;

        // Canvas half size
        Vector2 canvasHalfSize = canvasRect.rect.size * 0.5f;

        // Button half size (including scale)
        Vector2 buttonHalfSize = Vector2.Scale(
            buttonRect.rect.size * 0.5f,
            buttonRect.localScale
        );

        float minX = -canvasHalfSize.x + buttonHalfSize.x;
        float maxX = canvasHalfSize.x - buttonHalfSize.x;
        float minY = -canvasHalfSize.y + buttonHalfSize.y;
        float maxY = canvasHalfSize.y - buttonHalfSize.y;

        bool hitX = false;
        bool hitY = false;

        if (pos.x < minX)
        {
            pos.x = minX;
            hitX = true;
        }
        else if (pos.x > maxX)
        {
            pos.x = maxX;
            hitX = true;
        }

        if (pos.y < minY)
        {
            pos.y = minY;
            hitY = true;
        }
        else if (pos.y > maxY)
        {
            pos.y = maxY;
            hitY = true;
        }

        if (hitX)
        {
            direction.x *= -1;
            velocity.x = 0;
            smoothVelocity.x = 0;
        }

        if (hitY)
        {
            direction.y *= -1;
            velocity.y = 0;
            smoothVelocity.y = 0;
        }

        buttonRect.anchoredPosition = pos;
    }


}
