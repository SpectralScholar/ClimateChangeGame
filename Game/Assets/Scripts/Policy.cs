using UnityEngine;

public class Policy : MonoBehaviour
{
    public string policy_name = "Default Policy";
    public string description = "This is a default policy description.";

    public string variable_affected = "population";
    public float magnitude = 0.01f;
    public int months_to_update = 1;
    public int max_months_to_update = 12;

    public string policy_type = "permanent"; // or 'temporary'
    public int months_active = 12;

    void Start()
    {
        // Initialization code here
    }

    // Update is called once per frame
    void Update()
    {
        // Per-frame logic here
    }

    public bool UpdatePolicy()
    {
        months_to_update -= 1;
        if (months_to_update <= 0)
        {
            months_to_update = max_months_to_update;
            return true;
        }
        return false;
    }

}