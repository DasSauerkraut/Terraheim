using Terraheim.Utility;

namespace Terraheim.Armor
{
    internal static class AddCirculets
    {
        internal static void Init()
        {
            On.ObjectDB.CopyOtherDB += AddBelts;
        }

        private static void AddBelts(On.ObjectDB.orig_CopyOtherDB orig, ObjectDB self, ObjectDB other)
        {
            ArmorHelper.AddBelt("woodsmanHelmet");
            ArmorHelper.AddBelt("minersBelt");
            ArmorHelper.AddBelt("waterproofBelt");
            ArmorHelper.AddBelt("farmerBelt");
            ArmorHelper.AddBelt("thiefBelt");
        }
    }
}
