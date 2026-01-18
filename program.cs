// Program.cs
// CSE 310 Module #1 (C#) - Console Choose Your Own Adventure
// 3 main stories, each with 10+ choices (decision points).
// How it meets requirements:
// - Variables, conditionals, loops: input validation, branching, main loop
// - Methods: reusable functions for running stories, getting input
// - Classes: StoryNode + Option
// - Data structures: Dictionary for nodes, List for options

using System;
using System.Collections.Generic;

namespace ChooseYourAdventure
{
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
        public List<Option> Options { get; }
        public bool IsEnding { get; }

        public StoryNode(string id, string title, string description, bool isEnding = false)
        {
            Id = id;
            Title = title;
            Description = description;
            IsEnding = isEnding;
            Options = new List<Option>();
        }
    }

    class Program
    {
        static void Main()
        {
            Console.Title = "Choose Your Own Adventure (C#)";
            PrintHeader();

            while (true)
            {
                Console.WriteLine("Pick a story:");
                Console.WriteLine("1) The Lost Cabin (Mystery)");
                Console.WriteLine("2) Neon City Run (Sci-Fi)");
                Console.WriteLine("3) Dragon Peak (Fantasy)");
                Console.WriteLine("4) Quit");
                Console.WriteLine();

                int choice = GetMenuChoice(1, 4);

                if (choice == 4)
                {
                    Console.WriteLine("\nThanks for playing!");
                    break;
                }

                switch (choice)
                {
                    case 1:
                        RunStory("The Lost Cabin", BuildStory_LostCabin(), startId: "A1");
                        break;
                    case 2:
                        RunStory("Neon City Run", BuildStory_NeonCity(), startId: "B1");
                        break;
                    case 3:
                        RunStory("Dragon Peak", BuildStory_DragonPeak(), startId: "C1");
                        break;
                }

                Console.WriteLine("\nBack to main menu...\n");
            }
        }

        static void PrintHeader()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("     CHOOSE YOUR OWN ADVENTURE (C#)     ");
            Console.WriteLine("========================================");
            Console.WriteLine("Type the number of your choice and press Enter.\n");
        }

        static void RunStory(string storyName, Dictionary<string, StoryNode> nodes, string startId)
        {
            Console.Clear();
            Console.WriteLine($"=== {storyName} ===\n");

            string currentId = startId;

            while (true)
            {
                if (!nodes.ContainsKey(currentId))
                {
                    Console.WriteLine("ERROR: Story is missing a node. Ending early.");
                    return;
                }

                StoryNode node = nodes[currentId];

                Console.WriteLine($"-- {node.Title} --");
                Console.WriteLine(node.Description);
                Console.WriteLine();

                if (node.IsEnding)
                {
                    Console.WriteLine("=== THE END ===");
                    Console.WriteLine("\nPress Enter to return to the menu...");
                    Console.ReadLine();
                    Console.Clear();
                    return;
                }

                for (int i = 0; i < node.Options.Count; i++)
                {
                    Console.WriteLine($"{i + 1}) {node.Options[i].Text}");
                }

                Console.WriteLine();
                int optionPick = GetMenuChoice(1, node.Options.Count);
                currentId = node.Options[optionPick - 1].NextId;

                Console.Clear();
                Console.WriteLine($"=== {storyName} ===\n");
            }
        }

        static int GetMenuChoice(int min, int max)
        {
            while (true)
            {
                Console.Write($"Choose ({min}-{max}): ");
                string input = Console.ReadLine() ?? "";

                if (int.TryParse(input, out int number) && number >= min && number <= max)
                    return number;

                Console.WriteLine("Invalid choice. Try again.\n");
            }
        }

        // ----------------------------
        // STORY 1: The Lost Cabin (A*)
        // 10 decision points: A1..A10
        // ----------------------------
        static Dictionary<string, StoryNode> BuildStory_LostCabin()
        {
            var nodes = new Dictionary<string, StoryNode>();

            nodes["A1"] = new StoryNode("A1", "Foggy Trailhead",
                "You arrive at a foggy forest trail. A warning sign is half-covered in moss.\n" +
                "You hear a faint knock somewhere deeper in the woods.");

            nodes["A2"] = new StoryNode("A2", "Mossy Sign",
                "The sign says: 'TURN BACK AFTER DARK.' It’s already getting late.\n" +
                "A narrow path continues, and a wider road curves away.");

            nodes["A3"] = new StoryNode("A3", "Broken Lantern",
                "You find a broken lantern and footprints leading off the path.\n" +
                "A cold breeze makes the trees creak like old doors.");

            nodes["A4"] = new StoryNode("A4", "Creek Crossing",
                "A shallow creek blocks the trail. Stones form a wobbly crossing.\n" +
                "Downstream, you spot a fallen log that could be used as a bridge.");

            nodes["A5"] = new StoryNode("A5", "Cabin Lights",
                "You see a small cabin. A single light flickers inside.\n" +
                "The front door is closed, but a side window is slightly open.");

            nodes["A6"] = new StoryNode("A6", "Inside the Cabin",
                "Inside, you see dusty furniture and a journal on the table.\n" +
                "A trapdoor is set into the floor, and the back room is pitch black.");

            nodes["A7"] = new StoryNode("A7", "The Journal",
                "The journal mentions a 'hidden cellar' and a 'watcher in the trees.'\n" +
                "A map is drawn, but part of it is ripped out.");

            nodes["A8"] = new StoryNode("A8", "The Trapdoor",
                "The trapdoor creaks open. You smell damp earth.\n" +
                "A ladder goes down into darkness. You hear water dripping.");

            nodes["A9"] = new StoryNode("A9", "Back Room",
                "In the back room, you find a radio and a locked metal box.\n" +
                "The radio crackles with a repeating message: 'Don’t trust the knocking.'");

            nodes["A10"] = new StoryNode("A10", "The Watcher",
                "You step outside and see a shadow between the trees.\n" +
                "The knocking is closer now—on the cabin wall, from the inside.",
                isEnding: true);

            nodes["A11"] = new StoryNode("A11", "Safe Exit",
                "You decide not to gamble with the unknown. You leave calmly and follow the wide road back.\n" +
                "The fog lifts as you reach your car. Something taps the trunk once... then stops.",
                isEnding: true);

            // Choices (10+ decision points)
            nodes["A1"].Options.Add(new Option("Read the warning sign and look around", "A2"));
            nodes["A1"].Options.Add(new Option("Ignore it and follow the narrow path", "A3"));

            nodes["A2"].Options.Add(new Option("Take the wider road away from the woods", "A11"));
            nodes["A2"].Options.Add(new Option("Go anyway—follow the narrow path", "A3"));

            nodes["A3"].Options.Add(new Option("Follow the footprints", "A4"));
            nodes["A3"].Options.Add(new Option("Stay on the main trail", "A5"));

            nodes["A4"].Options.Add(new Option("Step across the stones", "A5"));
            nodes["A4"].Options.Add(new Option("Use the fallen log bridge", "A5"));

            nodes["A5"].Options.Add(new Option("Knock on the front door", "A6"));
            nodes["A5"].Options.Add(new Option("Climb through the side window", "A6"));

            nodes["A6"].Options.Add(new Option("Read the journal on the table", "A7"));
            nodes["A6"].Options.Add(new Option("Open the trapdoor", "A8"));

            nodes["A7"].Options.Add(new Option("Search for the missing map piece", "A9"));
            nodes["A7"].Options.Add(new Option("Go down the trapdoor anyway", "A8"));

            nodes["A8"].Options.Add(new Option("Go down carefully", "A10"));
            nodes["A8"].Options.Add(new Option("Close it and check the back room instead", "A9"));

            nodes["A9"].Options.Add(new Option("Try the radio for help", "A10"));
            nodes["A9"].Options.Add(new Option("Leave the cabin immediately", "A11"));

            return nodes;
        }

        // ----------------------------
        // STORY 2: Neon City Run (B*)
        // 10 decision points: B1..B10
        // ----------------------------
        static Dictionary<string, StoryNode> BuildStory_NeonCity()
        {
            var nodes = new Dictionary<string, StoryNode>();

            nodes["B1"] = new StoryNode("B1", "Rooftop Signal",
                "Neon lights flicker below. Your wrist device blinks: 'PACKAGE HOT.'\n" +
                "A drone patrol sweeps the skyline.");

            nodes["B2"] = new StoryNode("B2", "Alley Drop",
                "You drop into an alley. Two routes: a crowded market or a silent service tunnel.\n" +
                "Your comms whisper: 'They’re scanning faces.'");

            nodes["B3"] = new StoryNode("B3", "Market Crowd",
                "Music pounds. The crowd can hide you—but cameras are everywhere.\n" +
                "A street vendor offers a hooded jacket for cheap.");

            nodes["B4"] = new StoryNode("B4", "Service Tunnel",
                "The tunnel is quiet. Pipes hiss steam. Your footsteps echo.\n" +
                "A locked maintenance door blocks the straight path.");

            nodes["B5"] = new StoryNode("B5", "Checkpoint",
                "A corporate checkpoint appears ahead. Guards and scanners.\n" +
                "You spot a ladder to the catwalk and a dumpster nearby.");

            nodes["B6"] = new StoryNode("B6", "Catwalk Chase",
                "You climb the ladder. A drone spots you—red light flashes.\n" +
                "You can run the catwalk or jump to a neighboring building.");

            nodes["B7"] = new StoryNode("B7", "Hacker’s Door",
                "A hidden door opens: a hacker den. Someone says, 'Show me the package.'\n" +
                "They can help—or take it.");

            nodes["B8"] = new StoryNode("B8", "Data Upload",
                "You plug the package into a terminal. It starts uploading.\n" +
                "A warning pops up: 'TRACE DETECTED.'");

            nodes["B9"] = new StoryNode("B9", "Final Sprint",
                "Sirens blare. You have one shot to escape.\n" +
                "A metro entrance is ahead, and a delivery bike is leaning nearby.");

            nodes["B10"] = new StoryNode("B10", "Neon Dawn",
                "You disappear into the city as the sky turns gray.\n" +
                "Mission complete... for now.",
                isEnding: true);

            nodes["B11"] = new StoryNode("B11", "Caught",
                "A scanner pings your face. Guards close in fast.\n" +
                "You fight for a second, but the city always wins this round.",
                isEnding: true);

            // Choices (10 decision points)
            nodes["B1"].Options.Add(new Option("Wait for the drone to pass, then move", "B2"));
            nodes["B1"].Options.Add(new Option("Sprint now and risk being seen", "B2"));

            nodes["B2"].Options.Add(new Option("Blend into the market crowd", "B3"));
            nodes["B2"].Options.Add(new Option("Take the service tunnel", "B4"));

            nodes["B3"].Options.Add(new Option("Buy the hooded jacket (reduce risk)", "B5"));
            nodes["B3"].Options.Add(new Option("Skip it and move fast", "B5"));

            nodes["B4"].Options.Add(new Option("Try to force the maintenance door", "B5"));
            nodes["B4"].Options.Add(new Option("Backtrack and take the market instead", "B3"));

            nodes["B5"].Options.Add(new Option("Hide and sneak past the guards", "B6"));
            nodes["B5"].Options.Add(new Option("Create a distraction with the dumpster", "B6"));

            nodes["B6"].Options.Add(new Option("Run the catwalk and dodge the drone", "B7"));
            nodes["B6"].Options.Add(new Option("Jump to the neighboring building", "B7"));

            nodes["B7"].Options.Add(new Option("Trust the hacker and show the package", "B8"));
            nodes["B7"].Options.Add(new Option("Refuse and try to upload it yourself", "B8"));

            nodes["B8"].Options.Add(new Option("Finish upload despite trace", "B9"));
            nodes["B8"].Options.Add(new Option("Abort upload and flee", "B11"));

            nodes["B9"].Options.Add(new Option("Escape via metro", "B10"));
            nodes["B9"].Options.Add(new Option("Escape via delivery bike", "B10"));

            return nodes;
        }

        // ----------------------------
        // STORY 3: Dragon Peak (C*)
        // 10 decision points: C1..C10
        // ----------------------------
        static Dictionary<string, StoryNode> BuildStory_DragonPeak()
        {
            var nodes = new Dictionary<string, StoryNode>();

            nodes["C1"] = new StoryNode("C1", "Village Request",
                "A village elder asks you to retrieve a stolen relic from Dragon Peak.\n" +
                "You can take the mountain path or the forest trail.");

            nodes["C2"] = new StoryNode("C2", "Forest Trail",
                "The forest is quiet. Too quiet.\n" +
                "You find strange claw marks on trees and a small pouch of herbs.");

            nodes["C3"] = new StoryNode("C3", "Mountain Path",
                "The climb is steep. Wind cuts through your cloak.\n" +
                "A traveler warns: 'The peak listens.'");

            nodes["C4"] = new StoryNode("C4", "River Fork",
                "A river blocks your way. A bridge looks weak.\n" +
                "A shallow crossing is possible, but the current is fast.");

            nodes["C5"] = new StoryNode("C5", "Cave Entrance",
                "A cave entrance appears—warm air flows out.\n" +
                "You hear distant breathing inside.");

            nodes["C6"] = new StoryNode("C6", "Old Shrine",
                "An old shrine stands with runes. A pedestal is empty.\n" +
                "A note says: 'Offer truth to pass.'");

            nodes["C7"] = new StoryNode("C7", "Stone Door",
                "A stone door blocks the path. It has two levers: Sun and Moon.\n" +
                "The air hums with magic.");

            nodes["C8"] = new StoryNode("C8", "Dragon’s Hall",
                "You enter a massive hall. A dragon watches you silently.\n" +
                "The stolen relic glows behind it.");

            nodes["C9"] = new StoryNode("C9", "The Bargain",
                "The dragon speaks: 'Take it, and the village suffers. Leave it, and you walk free.'\n" +
                "A third option forms in your mind: negotiate.");

            nodes["C10"] = new StoryNode("C10", "Relic Returned",
                "You return with the relic—or a new agreement.\n" +
                "The village celebrates, and your name becomes legend.",
                isEnding: true);

            nodes["C11"] = new StoryNode("C11", "Dragon’s Wrath",
                "You rush for the relic. The dragon’s eyes flare.\n" +
                "A single breath of flame ends the story.",
                isEnding: true);

            // Choices (10 decision points)
            nodes["C1"].Options.Add(new Option("Take the forest trail", "C2"));
            nodes["C1"].Options.Add(new Option("Take the mountain path", "C3"));

            nodes["C2"].Options.Add(new Option("Pick up the herbs for later", "C4"));
            nodes["C2"].Options.Add(new Option("Ignore the herbs and move quickly", "C4"));

            nodes["C3"].Options.Add(new Option("Ask the traveler for advice", "C4"));
            nodes["C3"].Options.Add(new Option("Stay silent and keep climbing", "C4"));

            nodes["C4"].Options.Add(new Option("Cross the weak bridge", "C5"));
            nodes["C4"].Options.Add(new Option("Wade through the shallow crossing", "C5"));

            nodes["C5"].Options.Add(new Option("Enter the cave carefully", "C6"));
            nodes["C5"].Options.Add(new Option("Scout around the cave entrance first", "C6"));

            nodes["C6"].Options.Add(new Option("Speak an honest vow (truth)", "C7"));
            nodes["C6"].Options.Add(new Option("Try to ignore the shrine and pass", "C7"));

            nodes["C7"].Options.Add(new Option("Pull the Sun lever", "C8"));
            nodes["C7"].Options.Add(new Option("Pull the Moon lever", "C8"));

            nodes["C8"].Options.Add(new Option("Approach respectfully and bow", "C9"));
            nodes["C8"].Options.Add(new Option("Rush for the relic", "C11"));

            nodes["C9"].Options.Add(new Option("Negotiate: offer a trade/quest instead", "C10"));
            nodes["C9"].Options.Add(new Option("Leave the relic and walk away", "C10"));

            return nodes;
        }
    }
}
