using System;
using System.Collections.Generic;
using System.Linq;

class Room
{
    public string Name { get; }
    public string Description { get; }
    public Dictionary<string, Room> Exits { get; } = new(); // "N","S","E","W"
    public List<string> Items { get; } = new();

    public Room(string name, string description)
    {
        Name = name;
        Description = description;
    }
}

class Program
{
    static void Main()
    {
        // Build the world
        Room bedroom = new Room("Bedroom", "A small bedroom. The air feels still. There’s a door to the east.");
        Room hall = new Room("Hall", "A narrow hall with creaky floorboards. Doors lead back west and north.");
        Room kitchen = new Room("Kitchen", "A warm kitchen. It smells faintly like bread. There’s a door to the east.");
        Room garden = new Room("Garden", "A quiet garden with overgrown grass. The sky feels bigger out here.");

        bedroom.Exits["E"] = hall;

        hall.Exits["W"] = bedroom;
        hall.Exits["N"] = kitchen;

        kitchen.Exits["S"] = hall;
        kitchen.Exits["E"] = garden;

        garden.Exits["W"] = kitchen;

        // Items
        bedroom.Items.Add("key");
        kitchen.Items.Add("apple");

        // Player state
        Room currentRoom = bedroom;
        List<string> inventory = new();

        Console.Clear();
        Console.WriteLine("=== Text Adventure ===");
        Console.WriteLine("Commands: N/S/E/W, LOOK, INV, TAKE <item>, HELP, M (menu), Q (quit)\n");

        PrintRoom(currentRoom);

        // Game loop
        while (true)
        {
            Console.Write("\n> ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                continue;

            input = input.Trim();

            // Single-letter shortcuts
            if (input.Equals("Q", StringComparison.OrdinalIgnoreCase))
                return;

            if (input.Equals("M", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Menu not added yet for this project — but we can add one next.");
                Console.WriteLine("For now: Q quits.");
                continue;
            }

            // HELP
            if (input.Equals("HELP", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Commands:");
                Console.WriteLine("  N/S/E/W      Move");
                Console.WriteLine("  LOOK         Reprint the room description");
                Console.WriteLine("  INV          Show your inventory");
                Console.WriteLine("  TAKE <item>  Pick up an item (example: TAKE key)");
                Console.WriteLine("  Q            Quit");
                continue;
            }

            // LOOK
            if (input.Equals("LOOK", StringComparison.OrdinalIgnoreCase))
            {
                PrintRoom(currentRoom);
                continue;
            }

            // INV
            if (input.Equals("INV", StringComparison.OrdinalIgnoreCase))
            {
                if (inventory.Count == 0)
                    Console.WriteLine("Your inventory is empty.");
                else
                    Console.WriteLine("Inventory: " + string.Join(", ", inventory));
                continue;
            }

            // Movement: N/S/E/W
            string upper = input.ToUpper();
            if (upper is "N" or "S" or "E" or "W")
            {
                if (currentRoom.Exits.TryGetValue(upper, out Room? nextRoom))
                {
                    currentRoom = nextRoom;
                    PrintRoom(currentRoom);
                }
                else
                {
                    Console.WriteLine("You can’t go that way.");
                }
                continue;
            }

            // TAKE <item>
            if (input.StartsWith("TAKE ", StringComparison.OrdinalIgnoreCase))
            {
                string itemName = input.Substring(5).Trim().ToLower();

                if (string.IsNullOrWhiteSpace(itemName))
                {
                    Console.WriteLine("Take what?");
                    continue;
                }

                // Find item in room (case-insensitive)
                string? found = currentRoom.Items.FirstOrDefault(i => i.Equals(itemName, StringComparison.OrdinalIgnoreCase));

                if (found == null)
                {
                    Console.WriteLine($"There is no '{itemName}' here.");
                    continue;
                }

                currentRoom.Items.Remove(found);
                inventory.Add(found);
                Console.WriteLine($"You took the {found}.");
                continue;
            }

            Console.WriteLine("I don’t understand that command. Type HELP.");
        }
    }

    static void PrintRoom(Room room)
    {
        Console.WriteLine($"\n--- {room.Name} ---");
        Console.WriteLine(room.Description);

        if (room.Items.Count > 0)
            Console.WriteLine("You see: " + string.Join(", ", room.Items));

        if (room.Exits.Count > 0)
            Console.WriteLine("Exits: " + string.Join(", ", room.Exits.Keys));
    }
}
