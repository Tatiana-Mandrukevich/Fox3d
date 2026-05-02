using UnityEngine;

public class ItemTable : MonoBehaviour
{
    public ItemTableType ItemTableType;

    public void AttachTable(Transform attachPoint)
    {
        transform.SetParent(attachPoint);
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }
}