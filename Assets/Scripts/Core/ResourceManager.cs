

public class ResourceManager
{
    public int Food = 20;
    public int Water = 30;
    public int Medicine = 5;
    public int Materials = 15;
   /* Attempts to consume the requested amount of food
    * Returns true if enough food was available and consumed,
    * Returns false if not enough food was available.
    */
    public bool ConsumeFood(int amount)
    {
        if (Food < amount)
        {
            return false;
        }
        
        Food -= amount;
        return true;
    }

    public bool ConsumeWater(int amount)
    {
        if (Water < amount)
        {
            return false;
        }
        
        Water -= amount;
        return true;
    }

}