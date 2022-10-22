namespace DoomNG.Engine
{
    public enum FilterType { WhiteList, BlackList}
    internal struct ContactFilter
    {
        public FilterType FilterType;
        public GameObject[] filteredObjects;

        public ContactFilter(GameObject[] filteredObjects, FilterType type)
        {
            this.filteredObjects = filteredObjects;
            this.FilterType = type;
        }
    }
}
