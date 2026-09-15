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
    public string Name;
    public string Occupation;
    
    public int Health;
    public int Hunger;
    public int Energy;
    public int Stress;

    public SurvivorAction CurrentAction;

    // Updates the survivor's needs as one in-game hour passes.
    // Future versions may include: Work Type, Activity, Food Consumption, Medical Conditions, Stress, etc.
    public void PassHour()
    {
        Hunger += 5;
        Energy -= 5;

        if(Hunger > 100) 
        {
            Hunger = 100;
        }

        if(Energy < 0) 
        {
            Energy = 0;
        }
    }

    public void Rest()
    {
        Energy += 20;
        if(Energy > 100) 
        {
            Energy = 100;
        }
    }

    public void Work()
    {
        Energy -= 10;

        if(Energy < 0) 
        {
            Energy = 0;
        }
    }

    public bool IsHungry()
    {
        return Hunger >= 40;
    }

    /* Determines whether hunger has reached a critical level.
     *
     * Currently this causes the decision maker to select Idle,
     * because the game does not yet have a food/resource system.
     *
     * Later this could instead cause the survivor to seek food.
     */
    public bool IsVeryHungry()
    {
        return Hunger >= 80;
    }

    public bool IsTired()
    {
        return Energy <= 40;
    }

}