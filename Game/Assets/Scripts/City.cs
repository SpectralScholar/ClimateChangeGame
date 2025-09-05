using UnityEngine;
using System.Collections.Generic;

public class City : MonoBehaviour
{
    public string city_name = "Default City";
    public int population = 0;
    public float emmissions = 0;

    public float energy_demand = 0;
    public float energy_emission_per_unit = 0;
    public float transportation_demand = 0;
    public float public_transport_rate = 0;
    public float public_transport_emission_per_unit = 0;
    public float manual_transport_rate = 0;
    public float private_transport_emission_per_unit = 0;
    public float waste = 0;
    public float recycle_rate = 0;
    public float waste_emission_per_unit = 0;

    public float sea_level = 0;
    public float temperature = 0;
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

    void ProccessTurn()
    {
        CalculateEmmissions();
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

    float CalculateSectorEmmissions(float demand, float emission_per_unit)
    {
        return demand * emission_per_unit * population;
    }

    float CalculateElectricalEmmissions()
    {
        return CalculateSectorEmmissions(energy_demand, energy_emission_per_unit);
    }

    float CalculateTransportationEmmissions()
    {
        return CalculateSectorEmmissions(transportation_demand-(public_transport_rate+manual_transport_rate)*transportation_demand, private_transport_emission_per_unit) + 
               CalculateSectorEmmissions(public_transport_rate*transportation_demand, public_transport_emission_per_unit);
    }
    
    float CalculateAgriculturalEmmissions()
    {
        return CalculateSectorEmmissions(waste-(recycle_rate * waste), waste_emission_per_unit);
    }
}
