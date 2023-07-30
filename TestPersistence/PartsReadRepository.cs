namespace TestPersistence;

using Models.Types.Components;
using Application.Persistence;
using System.Collections.Generic;
using Models.Types.Common;

public class PartsReadRepository : IReadOnlyRepository<Part>
{
    // Specification of materials for the traffic light DIY project
    // Source: https://www.instructables.com/Traffic-Light-Project/
    public IEnumerable<Part> GetAll() => new[]
    {
        new Part(Ids[0], "Transistor BC547", new StockKeepingUnit("ELTRBC547")),
        new Part(Ids[1], "Resistor 1K", new StockKeepingUnit("ELRS1K")),
        new Part(Ids[2], "Resistor 100K", new StockKeepingUnit("ELRS100K")),
        new Part(Ids[3], "Resistor 33K", new StockKeepingUnit("ELRS33K")),
        new Part(Ids[4], "LED Red 3V", new StockKeepingUnit("ELLD3VRED")),
        new Part(Ids[5], "LED Yellow 3V", new StockKeepingUnit("ELLD3VYELLOW")),
        new Part(Ids[6], "LED Green 3V", new StockKeepingUnit("ELLD3VGREEN")),
        new Part(Ids[7], "Capacitor 470uF 25V", new StockKeepingUnit("ELCP470UF25V")),
        new Part(Ids[8], "Capacitor 100uF 25V", new StockKeepingUnit("ELCP100UF25V")),
        new Part(Ids[9], "Battery 9V", new StockKeepingUnit("BT9V")),
        new Part(Ids[10], "Battery clipper Type A", new StockKeepingUnit("BTCLA")),
    };

    public Option<Part> TryFind(Guid id) => 
        GetAll().Where(part => part.Id == id)
        .Select(part => part.Optional()).SingleOrDefault(None.Value);

    private static Guid[] Ids { get; } =
        Enumerable.Range(0, 1000).Select(_ => Guid.NewGuid()).ToArray();
}