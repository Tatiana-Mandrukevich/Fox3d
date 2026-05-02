using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour, IInteractable
{
    public ItemTableType AttachItemTableType;
    public Transform[] tableItemPoints;
    
    private List<ItemTable> _attachedTransforms = new List<ItemTable>();

    private bool TryToAttach(ItemTable item)
    {
        if (item.ItemTableType == AttachItemTableType)
        {
            return AttachItem(item);
        }
        return false;
    }

    private bool AttachItem(ItemTable item)
    {
        if (_attachedTransforms.Count <= tableItemPoints.Length)
        {
            _attachedTransforms.Add(item);
            item.AttachTable(tableItemPoints[_attachedTransforms.Count - 1]);
            return true;
        }
        return false;
    }

    public void Interact(Interactor interactor)
    {
        Transform currentAttachCollectableItem = interactor.PickUpMechanics.GetCollectableItem;
        if (currentAttachCollectableItem != null)
        {
            if (currentAttachCollectableItem.TryGetComponent<ItemTable>(out var tableItem))
            {
                if (TryToAttach(tableItem))
                {
                    interactor.PickUpMechanics.DeCollect();
                    
                }
            }
        }
    }
}