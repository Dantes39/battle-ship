using UnityEngine;

public class ShipFactory : MonoBehaviour
{
    [SerializeField] 
    private SerializableDictionary<int, Ship> _configs;
    public Ship Create(int size) 
    {
        var prefab = _configs[size];
        var instance = Instantiate(prefab);
        return instance;
    }
}

