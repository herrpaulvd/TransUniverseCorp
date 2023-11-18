using UnviersalMV;

namespace TransUniverseCorp.Experimental
{
    public class StrangeObject
    {
        [PassSimple]
        public string Name { get; set; }
        public int Count { get; set; }
        [WithName("x")]
        public float SomeValue { get; set; }
        public bool Checked { get; set; }
        [NoPass]
        public string NotUsed { get; set; }

        public StrangeObject() { }

        public StrangeObject(string name, int count, float someValue, bool @checked, string notUsed)
        {
            Name = name;
            Count = count;
            SomeValue = someValue;
            Checked = @checked;
            NotUsed = notUsed;
        }

        public static List<StrangeObject> Pool =
        [
            new("x", 1, 2, true, "a"),
            new("y", 11, 22, false, "aa"),
            new("z", 13, 23, true, "a333"),
        ];
    }
}
