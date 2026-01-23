using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Map map = Map.BuildDefault();
        Room currentRoom = map.StartRoom;

        List<Item> inventory = new();

        Console.Clear();
        Console.WriteLine("=== Text Adventure ===");
        Console.WriteLine("Commands: N/S/E/W, LOOK, INV, TAKE <item>, DROP <item>, HELP, Q\n");

        PrintRoom(currentRoom);

        while (true)
        {
            Console.Write("\n> ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                continue;

            input = input.Trim();

            if (input.Equals("Q", StringComparison.OrdinalIgnoreCase))
                return;

            if (input.Equals("HELP", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Commands:");
                Console.WriteLine("  N/S/E/W        Move");
                Console.WriteLine("  LOOK           Reprint the room");
                Console.WriteLine("  INV            Inventory");
                Console.WriteLine("  TAKE <item>    Pick up an item (TAKE key)");
                Console.WriteLine("  DROP <item>    Drop an item (DROP apple)");
                Console.WriteLine("  Q              Quit");
                continue;
            }

            if (input.Equals("LOOK", StringComparison.OrdinalIgnoreCase))
            {
                PrintRoom(currentRoom);
                continue;
            }

            if (input.Equals("INV", StringComparison.OrdinalIgnoreCase))
            {
                if (inventory.Count == 0) Console.WriteLine("Your inventory is empty.");
                else Console.WriteLine("Inventory: " + string.Join(", ", inventory.Select(i => i.Name)));
                continue;
            }

            // Movement
            var cmdUpper = input.ToUpper();
            if (cmdUpper is "N" or "S" or "E" or "W" or "NORTH" or "SOUTH" or "EAST" or "WEST")
            {
                if (currentRoom.TryGetExit(cmdUpper, out Room? next) && next != null)
                {
                    currentRoom = next;
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
                string itemId = input.Substring(5).Trim();

                if (currentRoom.TryRemoveItem(itemId, out Item? pickedUp) && pickedUp != null)
                {
                    inventory.Add(pickedUp);
                    Console.WriteLine($"You took the {pickedUp.Name}.");
                }
                else
                {
                    Console.WriteLine($"There is no '{itemId}' here.");
                }
                continue;
            }

            // DROP <item>
            if (input.StartsWith("DROP ", StringComparison.OrdinalIgnoreCase))
            {
                string itemId = input.Substring(5).Trim().ToLower();

                Item? item = inventory.FirstOrDefault(i =>
                    i.Id.Equals(itemId, StringComparison.OrdinalIgnoreCase) ||
                    i.Name.Equals(itemId, StringComparison.OrdinalIgnoreCase));

                if (item == null)
                {
                    Console.WriteLine($"You don’t have '{itemId}'.");
                    continue;
                }

                inventory.Remove(item);
                currentRoom.AddItem(item);
                Console.WriteLine($"You dropped the {item.Name}.");
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
            Console.WriteLine("You see: " + string.Join(", ", room.Items.Select(i => i.Name)));

        if (room.Exits.Count > 0)
            Console.WriteLine("Exits: " + string.Join(", ", room.Exits.Keys));
    }
}
