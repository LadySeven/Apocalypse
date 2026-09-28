// ====================== Survivor.cs =========================
/*
 * Represents one individual person in the game's simulation.
 *
 * Currently contains:
 * - Basic identity information.
 * - Basic physical needs.
 * - Current action.
 * - Simple methods for updating needs and performing actions.
 *
 * Future survivor systems may include:
 * - Skills.
 * - Personality.
 * - Traits.
 * - Memories and experiences.
 * - Relationships.
 * - Goals and motivations.
 * - Injuries and medical conditions.
 * - Inventory and equipment.
 * - Individual story information.
 *
 * As the project grows, complex systems should be separated
 * into their own classes rather than making this class
 * responsible for everything about a survivor.
 */


public class Survivor
{
    public const int MinValue = 0;
    public const int MaxValue = 100;

    public const int HungryThreshold = 40;
    public const int VeryHungryThreshold = 80;
    public const int ThirstyThreshold = 40;
    public const int VeryThirstyThreshold = 80;
    public const int TiredThreshold = 40;

    public string Name;
    public string Occupation;
    
    public int Health;
    public int Hunger;
    public int Thirst;
    public int Energy;
    public int Stress;

    public SurvivorAction CurrentAction;

    // Tracks whether this survivor is currently assigned to an expedition away from the settlement.
    public bool IsOnExpedition = false;

    // Updates the survivor's needs as one in-game hour passes.
    // Future versions may include: Work Type, Activity, Food Consumption, Medical Conditions, Stress, etc.
    public void PassHour()
    {
        Hunger += 5;
        Thirst += 5;
        Energy -= 5;

        if(Hunger > MaxValue) 
        {
            Hunger = MaxValue;
        }

        if(Thirst > MaxValue)
        {
            Thirst = MaxValue;
        }

        if(Energy < MinValue) 
        {
            Energy = MinValue;
        }
    }

    public void Rest()
    {
        Energy += 20;
        if(Energy > MaxValue) 
        {
            Energy = MaxValue;
        }
    }

    public void Work()
    {
        Energy -= 10;

        if(Energy < MinValue) 
        {
            Energy = MinValue;
        }
    }

    public void Eat()
    {
        Hunger -= 30;
        if(Hunger < MinValue) 
        {
            Hunger = MinValue;
        }
    }

    public void Drink()
    {
        Thirst -= 30;
        if(Thirst < MinValue) 
        {
            Thirst = MinValue;
        }
    }

    public bool IsHungry()
    {
        return Hunger >= HungryThreshold;
    }

    public bool IsThirsty()
    {
        return Thirst >= ThirstyThreshold;
    }

    /* Determines whether thirst has reached a severe level.
     *
     * This is not currently used by the basic utility decision
     * system, but may later be used for dehydration effects,
     * emergency behavior, health penalties, or other consequences.
     */
    public bool IsVeryThirsty()
    {
        return Thirst >= VeryThirstyThreshold;
    }

   /* Determines whether hunger has reached a severe level.
    *
    * This is not currently used by the basic utility decision
    * system, but may later be used for starvation effects,
    * emergency behavior, health penalties, or other consequences.
    */
    public bool IsVeryHungry()
    {
        return Hunger >= VeryHungryThreshold;
    }

    public bool IsTired()
    {
        return Energy <= TiredThreshold;
    }

}