using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Map
{
    public Room StartRoom { get; }

    public Map(Room startRoom)
    {
        StartRoom = startRoom;
    }

    public static Map BuildDefault()
    {
        // Rooms
        var bedroom = new Room("bedroom", "Bedroom", "A small bedroom. The air feels still. There’s a door to the east.");
        var hall = new Room("hall", "Hall", "A narrow hall with creaky floorboards. Doors lead west and north.");
        var kitchen = new Room("kitchen", "Kitchen", "A warm kitchen. It smells faintly like bread. There’s a door to the east.");
        var garden = new Room("garden", "Garden", "A quiet garden with overgrown grass. The sky feels bigger out here.");

        // Exits (manual map)
        bedroom.SetExit("E", hall);

        hall.SetExit("W", bedroom);
        hall.SetExit("N", kitchen);

        kitchen.SetExit("S", hall);
        kitchen.SetExit("E", garden);

        garden.SetExit("W", kitchen);

        // Items
        bedroom.AddItem(new Item("key", "Key", "A small brass key with scratches along the edge."));
        kitchen.AddItem(new Item("apple", "Apple", "A slightly bruised apple. Still looks edible."));

        return new Map(bedroom);
    }
}
