// ====================== SurvivorAction.cs =========================
/* Defines the actions a survivor can currently perform.
 *
 * This enum is used by the survivor decision system to describe
 * what a survivor has decided to do.
 *
 * As the game grows, new actions can be added when their
 * corresponding systems are implemented.
 */

public enum SurvivorAction
{
    Idle,
    Rest,
    Work,
    Explore,
    Eat,
}