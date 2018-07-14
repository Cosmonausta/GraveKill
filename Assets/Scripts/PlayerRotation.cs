using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private Vector2 playerPositionOnScreen;

    private Vector2 mouseOnScreen;

    private float angle;

    void Update()
    {
        playerPositionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        angle = AngleBetweenTwoPoints(playerPositionOnScreen, mouseOnScreen);

        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return (Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg) + 90;
    }
}
