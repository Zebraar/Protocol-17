using UnityEngine;

public class EnergyHandler : MonoBehaviour
{
    private int energy;
    void Start()
    {
        energy = PlayerPrefs.GetInt("Energy", 10);
    }
    public void AddEnergy(int amount)
    {
        energy += amount;
        Save();
    }
    public void RemoveEnergy(int amount)
    {
        energy -= amount;
        Save();
    }
    public int GetEnergy()
    {
        return energy;
    }
    private void Save()
    {
        PlayerPrefs.SetInt("Energy", energy);
        PlayerPrefs.Save();
    }
}
