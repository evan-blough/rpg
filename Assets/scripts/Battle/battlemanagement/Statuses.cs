public enum Status { Poisoned, Paralyzed, Bleeding, Vulnerable, Berserk, Afraid, Defending, Death }
[System.Serializable]
public class Statuses
{
    public Status status;
    public int expirationTurn;
    public bool canBeCured = true;
    public float accuracy = 1;

    //deep copy creator
    public Statuses(Statuses oldStatus)
    {
        status = oldStatus.status;
        expirationTurn = oldStatus.expirationTurn;
        canBeCured = oldStatus.canBeCured;
        accuracy = oldStatus.accuracy;
    }
    public Statuses(Status status, int expirationTurn, bool canBeCured, float accuracy)
    {
        this.status = status;
        this.expirationTurn = expirationTurn;
        this.canBeCured = canBeCured;
        this.accuracy = accuracy;
    }

    public Statuses(Status status, int expirationTurn)
    {
        this.status = status;
        this.expirationTurn = expirationTurn;
    }

    public static void HandleStatuses(Character character, int turn)
    {
        character.currStatuses.RemoveAll(s => s.expirationTurn == turn);
    }
}
