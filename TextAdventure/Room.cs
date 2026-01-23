using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Room
{
    public string Id { get; }
    public string Name { get; }
    public string Description { get; }

    // Exits keyed by direction: "N", "S", "E", "W"
    private readonly Dictionary<string, Room> _exits = new(StringComparer.OrdinalIgnoreCase);
    public IReadOnlyDictionary<string, Room> Exits => _exits;

    private readonly List<Item> _items = new();
    public IReadOnlyList<Item> Items => _items;

    public Room(string id, string name, string description)
    {
        Id = id.Trim().ToLower();
        Name = name;
        Description = description;
    }

    public void SetExit(string direction, Room destination)
    {
        direction = NormalizeDirection(direction);
        _exits[direction] = destination;
    }

    public bool TryGetExit(string direction, out Room? destination)
    {
        direction = NormalizeDirection(direction);
        return _exits.TryGetValue(direction, out destination);
    }

    public void AddItem(Item item) => _items.Add(item);

    public bool TryRemoveItem(string itemId, out Item? item)
    {
        itemId = itemId.Trim().ToLower();

        item = _items.FirstOrDefault(i => i.Id.Equals(itemId, StringComparison.OrdinalIgnoreCase)
                                       || i.Name.Equals(itemId, StringComparison.OrdinalIgnoreCase));

        if (item == null) return false;

        _items.Remove(item);
        return true;
    }

    private static string NormalizeDirection(string direction)
    {
        direction = direction.Trim().ToUpper();
        return direction switch
        {
            "N" or "NORTH" => "N",
            "S" or "SOUTH" => "S",
            "E" or "EAST" => "E",
            "W" or "WEST" => "W",
            _ => direction
        };
    }
}

