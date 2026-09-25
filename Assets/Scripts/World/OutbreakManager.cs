// ====================== OutbreakManager.cs =========================
/*
 * Responsible for tracking the general progression of the
 * zombie outbreak.
 *
 * Currently, outbreak severity is determined only by the
 * current in-game day.
 *
 * Temporary progression:
 *
 * Day 1-7:
 * Low
 *
 * Day 8-20:
 * Moderate
 *
 * Day 21+:
 * High
 *
 * This simple implementation allows me to connect world
 * progression to the existing time simulation before adding
 * more complex outbreak mechanics.
 *
 * Future outbreak progression may consider:
 * - Infection spread.
 * - Zombie population.
 * - Human population.
 * - Government response.
 * - Military activity.
 * - Civil order.
 * - Infrastructure failure.
 * - Player and NPC settlement activity.
 * - Major world events.
 *
 * Eventually the outbreak should represent a changing world
 * rather than simply increasing because time passed.
 */


public class OutbreakManager
{
    public OutbreakLevel CurrentLevel = OutbreakLevel.Low;

    public void UpdateOutbreak(int currentDay)
    {
        if (currentDay >= 21)
        {
            CurrentLevel = OutbreakLevel.High; 
        } 
        else if (currentDay >= 8) 
        {
            CurrentLevel = OutbreakLevel.Moderate;
        }
        else
        {
            CurrentLevel = OutbreakLevel.Low;
        }
    }
}