using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;

namespace Terraheim.Utility
{
    class Pieces
    {
        public static CraftingStation Reforger;
        public static void Init()
        {
            AddReforgerPiece();
            AddUpgradePieces();
        }

        static void AddReforgerPiece()
        {
            PieceConfig config = new PieceConfig()
            {
                PieceTable = "Hammer",
                CraftingStation = "piece_workbench",
                Requirements = new RequirementConfig[]
                {
                    new RequirementConfig() {Item = "Wood", Amount = 10, Recover = true },
                    new RequirementConfig() {Item = "Stone", Amount = 10, Recover = true },
                    new RequirementConfig() {Item = "DeerHide", Amount = 3, Recover = true }
                }
            };

            //CustomPiece CP = new CustomPiece(AssetHelper.PieceReforgerPrefab, config);
            CustomPiece CP = new CustomPiece(AssetHelper.PieceReforgerPrefab, true, config);

            if (CP != null)
            {
                PieceManager.Instance.AddPiece(CP);
                Reforger = CP.Piece.GetComponent<CraftingStation>();
            }
        }

        static void AddUpgradePieces()
        {
            PieceConfig config1 = new PieceConfig()
            {
                PieceTable = "Hammer",
                CraftingStation = "piece_workbench",
                Requirements = new RequirementConfig[]
                    {
                        new RequirementConfig() {Item = "Wood", Amount = 10, Recover = true },
                        new RequirementConfig() {Item = "LeatherScraps", Amount = 3, Recover = true },
                        new RequirementConfig() {Item = "TrollHide", Amount = 3, Recover = true }
                    }
            };

            PieceConfig config2 = new PieceConfig()
            {
                PieceTable = "Hammer",
                CraftingStation = "forge",
                Requirements = new RequirementConfig[]
                    {
                        new RequirementConfig() {Item = "Wood", Amount = 5, Recover = true },
                        new RequirementConfig() {Item = "Bronze", Amount = 3, Recover = true },
                        new RequirementConfig() {Item = "Coal", Amount = 4, Recover = true }
                    }
            };

            PieceConfig config3 = new PieceConfig()
            {
                PieceTable = "Hammer",
                CraftingStation = "forge",
                Requirements = new RequirementConfig[]
                    {
                        new RequirementConfig() {Item = "Wood", Amount = 10, Recover = true },
                        new RequirementConfig() {Item = "Iron", Amount = 10, Recover = true }
                    }
            };

            PieceConfig config4 = new PieceConfig()
            {
                PieceTable = "Hammer",
                CraftingStation = "forge",
                Requirements = new RequirementConfig[]
                    {
                        new RequirementConfig() {Item = "FineWood", Amount = 10, Recover = true },
                        new RequirementConfig() {Item = "Silver", Amount = 2, Recover = true },
                        new RequirementConfig() {Item = "Obsidian", Amount = 10, Recover = true }
                    }
            };

            PieceConfig config5 = new PieceConfig()
            {
                PieceTable = "Hammer",
                CraftingStation = "forge",
                Requirements = new RequirementConfig[]
                    {
                        new RequirementConfig() {Item = "FineWood", Amount = 10, Recover = true },
                        new RequirementConfig() {Item = "BlackMetal", Amount = 5, Recover = true }
                    }
            };

            CustomPiece CP1 = new CustomPiece(AssetHelper.PieceReforgerExt1Prefab, true, config1);
            CustomPiece CP2 = new CustomPiece(AssetHelper.PieceReforgerExt2Prefab, true, config2);
            CustomPiece CP3 = new CustomPiece(AssetHelper.PieceReforgerExt3Prefab, true, config3);
            CustomPiece CP4 = new CustomPiece(AssetHelper.PieceReforgerExt4Prefab, true, config4);
            CustomPiece CP5 = new CustomPiece(AssetHelper.PieceReforgerExt5Prefab, true, config5);

            PieceManager.Instance.AddPiece(CP1);
            PieceManager.Instance.AddPiece(CP2);
            PieceManager.Instance.AddPiece(CP3);
            PieceManager.Instance.AddPiece(CP4);
            PieceManager.Instance.AddPiece(CP5);
        }
    }
}
