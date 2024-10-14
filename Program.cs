// TODO: skapa fler fiender och fler attacker
// TODO: shield

// En klass för att hålla reda på en magiker.
// Denna kan vi sen skapa flera magiker (instanser) av
using System.Runtime.CompilerServices;

class Mage
{
    // Egenskaper
    public string Name { get; set; }
    public int Health { get; set; }
    public int Mana { get; set; }

    // konstruktor för att skapa magikern:
    public Mage(string name)
    {
        Random random = new Random();
        Health = 20 + random.Next(0, 80);
        Mana = 20 + random.Next(0, 80);
        Name = name;
    }
    
    // Magikern attackerar
    // return value - denna metod returnerar ett heltal som är skadan som magikern gör
    public int Attack()
    {
        int damage; // hur mycket skada ska magikern göra?

        // om magikern har mer än 10 mana, kan den kasta en eldboll
        // TODO: Lägg till fler attacker som vi slumpar emellan
        if(Mana > 10)
        {
            Random random = new Random();
            damage = 10 + random.Next(0, 30);
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

// Huvudprogrammet
static class Program
{
    static void Main(string[] args)
    {
        int playerHealth = 500; // sätt spelarens starthälsa

        // skapa en lista med magiker (fiender)
        // TODO: denna lista ska innehålla alla möjliga typer av fiender
        List<Mage> mages = new List<Mage>();
        mages.Add(new Mage("Gandalf"));
        mages.Add(new Mage("Sauron"));
        mages.Add(new Mage("Saruman"));


        // spelloop - spelet körs så länge spelaren har hälsa kvar
        while(playerHealth > 0)
        {
            // Skriv ut en meny
            Console.WriteLine("========================================");
            Console.WriteLine($"Du har {playerHealth} hälsa");
            Console.WriteLine("---------------------------");
            Console.WriteLine("Det finns följande fiender:");
            for(int i = 0; i < mages.Count; i++)
            {
                Mage mage = mages[i];
                Console.WriteLine($"{i+1}. {mage.Name} har {mage.Health} hälsa och {mage.Mana} mana");
            }
            Console.WriteLine("---------------------------");
            Console.WriteLine("Välj vad du vill göra:");
            Console.WriteLine("1. Attackera");
            Console.WriteLine("2. Heala");
            Console.WriteLine("3. Avsluta");
            Console.Write("Ange alternativ 1-3:");
            string input = Console.ReadLine();

            Random random = new Random(); // används för att slumpa skada och häloregenerering
            switch(input)
            {
                case "1": // Attackera en viss fiende
                    Console.Write("Vilken fiende vill du attackera:");
                    // läs in vem spelaren vill attackera,
                    // tag värde -1 för att få rätt index i listan
                    int enemyIndex = int.Parse(Console.ReadLine())-1;
                    Mage mage = mages[enemyIndex];
                    // ge fienden skada:
                    mage.Health -= 100 + random.Next(0, 30);
                    break;

                case "2": // Heala sig själv
                    Console.WriteLine("Du healar dig själv");
                    playerHealth += 50 + random.Next(0, 50);
                    break;

                case "3": // Avsluta spelet
                    Console.WriteLine("Du avslutar spelet");
                    return;

                default:
                    Console.WriteLine("Felaktig input");
                    break;
            }

            // ---------------------------------------------
            // Gå igenom alla fiender, en efter en och:
            //
            // OM:      en fiende har 0 eller mindre hälsa, skriv att den är död
            //          och ta bort den ur listan
            // 
            // ANNARS   låt fienden attackera spelaren 
            // ---------------------------------------------
            for(int i = 0; i < mages.Count; i++)
            {
                // om fienden har 0 eller mindre hälsa, döda den
                if(mages[i].Health <= 0)
                {
                    Console.WriteLine($"{mages[i].Name} dog");
                    // ta bort fienden från listan utifrån dess index
                    mages.RemoveAt(i);

                    // i och med att denna fiende tas bort från listan så kommer 
                    // alla andra fiender att flyttas ett steg uppåt i listan
                    // Därför måste vi minska i med 1 för att inte hoppa över en fiende
                    i--;
                    // Vi hoppar över resten av loopen och går till nästa iteration.
                    continue;
                }
                // om vi inte kör continue ovan, riskerar vi att hamna out of bounds
                // , på sista elementet

                // Fienden gör sin attack på spelaren:
                playerHealth -= mages[i].Attack();
            }

            // ---------------------------------------------
            // Kontrollera om spelaren har dött, isf avsluta mainloopen
            if(playerHealth <= 0)
            {
                Console.WriteLine("Du dog.");
                break;
            }

            // Vänta på att användaren ska trycka på en tangent och rensa skärmen
            Console.ReadKey();
            Console.Clear();
        }
        Console.WriteLine("Spelet är slut");
    }
}