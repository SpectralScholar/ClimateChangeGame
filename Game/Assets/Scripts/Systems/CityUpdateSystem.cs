using UnityEngine;

public class CityUpdateSystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void ProccessTurn()
    {
        GameObject[] cities = GameObject.FindGameObjectsWithTag("City");
        foreach (GameObject obj in cities)
        {
            City city = obj.GetComponent<City>();
            if (city != null)
            {
                city.ProccessTurn();
            }
            else
            {
                Debug.LogWarning("City component not found on object: " + obj.name);
            }
            Debug.Log("Found object: " + obj.name);
        }
    }
}
