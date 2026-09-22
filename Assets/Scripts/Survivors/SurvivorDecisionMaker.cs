// ====================== SurvivorDecisionMaker.cs =========================
/* Responsible for deciding what a survivor should do based
 * on their current state and circumstances.
 *
 * Currently:
 * - Very high hunger causes Idle.
 * - Tired survivors choose Rest.
 * - Otherwise the survivor chooses Work.
 *
 * This class decides WHAT the survivor wants to do.
 * It does not perform the action itself.
 *
 * Future decision-making may consider:
 * - Hunger and thirst.
 * - Energy.
 * - Health and injuries.
 * - Personality.
 * - Skills.
 * - Memories.
 * - Relationships.
 * - Personal goals.
 * - Player instructions.
 * - Available resources.
 * - Current threats.
 * - Risk tolerance.
 *
 * Eventually this can evolve into a Utility AI system where
 * possible actions are scored and the survivor chooses an
 * appropriate action based on the current situation.
 */

public class SurvivorDecisionMaker
{
    /* Determines which action the survivor should currently take.
     *
     * The checks are performed in priority order.
     *
     * Currently:
     * 1. Critical hunger.
     * 2. Tiredness.
     * 3. Otherwise work.
     */
    public SurvivorAction DecideAction(Survivor survivor, ResourceManager resourceManager)
    {   
        if (survivor.IsVeryThirsty() && resourceManager.Water > 0)
        {
            return SurvivorAction.Drink;
        }
        if (survivor.IsVeryHungry() && resourceManager.Food > 0)
        {
            return SurvivorAction.Eat;
        }

        if (survivor.IsTired())
        {
            return SurvivorAction.Rest;
        }
        
        return SurvivorAction.Work;
    }
}