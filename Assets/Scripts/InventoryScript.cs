using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.TextCore;
using TMPro;
//using TextMeshPro;




public class InventoryScript : MonoBehaviour
{
    public DataBase data;

    public List<ItemInventory> items = new List<ItemInventory>();

    public GameObject gameObjShow;

    public GameObject InventoryMainObj;
    public int maxCount;

    public Camera cam;
    public EventSystem es;

    public int currentID;
    public ItemInventory currentItem;

    public RectTransform movingObj;
    public Vector3 offset;

    public GameObject backGround;
    public Button backpackButton;

    public void Start()
    {
        backpackButton.onClick.AddListener(OpenInventory);

        if (items.Count == 0)
        {
            AddGraphics();
        }

        Debug.Log(maxCount);

        //for (int i = 0; i < maxCount; i++)
        //{
        //    int index = Random.Range(0, data.items.Count);
        //    AddItem(i, data.items[index], Random.Range(1, 99));

        //}
        UpdateInventory();
    }

    public void Update()
    {
        if (currentID != -1)
        {
            MoveObject();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            backGround.SetActive(!backGround.activeSelf);
            if (backGround.activeSelf)
            {
                UpdateInventory();
            }
        }
    }

    void OpenInventory()
    {
        backGround.SetActive(!backGround.activeSelf);
        if (backGround.activeSelf)
        {
            UpdateInventory();
        }
    }

    public void SearchForSameItem(Item item, int count)
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (items[i].id == item.id)
            {
                if (items[i].count < 128)
                {
                    items[i].count += count;

                    if (items[i].count > 128)
                    {
                        count = items[i].count - 128;
                        items[i].count = 128;
                    }
                    else
                    {
                        count = 0;
                        i = maxCount;
                    }
                }
            }
        }
        
        if (count > 0)
        {
            for (int i = 0; i < maxCount; i++)
            {
                if (items[i].id == 0)
                {
                    AddItem(i, item, count);
                    i = maxCount;
                }
            }
        }
    }

    public void AddItem(int id, Item item, int count)
    {

        Debug.Log("AddItem:" + id);
        items[id].id = item.id;
        items[id].count = count;
        items[id].itemGameObj.GetComponent<Image>().sprite = item.sprite;

        if (count > 1 && item.id != 0)
        {
            //items[id].itemGameObj.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = count.ToString();
            GameObject child = items[id].itemGameObj.transform.GetChild(0).gameObject;
            child.GetComponent<TextMeshProUGUI>().text = count.ToString();

        }
        else
        {
            //items[id].itemGameObj.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "";
            GameObject child = items[id].itemGameObj.transform.GetChild(0).gameObject;
            child.GetComponent<TextMeshProUGUI>().text = "";
        }

    }

    public void RemoveItem(int i)
    {
        items[i].id = 0;
        items[i].count = 0;
        items[i].itemGameObj.GetComponent<Image>().sprite = data.items[0].sprite;
        UpdateInventory();
    }

    public void AddInvetoryItem(int id, ItemInventory invItem)
    {
        items[id].id = invItem.id;
        items[id].count = invItem.count;
        items[id].itemGameObj.GetComponent<Image>().sprite = data.items[invItem.id].sprite;

        if (invItem.count > 1 && invItem.id != 0)
        {
            //items[id].itemGameObj.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = invItem.count.ToString();
            GameObject child = items[id].itemGameObj.transform.GetChild(0).gameObject;
            child.GetComponent<TextMeshProUGUI>().text = invItem.count.ToString();
        }
        else
        {
            //items[id].itemGameObj.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "";
            GameObject child = items[id].itemGameObj.transform.GetChild(0).gameObject;
            child.GetComponent<TextMeshProUGUI>().text = "";
        }

    }

    public void AddGraphics()
    {
        for (int i = 0; i < maxCount; i++)
        {
            GameObject newItem = Instantiate(gameObjShow, InventoryMainObj.transform) as GameObject;

            newItem.name = i.ToString();

            ItemInventory ii = new ItemInventory();
            ii.itemGameObj = newItem;

            RectTransform rt = newItem.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button tempButton = newItem.GetComponent<Button>();

            // Получаем компонент deleteButton из иерархии кнопки инвентаря
            Button deleteButton = newItem.transform.Find("DeleteButton").GetComponent<Button>();

            // Подписываемся на событие нажатия на кнопку инвентаря
            tempButton.onClick.AddListener(delegate { SetDeleteButton(tempButton); });

            // Подписываемся на событие нажатия на кнопку deleteButton
            deleteButton.onClick.AddListener(delegate { RemoveItem(int.Parse(newItem.name)); });

            items.Add(ii);
        }
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (items[i].id != 0 && items[i].count > 1)
            {
                //items[i].itemGameObj.GetComponentInChildren<Text>().text = items[i].count.ToString();
                GameObject child = items[i].itemGameObj.transform.GetChild(0).gameObject;
                child.GetComponent<TextMeshProUGUI>().text = items[i].count.ToString();
            }
            else
            {
                //items[i].itemGameObj.GetComponentInChildren<Text>().text = "";
                GameObject child = items[i].itemGameObj.transform.GetChild(0).gameObject;
                child.GetComponent<TextMeshProUGUI>().text = "";
            }

            items[i].itemGameObj.GetComponent<Image>().sprite = data.items[items[i].id].sprite;
        }
    }

    public void SelectObject()
    {
        if (currentID == -1)
        {
            currentID = int.Parse(es.currentSelectedGameObject.name);
            currentItem = CopyInventoryItem(items[currentID]);
            movingObj.gameObject.SetActive(true);
            movingObj.GetComponent<Image>().sprite = data.items[currentItem.id].sprite;

            AddItem(currentID, data.items[0], 0);

        }
        else
        {
            ItemInventory II = items[int.Parse(es.currentSelectedGameObject.name)];

            if (currentItem.id != II.id)
            {
                AddInvetoryItem(currentID, II);
                AddInvetoryItem(int.Parse(es.currentSelectedGameObject.name), currentItem);
            }
            else
            {
                if (II.count + currentItem.count <= 128)
                {
                    II.count += currentItem.count;
                }
                else
                {
                    AddItem(currentID, data.items[II.id], II.count + currentItem.count - 128);
                    II.count = 128;
                }

                GameObject child = II.itemGameObj.transform.GetChild(0).gameObject;
                child.GetComponent<TextMeshProUGUI>().text = II.count > 1 ? II.count.ToString() : "";

                
            }
            
            currentID = -1;

            movingObj.gameObject.SetActive(false);
        }
    }

    public void SetDeleteButton(Button button)
    {
        var isActieve = button.transform.Find("DeleteButton").gameObject.activeSelf;
        button.transform.Find("DeleteButton").gameObject.SetActive(!isActieve);
    }


    public void MoveObject()
    {
        Vector3 pos = Input.mousePosition + offset;
        pos.z = InventoryMainObj.GetComponent<RectTransform>().position.z;
        movingObj.position = cam.ScreenToWorldPoint(pos);
    }

    public ItemInventory CopyInventoryItem(ItemInventory old)
    {
        ItemInventory newII = new ItemInventory();
        newII.id = old.id;
        newII.itemGameObj = old.itemGameObj;
        newII.count = old.count;
        return newII;
    }

}

[System.Serializable]

public class ItemInventory
{
    public int id;
    public GameObject itemGameObj;
    public int count;
}
