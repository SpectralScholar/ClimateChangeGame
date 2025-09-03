using UnityEngine;
using System.Collections.Generic;

public class City : MonoBehaviour
{
    public int population = 0;
    public int emmissions = 0;

    public int energy_demand = 0;
    public int energy_emission_per_unit = 0;
    public int transportation_demand = 0;
    public int transportation_emission_per_unit = 0;
    public int agricultural_demand = 0;
    public int agricultural_emission_per_unit = 0;

    public int sea_level = 0;
    public int temperature = 0;
    public Dictionary<string, int> risks = new Dictionary<string, int>();
    public List<Policy> policies = new List<Policy>();
    public List<Industry> industries = new List<Industry>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void CalculateEmmissions()
    {
        emmissions = 0;
        foreach (Industry industry in industries)
        {
            emmissions += industry.emmissions;
        }
        emmissions += CalculateElectricalEmmissions();
        emmissions += CalculateTransportationEmmissions();
        emmissions += CalculateAgriculturalEmmissions();
    }

    int CalculateSectorEmmissions(int demand, int emission_per_unit)
    {
        return demand * emission_per_unit;
    }

    int CalculateElectricalEmmissions()
    {
        return CalculateSectorEmmissions(energy_demand, energy_emission_per_unit);
    }

    int CalculateTransportationEmmissions()
    {
        return CalculateSectorEmmissions(transportation_demand, transportation_emission_per_unit);
    }
    
    int CalculateAgriculturalEmmissions()
    {
        return CalculateSectorEmmissions(agricultural_demand, agricultural_emission_per_unit);
    }
}
