namespace ItemsApi.Models
{
    using System.Collections.Generic;

    public class ItemSelector
    {
        public string ItemName;
        public ICollection<PotentialProperty> Properties;
        public ICollection<PotentialRelationship> Relationships;
    }

    public class PotentialRelationship
    {
        public string RelationshipName;
        public string ThingTarget;
        public int? SourceNumberLower;
        public int? SourceNumberUpper;
        public int? TargetNumberLower;
        public int? TargetNumberUpper;
    }

    public class PotentialProperty
    {
        public string Name;
        public string DataType;
        public bool IsSelected;
    }
}