using UnityEngine;

public class AmmoScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DataBase db = FindObjectOfType<DataBase>();
            InventoryScript inventory = FindObjectOfType<InventoryScript>();
            inventory.SearchForSameItem(db.items[1], 25);
            inventory.UpdateInventory();

            Destroy(gameObject);
        }
    }
}