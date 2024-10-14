// Huvudprogrammet
static class Program
{
    static void Main(string[] args)
    {
        int playerHealth = 500; // sätt spelarens starthälsa

        // skapa en lista med magiker (fiender)
        // TODO: denna lista ska innehålla alla möjliga typer av fiender
        List<Enemy> enemies = new List<Enemy>();
        enemies.Add(new Mage("Saruman"));
        enemies.Add(new Assassin("Hitman"));
        enemies.Add(new Mage("Sauron"));
        enemies.Add(new Assassin("James Bond"));

        // spelloop - spelet körs så länge spelaren har hälsa kvar
        while(playerHealth > 0)
        {
            // Skriv ut en meny
            Console.WriteLine("========================================");
            Console.WriteLine($"Du har {playerHealth} hälsa");
            Console.WriteLine("---------------------------");
            Console.WriteLine("Det finns följande fiender:");
            List<int> invisibleEnemyIndexes = new List<int>();
            for(int i = 0; i < enemies.Count; i++)
            {
                if(enemies[i].GetInfo() != null) // kontrolerar så att fienden är synlig
                {
                    Console.WriteLine($"{(i+1)}.  {enemies[i].GetInfo()}"); // skriv ut den synliga fienden
                }
                else // detta är en osynlig fiende (den returnerade "")
                { 
                    // Ingen utskrift här...
                    invisibleEnemyIndexes.Add(i); // lägg till indexet i listan med osynliga fiender
                }
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
                    // om spelaren valt en osynlig fiende, skriv ut felmeddelande och hoppa ur switchen
                    if(invisibleEnemyIndexes.Contains(enemyIndex))
                    {
                        Console.WriteLine("Du kan inte attackera en osynlig fiende");
                        break;
                    }
                    Enemy e = enemies[enemyIndex];
                    // ge fienden skada:
                    int damage = 20 + random.Next(0, 20);
                    System.Console.WriteLine($"Du attackerar {e.Name} för {damage} skada");
                    e.Defend(damage);
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
            for(int i = 0; i < enemies.Count; i++)
            {
                // om fienden har 0 eller mindre hälsa, döda den
                if(enemies[i].Health <= 0)
                {
                    Console.WriteLine($"{enemies[i].Name} dog");
                    // ta bort fienden från listan utifrån dess index
                    enemies.RemoveAt(i);

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
                playerHealth -= enemies[i].Attack();
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