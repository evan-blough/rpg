using System.Collections.Generic;

public class Attack
{
    public int damage;
    public bool isCritical;
    public bool isMagic;
    public List<Statuses> attackStatuses;
    public List<Elements> attackElements;
    public List<Status> statusToRemove;
    public int turnCounter = 0;

    public Attack(int damage, bool isCritical, bool isMagic)
    {
        this.damage = damage;
        this.isCritical = isCritical;
        this.isMagic = isMagic;
        this.attackStatuses = new List<Statuses>();
        this.attackElements = new List<Elements>();
        this.statusToRemove = new List<Status>();
        this.turnCounter = 0;
    }

    public Attack(int damage, bool isCritical, bool isMagic, List<Statuses> attackStatuses, List<Elements> attackElements, int turnCounter)
    {
        this.damage = damage;
        this.isCritical = isCritical;
        this.isMagic = isMagic;
        this.attackStatuses = attackStatuses;
        this.attackElements = attackElements;
        this.turnCounter = turnCounter;
        this.statusToRemove = new List<Status>();
    }

    public Attack(int damage, bool isCritical, bool isMagic, List<Statuses> attackStatuses, List<Elements> attackElements, List<Status> statusToRemove, int turnCounter) : this(damage, isCritical, isMagic)
    {
        this.damage = damage;
        this.isCritical = isCritical;
        this.isMagic = isMagic;
        this.attackStatuses = attackStatuses;
        this.attackElements = attackElements;
        this.statusToRemove = statusToRemove;
        this.turnCounter = turnCounter;
    }
}
