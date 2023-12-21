namespace TransUniverseCorp.Algorithm
{
    internal class ObjectKeeper
    {
        private List<object?> kept = [];

        public int Keep(object obj)
        {
            for(int i = 0; i < kept.Count; i++)
            {
                if (kept[i] is null)
                {
                    kept[i] = obj;
                    return i;
                }
            }
            kept.Add(obj);
            return kept.Count - 1;
        }

        public object? Get(int index) => kept[index];

        public void Free(int index) => kept[index] = null;

        private ObjectKeeper() { }

        public static readonly ObjectKeeper Instance = new();
    }
}
