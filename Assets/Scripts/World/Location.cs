

public class Location
{
    public string Name;
    public LocationType Type;

    public int Food;
    public int Water;
    public int Medicine;
    public int Materials;

    /*
     * A constructor for Location class that initializes the location immediately.
     * This means a location must be created with this basic information and cannot be created without it.
    */
    public Location(
        string name,
        LocationType type,
        int food,
        int water,
        int medicine,
        int materials)
    {
        Name = name;
        Type = type;

        Food = food;
        Water = water;
        Medicine = medicine;
        Materials = materials;
    }

   /*
    * Attempts to remove food from this location.
    *
    * Returns true if the requested amount of food was available and succesfully removed.
    * Returns false if the requested amount of food was not available and no food was removed.
    *
    * Future exploration and scavenging systems can use this method when survivors retrieve food from a location.
    */
    public bool TakeFood(int amount)
    {
        if (amount <= 0 || Food < amount)
        {
            return false;
        }

        Food -= amount;
        return true;
        
    }
}