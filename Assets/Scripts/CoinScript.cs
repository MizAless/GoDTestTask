using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("123123123");
        if (collision.CompareTag("Player"))
        {
            DataBase db = FindObjectOfType<DataBase>();
            InventoryScript inventory = FindObjectOfType<InventoryScript>();
            inventory.SearchForSameItem(db.items[2], 1);
            inventory.UpdateInventory();
            //inventory.AddItem(0,db.items[2],1);

            Destroy(gameObject);
        }
    }
}