using UnityEngine;

public class CursorHandler : MonoBehaviour
{
    public void ChangeCursor(Texture2D cursor)
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }
}
