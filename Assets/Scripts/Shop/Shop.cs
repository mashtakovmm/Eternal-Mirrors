using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject[] itemsPrefabs;
    [SerializeField] GameObject[] itemStands;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnItems()
    {
        foreach (GameObject itemStand in itemStands)
        {
            Vector2 position = itemStand.transform.position;
            int randomIndex = Random.Range(0, itemsPrefabs.Length);
            GameObject selectedItem = itemsPrefabs[randomIndex];
            Instantiate(selectedItem, position, Quaternion.identity, itemStand.transform);
        }
    }

    public void ClearShop()
    {
        foreach (GameObject itemStand in itemStands)
        {
            foreach (Transform child in itemStand.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
