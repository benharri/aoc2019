namespace aoc2019;

public sealed class Day14 : Day
{
    private readonly Dictionary<string, Reaction> reactions;

    private Dictionary<string, long> available;

    public Day14() : base(14, "Space Stoichiometry")
    {
        reactions = Input
            .Select(Reaction.Parse)
            .ToDictionary(r => r.Product.Name);
        
        available = new();
    }

    private bool Consume(string chem, long quantity)
    {
        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity));

        if (!available.ContainsKey(chem))
            available[chem] = 0;

        if (available[chem] < quantity && !Produce(chem, quantity - available[chem]))
            return false;

        available[chem] -= quantity;
        return true;
    }

    private bool Produce(string chem, long quantity)
    {
        if (chem == "ORE")
            return false;

        var reaction = reactions[chem];
        var reactionCount = (long)Math.Ceiling((double)quantity / reaction.Product.Quantity);

        if (reaction.Reactants.Any(reactant => !Consume(reactant.Name, reactionCount * reactant.Quantity)))
            return false;

        available[chem] = available.GetValueOrDefault(chem) + reactionCount * reaction.Product.Quantity;
        return true;
    }

    public override string Part1()
    {
        available = new() { { "ORE", long.MaxValue } };
        Consume("FUEL", 1);
        return $"{long.MaxValue - available["ORE"]}";
    }

    public override string Part2()
    {
        const long capacity = 1_000_000_000_000;
        available = new() { { "ORE", capacity } };
        Consume("FUEL", 1);

        var oreConsumed = capacity - available["ORE"];
        while (Produce("FUEL", Math.Max(1, available["ORE"] / oreConsumed)))
        {
        }

        return $"{available["FUEL"] + 1}";
    }

    private struct Component
    {
        public string Name { get; init; }
        public int Quantity { get; init; }
    }

    private class Reaction
    {
        public readonly Component Product;
        public readonly Component[] Reactants;

        private Reaction(Component[] reactants, Component product)
        {
            Reactants = reactants;
            Product = product;
        }

        public static Reaction Parse(string s)
        {
            var ss = s.Split(new[] { ", ", " => " }, StringSplitOptions.None);

            return new(
                ss.Take(ss.Length - 1).Select(ParseComponent).ToArray(),
                ParseComponent(ss[^1])
            );

            static Component ParseComponent(string s)
            {
                var spl = s.Split(' ', 2);
                return new()
                {
                    Quantity = int.Parse(spl[0]),
                    Name = spl[1]
                };
            }
        }
    }
}
