using System;
using System.Collections.Generic;
using System.IO;

namespace ChooseYourAdventure
{
    // ----------------------------
    // Story building blocks
    // ----------------------------
    public class Option
    {
        public string Text { get; }
        public string NextId { get; }

        public Option(string text, string nextId)
        {
            Text = text;
            NextId = nextId;
        }
    }

    public class StoryNode
    {
        public string Id { get; }
        public string Title { get; }
        public string Description { get; }
        public List<Option> Options { get; } = new List<Option>();
        public bool IsEnding { get; }

        public StoryNode(string id, string title, string description, bool isEnding = false)
        {
            Id = id;
            Title = title;
            Description = description;
            IsEnding = isEnding;
        }
    }

    // ----------------------------
    // REQUIRED: abstract + inheritance
    // ----------------------------
    public abstract class StoryBase
    {
        public abstract string Name { get; }
        public abstract string StartId { get; }
        public abstract Dictionary<string, StoryNode> BuildStory();
    }

    // ============================
    // STORY 1: Lost Cabin (A*)
    // 10 decision nodes: A1..A10
    // Endings: A11, A12
    // ============================
    public class LostCabinStory : StoryBase
    {
        public override string Name => "The Lost Cabin";
        public override string StartId => "A1";

        public override Dictionary<string, StoryNode> BuildStory()
        {
            var n = new Dictionary<string, StoryNode>();

            n["A1"] = new StoryNode("A1", "Foggy Trailhead",
                "You step into a foggy forest. A warning sign leans to the side.");
            n["A2"] = new StoryNode("A2", "The Warning Sign",
                "It reads: 'TURN BACK AFTER DARK.' The fog thickens.");
            n["A3"] = new StoryNode("A3", "Footprints",
                "You find fresh footprints and a broken lantern.");
            n["A4"] = new StoryNode("A4", "Creek Crossing",
                "A creek blocks the path. Stones are slick and unstable.");
            n["A5"] = new StoryNode("A5", "Cabin in the Mist",
                "A cabin appears. A light flickers inside.");
            n["A6"] = new StoryNode("A6", "Front Door",
                "The door is cold to the touch. You hear a faint knocking.");
            n["A7"] = new StoryNode("A7", "Inside the Cabin",
                "Dusty furniture. A journal on the table. A trapdoor in the floor.");
            n["A8"] = new StoryNode("A8", "The Journal",
                "The journal mentions a 'watcher' and a hidden cellar.");
            n["A9"] = new StoryNode("A9", "The Trapdoor",
                "The trapdoor creaks open. Darkness and damp air rise.");
            n["A10"] = new StoryNode("A10", "The Knock",
                "The knocking is now inside the walls... and behind you.");

            n["A11"] = new StoryNode("A11", "Safe Exit",
                "You leave fast and make it back to the road. The fog fades. You’re safe... probably.",
                isEnding: true);
            n["A12"] = new StoryNode("A12", "The Watcher",
                "You turn too late. Something steps out of the fog. The forest goes quiet.",
                isEnding: true);

            // 10 DECISIONS (A1..A10 each has choices)
            n["A1"].Options.Add(new Option("Read the sign", "A2"));
            n["A1"].Options.Add(new Option("Ignore it and walk in", "A3"));

            n["A2"].Options.Add(new Option("Turn back", "A11"));
            n["A2"].Options.Add(new Option("Keep going", "A3"));

            n["A3"].Options.Add(new Option("Follow the footprints", "A4"));
            n["A3"].Options.Add(new Option("Stay on the main path", "A5"));

            n["A4"].Options.Add(new Option("Cross using stones", "A5"));
            n["A4"].Options.Add(new Option("Follow creek edge", "A5"));

            n["A5"].Options.Add(new Option("Approach the front door", "A6"));
            n["A5"].Options.Add(new Option("Look for another way in", "A6"));

            n["A6"].Options.Add(new Option("Knock first", "A7"));
            n["A6"].Options.Add(new Option("Open the door quietly", "A7"));

            n["A7"].Options.Add(new Option("Read the journal", "A8"));
            n["A7"].Options.Add(new Option("Check the trapdoor", "A9"));

            n["A8"].Options.Add(new Option("Search for a key", "A9"));
            n["A8"].Options.Add(new Option("Leave the cabin now", "A11"));

            n["A9"].Options.Add(new Option("Go down the ladder", "A10"));
            n["A9"].Options.Add(new Option("Close it and go outside", "A11"));

            n["A10"].Options.Add(new Option("Run for the door", "A11"));
            n["A10"].Options.Add(new Option("Turn around and face it", "A12"));

            return n;
        }
    }

    // ============================
    // STORY 2: Neon City Run (B*)
    // 10 decision nodes: B1..B10
    // Endings: B11, B12
    // ============================
    public class NeonCityStory : StoryBase
    {
        public override string Name => "Neon City Run";
        public override string StartId => "B1";

        public override Dictionary<string, StoryNode> BuildStory()
        {
            var n = new Dictionary<string, StoryNode>();

            n["B1"] = new StoryNode("B1", "Rooftop Drop",
                "You hold a hot package. Drones scan the skyline.");
            n["B2"] = new StoryNode("B2", "Fire Escape",
                "Metal stairs rattle under your feet. A drone light sweeps nearby.");
            n["B3"] = new StoryNode("B3", "Alley Split",
                "Two routes: busy market or quiet service tunnel.");
            n["B4"] = new StoryNode("B4", "Market Crowd",
                "Crowds can hide you, but cameras are everywhere.");
            n["B5"] = new StoryNode("B5", "Service Tunnel",
                "Quiet, dark, and echoing. A locked door blocks part of the way.");
            n["B6"] = new StoryNode("B6", "Checkpoint Ahead",
                "Guards and scanners. A ladder leads to a catwalk.");
            n["B7"] = new StoryNode("B7", "Catwalk Run",
                "You sprint above the street. A drone spots you.");
            n["B8"] = new StoryNode("B8", "Hacker Den",
                "A hidden door opens. A hacker says, 'Show me the package.'");
            n["B9"] = new StoryNode("B9", "Upload Terminal",
                "Uploading begins. Warning: TRACE DETECTED.");
            n["B10"] = new StoryNode("B10", "Final Escape",
                "Sirens scream. Metro entrance left. Bike right.");

            n["B11"] = new StoryNode("B11", "Neon Dawn",
                "You vanish into the city. Mission complete.",
                isEnding: true);
            n["B12"] = new StoryNode("B12", "Captured",
                "Scanner pings. Guards surround you. Game over.",
                isEnding: true);

            // 10 DECISIONS (B1..B10)
            n["B1"].Options.Add(new Option("Wait for drone to pass", "B2"));
            n["B1"].Options.Add(new Option("Move now and risk it", "B2"));

            n["B2"].Options.Add(new Option("Go down fast", "B3"));
            n["B2"].Options.Add(new Option("Go down slow and quiet", "B3"));

            n["B3"].Options.Add(new Option("Blend into the market", "B4"));
            n["B3"].Options.Add(new Option("Take the service tunnel", "B5"));

            n["B4"].Options.Add(new Option("Buy a hoodie disguise", "B6"));
            n["B4"].Options.Add(new Option("Skip disguise, keep moving", "B6"));

            n["B5"].Options.Add(new Option("Force the locked door", "B6"));
            n["B5"].Options.Add(new Option("Backtrack to the market", "B4"));

            n["B6"].Options.Add(new Option("Sneak near the guards", "B7"));
            n["B6"].Options.Add(new Option("Climb the ladder to catwalk", "B7"));

            n["B7"].Options.Add(new Option("Run the catwalk", "B8"));
            n["B7"].Options.Add(new Option("Jump to the next roof", "B8"));

            n["B8"].Options.Add(new Option("Trust the hacker", "B9"));
            n["B8"].Options.Add(new Option("Refuse and do it yourself", "B9"));

            n["B9"].Options.Add(new Option("Finish upload anyway", "B10"));
            n["B9"].Options.Add(new Option("Abort and run", "B12"));

            n["B10"].Options.Add(new Option("Escape via metro", "B11"));
            n["B10"].Options.Add(new Option("Escape via bike", "B11"));

            return n;
        }
    }

    // ============================
    // STORY 3: Dragon Peak (C*)
    // 10 decision nodes: C1..C10
    // Endings: C11, C12
    // ============================
    public class DragonPeakStory : StoryBase
    {
        public override string Name => "Dragon Peak";
        public override string StartId => "C1";

        public override Dictionary<string, StoryNode> BuildStory()
        {
            var n = new Dictionary<string, StoryNode>();

            n["C1"] = new StoryNode("C1", "Village Request",
                "A village elder asks you to recover a stolen relic from Dragon Peak.");
            n["C2"] = new StoryNode("C2", "Path Choice",
                "You can take the forest trail or the rocky mountain path.");
            n["C3"] = new StoryNode("C3", "Forest Signs",
                "Claw marks on trees. You find herbs and tracks.");
            n["C4"] = new StoryNode("C4", "Windy Ridge",
                "Cold wind cuts hard. A traveler warns you: 'The peak listens.'");
            n["C5"] = new StoryNode("C5", "River Crossing",
                "A river blocks you. Bridge looks weak. Water is fast.");
            n["C6"] = new StoryNode("C6", "Cave Entrance",
                "Warm air flows out of a cave. You hear deep breathing.");
            n["C7"] = new StoryNode("C7", "Old Shrine",
                "A shrine says: 'Offer truth to pass.'");
            n["C8"] = new StoryNode("C8", "Stone Door",
                "A stone door has two levers: Sun and Moon.");
            n["C9"] = new StoryNode("C9", "Dragon’s Hall",
                "A dragon watches. The relic glows behind it.");
            n["C10"] = new StoryNode("C10", "The Bargain",
                "The dragon speaks: 'Take it and the village suffers. Leave it and walk free.'");

            n["C11"] = new StoryNode("C11", "Legend",
                "You make a smart deal and return home with peace. You become a legend.",
                isEnding: true);
            n["C12"] = new StoryNode("C12", "Dragon’s Wrath",
                "You rush the relic. The dragon ends the story instantly.",
                isEnding: true);

            // 10 DECISIONS (C1..C10)
            n["C1"].Options.Add(new Option("Accept the quest", "C2"));
            n["C1"].Options.Add(new Option("Ask for more info", "C2"));

            n["C2"].Options.Add(new Option("Take the forest trail", "C3"));
            n["C2"].Options.Add(new Option("Take the mountain path", "C4"));

            n["C3"].Options.Add(new Option("Collect herbs", "C5"));
            n["C3"].Options.Add(new Option("Follow tracks", "C5"));

            n["C4"].Options.Add(new Option("Listen to the traveler", "C5"));
            n["C4"].Options.Add(new Option("Ignore and press on", "C5"));

            n["C5"].Options.Add(new Option("Cross the weak bridge", "C6"));
            n["C5"].Options.Add(new Option("Wade through water", "C6"));

            n["C6"].Options.Add(new Option("Enter the cave quietly", "C7"));
            n["C6"].Options.Add(new Option("Scout the entrance first", "C7"));

            n["C7"].Options.Add(new Option("Speak an honest vow", "C8"));
            n["C7"].Options.Add(new Option("Try to pass without it", "C8"));

            n["C8"].Options.Add(new Option("Pull Sun lever", "C9"));
            n["C8"].Options.Add(new Option("Pull Moon lever", "C9"));

            n["C9"].Options.Add(new Option("Approach respectfully", "C10"));
            n["C9"].Options.Add(new Option("Rush for the relic", "C12"));

            n["C10"].Options.Add(new Option("Negotiate a trade/quest", "C11"));
            n["C10"].Options.Add(new Option("Leave the relic and walk away", "C11"));

            return n;
        }
    }

    class Program
    {
        static readonly string LogFilePath = "game_log.txt";

        static void Main()
        {
            Console.WriteLine("=== Choose Your Own Adventure ===\n");

            Console.Write("Enter your name: ");
            string playerName = Console.ReadLine() ?? "Player";

            List<StoryBase> stories = new List<StoryBase>
            {
                new LostCabinStory(),
                new NeonCityStory(),
                new DragonPeakStory()
            };

            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1) Play The Lost Cabin");
                Console.WriteLine("2) Play Neon City Run");
                Console.WriteLine("3) Play Dragon Peak");
                Console.WriteLine("4) View past game logs (READ FILE)");
                Console.WriteLine("5) Quit");

                int choice = GetChoice(1, 5);

                if (choice == 5)
                {
                    Console.WriteLine("Goodbye!");
                    return;
                }

                if (choice == 4)
                {
                    ShowLogs(); // READ FILE
                    continue;
                }

                StoryBase selected = stories[choice - 1];
                string endingTitle = RunStory(selected);

                SaveGameResult(playerName, selected.Name, endingTitle); // WRITE FILE
            }
        }

        static string RunStory(StoryBase story)
        {
            var nodes = story.BuildStory();
            string currentId = story.StartId;

            while (true)
            {
                StoryNode node = nodes[currentId];

                Console.WriteLine($"\n--- {story.Name} ---");
                Console.WriteLine($"[{node.Title}]");
                Console.WriteLine(node.Description);

                if (node.IsEnding)
                {
                    Console.WriteLine("\n=== THE END ===");
                    return node.Title;
                }

                for (int i = 0; i < node.Options.Count; i++)
                {
                    Console.WriteLine($"{i + 1}) {node.Options[i].Text}");
                }

                int pick = GetChoice(1, node.Options.Count);
                currentId = node.Options[pick - 1].NextId;
            }
        }

        static int GetChoice(int min, int max)
        {
            while (true)
            {
                Console.Write($"Choose ({min}-{max}): ");
                string input = Console.ReadLine() ?? "";

                if (int.TryParse(input, out int num) && num >= min && num <= max)
                    return num;

                Console.WriteLine("Invalid choice, try again.");
            }
        }

        // WRITE FILE
        static void SaveGameResult(string playerName, string storyName, string endingTitle)
        {
            string line = $"{DateTime.Now} | Player: {playerName} | Story: {storyName} | Ending: {endingTitle}";
            File.AppendAllText(LogFilePath, line + Environment.NewLine);
            Console.WriteLine($"\nSaved to log file: {LogFilePath}");
        }

        // READ FILE
        static void ShowLogs()
        {
            Console.WriteLine("\n=== Past Game Logs ===");

            if (!File.Exists(LogFilePath))
            {
                Console.WriteLine("No log file found yet. Play a story first.");
                return;
            }

            string[] lines = File.ReadAllLines(LogFilePath);

            if (lines.Length == 0)
            {
                Console.WriteLine("Log file is empty.");
                return;
            }

            // Show last 10 entries (simple)
            int start = Math.Max(0, lines.Length - 10);
            for (int i = start; i < lines.Length; i++)
            {
                Console.WriteLine(lines[i]);
            }
        }
    }
}
