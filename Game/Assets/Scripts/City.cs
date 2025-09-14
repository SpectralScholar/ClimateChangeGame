using UnityEngine;
using UnityEngine.InputSystem;
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

    public float industry_production = 0;
    public float industry_emission_per_unit = 0;

    public float sea_level = 0;
    public float temperature = 0;
    public Dictionary<string, int> risks = new Dictionary<string, int>();
    public List<Policy> policies = new List<Policy>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ProccessTurn();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            ProccessTurn();
        }
    }

    public void ProccessTurn()
    {
        CalculateEmmissions();
        Update_Policies();
    }

    void CalculateEmmissions()
    {
        emmissions = 0;
        emmissions += CalculateElectricalEmmissions();
        emmissions += CalculateTransportationEmmissions();
        emmissions += CalculateAgriculturalEmmissions();
        emmissions += CalculateIndustrialEmmissions();

        Debug.Log("electrical emmissions: " + CalculateElectricalEmmissions());
        Debug.Log("transportation emmissions: " + CalculateTransportationEmmissions());
        Debug.Log("agricultural emmissions: " + CalculateAgriculturalEmmissions());
        Debug.Log("industrial emmissions: " + CalculateIndustrialEmmissions());
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
        return CalculateSectorEmmissions(transportation_demand - (public_transport_rate + manual_transport_rate) * transportation_demand, private_transport_emission_per_unit) +
               CalculateSectorEmmissions(public_transport_rate * transportation_demand, public_transport_emission_per_unit);
    }

    float CalculateAgriculturalEmmissions()
    {
        return CalculateSectorEmmissions(waste - (recycle_rate * waste), waste_emission_per_unit);
    }

    float CalculateIndustrialEmmissions()
    {
        return industry_production * industry_emission_per_unit;
    }

    void Update_Policies()
    {
        foreach (Policy policy in policies)
        {
            switch (policy.variable_affected)
            {
                case "population":
                    population = (int)(population * (1 + policy.magnitude));
                    break;
                case "energy_demand":
                    energy_demand *= 1 + policy.magnitude;
                    break;
                case "energy_emission_per_unit":
                    energy_emission_per_unit *= 1 + policy.magnitude;
                    break;
                case "transportation_demand":
                    transportation_demand *= 1 + policy.magnitude;
                    break;
                case "public_transport_rate":
                    public_transport_rate *= 1 + policy.magnitude;
                    break;
                case "public_transport_emission_per_unit":
                    public_transport_emission_per_unit *= 1 + policy.magnitude;
                    break;
                case "manual_transport_rate":
                    manual_transport_rate *= 1 + policy.magnitude;
                    break;
                case "private_transport_emission_per_unit":
                    private_transport_emission_per_unit *= 1 + policy.magnitude;
                    break;
                case "waste":
                    waste *= 1 + policy.magnitude;
                    break;
                case "recycle_rate":
                    recycle_rate = Mathf.Min(recycle_rate*(1 + policy.magnitude), 1.0f);
                    break;
                case "waste_emission_per_unit":
                    waste_emission_per_unit *= 1 + policy.magnitude;
                    break;
                case "industry_production":
                    industry_production *= 1 + policy.magnitude;
                    break;
                case "industry_emission_per_unit":
                    industry_emission_per_unit *= 1 + policy.magnitude;
                    break;
                default:
                    Debug.Log("Unknown variable affected: " + policy.variable_affected);
                    break;
            }
            if (policy.policy_type == "temporary")
            {
                policy.months_active -= 1;
                if (policy.months_active <= 0)
                {
                    // Remove policy effects
                    policies.Remove(policy);
                }
            }
        }
    }
    

}
