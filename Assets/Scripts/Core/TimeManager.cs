// ====================== TimeManager.cs =========================
/*
 * Responsible for controlling the simulation's in-game time.
 *
 * Currently:
 * - Tracks the current day and hour.
 * - Allows other systems to advance time.
 * - Notifies subscribed systems whenever an hour passes.
 *
 * Future responsibilities may include:
 * - Day/night progression.
 * - Scheduled events.
 * - Connecting time to the outbreak system.
 * - Triggering daily world simulation.
 * - Supporting different time speeds.
 *
 * This class should focus only on TIME.
 * Other systems should react to time rather than being controlled directly here.
 */

using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public int currentDay = 1;
    public int currentHour = 8;
    // Event to notify  whenever one in-game hour has passed.
    // Other systems can subscribe to this event to react to time changes.
    public event System.Action OnHourPassed;

    void Start()
    {
        Debug.Log("The simulation has started.");
        Debug.Log("Day " + currentDay + ", Hour " + currentHour);
    }
    /*
     * Advances the in-game time by the specified number of hours.
     * If the hour exceeds 23, it wraps around to 0 and increments the day.
     * After advancing time, OnHourPassed event is triggered so other systems can update.
     */
    public void AdvanceHours(int hours)
    {
        for(int i = 0; i < hours; i++)
        {
            currentHour++;

            if(currentHour >= 24)
            {
                currentHour = 0;
                currentDay++;
            }

            OnHourPassed?.Invoke();
        }
        
        Debug.Log("Time advanced by " + hours + " hours.");
        Debug.Log("Day " + currentDay + ", Hour " + currentHour);
    }
}
