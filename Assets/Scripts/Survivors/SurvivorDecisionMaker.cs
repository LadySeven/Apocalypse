// ====================== SurvivorDecisionMaker.cs =========================

/*
 * Responsible for deciding what a survivor should do based
 * on their current state and circumstances.
 *
 * Currently uses a basic utility-style decision system.
 *
 * The survivor compares the urgency of:
 * - Eating.
 * - Drinking.
 * - Resting.
 *
 * The valid action with the highest utility score is selected.
 *
 * This class decides WHAT the survivor wants to do.
 * It does not perform the action itself.
 *
 * Future decision-making may consider:
 * - Health and injuries.
 * - Stress.
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
 * Eventually each possible action may calculate its own
 * utility score using several parts of the simulation.
 */
public class SurvivorDecisionMaker
{
    /*
     * Evaluates the survivor's current needs and returns
     * a SurvivorDecision containing:
     *
     * - The selected action.
     * - Eat utility score.
     * - Drink utility score.
     * - Rest utility score.
     */
    public SurvivorDecision DecideAction(
        Survivor survivor,
        ResourceManager resourceManager)
    {
        // 1. Calculate the initial utility score for each need.
        int eatScore = survivor.Hunger;
        int drinkScore = survivor.Thirst;
        int restScore = Survivor.MaxValue - survivor.Energy;

        /*
         * 2. Check resource availability.
         *
         * Eating cannot currently be selected without food.
         * Drinking cannot currently be selected without water.
         */
        if (resourceManager.Food <= 0)
        {
            eatScore = 0;
        }

        if (resourceManager.Water <= 0)
        {
            drinkScore = 0;
        }

        /*
         * 3. Ignore needs that have not reached their
         * action threshold.
         */
        if (!survivor.IsHungry())
        {
            eatScore = 0;
        }

        if (!survivor.IsThirsty())
        {
            drinkScore = 0;
        }

        if (!survivor.IsTired())
        {
            restScore = 0;
        }

        /*
         * 4. Create a SurvivorDecision object and store
         * the calculated utility scores.
         */
        SurvivorDecision decision = new SurvivorDecision();

        decision.EatScore = eatScore;
        decision.DrinkScore = drinkScore;
        decision.RestScore = restScore;

        /*
         * 5. Compare the valid utility scores.
         *
         * The action with the highest score is selected.
         *
         * Eat currently wins an exact tie because it is
         * checked first using >=.
         */
        if (eatScore >= drinkScore &&
            eatScore >= restScore &&
            eatScore > 0)
        {
            decision.SelectedAction = SurvivorAction.Eat;
        }
        else if (drinkScore >= eatScore &&
                 drinkScore >= restScore &&
                 drinkScore > 0)
        {
            decision.SelectedAction = SurvivorAction.Drink;
        }
        else if (restScore >= eatScore &&
                 restScore >= drinkScore &&
                 restScore > 0)
        {
            decision.SelectedAction = SurvivorAction.Rest;
        }
        else
        {
            decision.SelectedAction = SurvivorAction.Work;
        }

        // 6. Return the complete decision.
        return decision;
    }
}