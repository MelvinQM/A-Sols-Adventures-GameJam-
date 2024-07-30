using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    public Texture2D cursor;
    public Vector2 hotspot = Vector2.zero; // Hotspot position within the cursor texture

    void Start()
    {
        Cursor.SetCursor(cursor, hotspot, CursorMode.Auto);
    }
}