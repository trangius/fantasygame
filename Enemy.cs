// TODO: skapa fler attacker

// Basklas för alla fiender
class Enemy
{
    // Alla fiender ska ha detta gemensamt:
    public string Name { get; set; }
    public int Health { get; set; }
    public int BaseDamage { get; set; }
    public int Armor { get; set; }

    // Metoder som alla fiender ska ha. Dessa anropas med hjälp av polymorfism:

    // En metod för att ge info information om fienden
    public virtual string? GetInfo()
    {
        return $"{Name} har {Health} hälsa";
    } 

    // Fienden attackerar.
    public virtual int Attack()
    {
        return 0;
    }

    // Fienden försvarar sig.
    public virtual void Defend(int damage)
    {
        int totalDamage = damage - Armor;
        System.Console.WriteLine($"{Name} tar {totalDamage} skada");
        Health -= totalDamage;
    }
}

// En klass för att hålla reda på en lönnmördare
// Denna kan vi sen skapa flera lönnmördare (instanser) av
// Denna ärver av basklassen Enemy
class Assassin : Enemy
{
    // Förutom de egenskaper som finns i basklassen Enemy, har lönnmördaren möjligheten
    // att vara osynlig
    bool isVisible;

    // Konstruktor
    public Assassin(string name)
    {
        Random random = new Random();
        Health = 20 + random.Next(0, 80);
        BaseDamage = 20;
        Armor = 15;
        isVisible = false; // TODO: random på/av???
        Name = name;
    }

    // Ger information om lönnmördaren.
    public override string? GetInfo()
    {
        if(isVisible)
            return base.GetInfo();
        else
            return null;
    }

    // lönnmördaren attackerar olika beroende på om den är synlig eller inte:
    public override int Attack()
    {
        int damage;
        // if it is visible, it will attack
        Random random = new Random();
        
        if(isVisible)
        {
            damage = BaseDamage + random.Next(0, 30);
            Console.WriteLine($"{Name} hugger dig med sin vassa kniv och gör {damage} skada");
            isVisible = false;
            return damage;
        }
        else
        {
            damage = BaseDamage + random.Next(0, 10);
            Console.WriteLine($"någonstans ifrån kommer en pil som ger dig {damage} skada");
            isVisible = true;
            return damage;
        }
    }

    // lönnmördaren försvarar sig men bara om den är synlig, annars tar den ingen skada 
    public override void Defend(int damage)
    {
        if(isVisible)
        {
           base.Defend(damage);
        }
    }
}

// En klass för att hålla reda på en magiker.
// Denna kan vi sen skapa flera magiker (instanser) av
class Mage : Enemy
{
    // Egenskaper
    public int Mana { get; set; }

    // konstruktor för att skapa magikern:
    public Mage(string name)
    {
        BaseDamage = 10;
        Random random = new Random();
        Health = 20 + random.Next(0, 80);
        Mana = 20 + random.Next(0, 80);
        Armor = 5;
        Name = name;
    }
    
    // Magikern attackerar
    // return value - denna metod returnerar ett heltal som är skadan som magikern gör
    public override int Attack()
    {
        int damage; // hur mycket skada ska magikern göra?

        // om magikern har mer än 10 mana, kan den kasta en eldboll
        // TODO: Lägg till fler attacker som vi slumpar emellan
        if(Mana > 10)
        {
            Random random = new Random();
            damage = BaseDamage + random.Next(0, 30);
            Console.WriteLine($"{Name} kastar en eldboll som gör {damage} skada");
            Mana -= 10;
        }
        else // magikern har inte tillräckligt med mana för att kasta en eldboll
        {
            Console.WriteLine($"{Name} har för lite mana för att kunna attackera");
            Mana += 5;
            damage = 0;
        }
        return damage;
    }
}