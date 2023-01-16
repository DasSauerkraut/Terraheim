using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;

namespace Terraheim.Utility;

internal class Pieces
{
	public static CraftingStation Reforger;

	public static void Init()
	{
		AddReforgerPiece();
		AddUpgradePieces();
	}

	private static void AddReforgerPiece()
	{
		PieceConfig pieceConfig = new PieceConfig();
		pieceConfig.PieceTable = "Hammer";
		pieceConfig.CraftingStation = "piece_workbench";
		pieceConfig.Requirements = new RequirementConfig[3]
		{
			new RequirementConfig
			{
				Item = "Wood",
				Amount = 10,
				Recover = true
			},
			new RequirementConfig
			{
				Item = "Stone",
				Amount = 10,
				Recover = true
			},
			new RequirementConfig
			{
				Item = "DeerHide",
				Amount = 3,
				Recover = true
			}
		};
		PieceConfig pieceConfig2 = pieceConfig;
		CustomPiece customPiece = new CustomPiece(AssetHelper.PieceReforgerPrefab, pieceConfig2);
		if (customPiece != null)
		{
			PieceManager.Instance.AddPiece(customPiece);
			Reforger = customPiece.Piece.GetComponent<CraftingStation>();
		}
	}

	private static void AddUpgradePieces()
	{
		PieceConfig pieceConfig = new PieceConfig();
		pieceConfig.PieceTable = "Hammer";
		pieceConfig.CraftingStation = "piece_workbench";
		pieceConfig.Requirements = new RequirementConfig[3]
		{
			new RequirementConfig
			{
				Item = "Wood",
				Amount = 10,
				Recover = true
			},
			new RequirementConfig
			{
				Item = "LeatherScraps",
				Amount = 3,
				Recover = true
			},
			new RequirementConfig
			{
				Item = "TrollHide",
				Amount = 3,
				Recover = true
			}
		};
		PieceConfig pieceConfig2 = pieceConfig;
		pieceConfig = new PieceConfig();
		pieceConfig.PieceTable = "Hammer";
		pieceConfig.CraftingStation = "forge";
		pieceConfig.Requirements = new RequirementConfig[3]
		{
			new RequirementConfig
			{
				Item = "Wood",
				Amount = 5,
				Recover = true
			},
			new RequirementConfig
			{
				Item = "Bronze",
				Amount = 3,
				Recover = true
			},
			new RequirementConfig
			{
				Item = "Coal",
				Amount = 4,
				Recover = true
			}
		};
		PieceConfig pieceConfig3 = pieceConfig;
		pieceConfig = new PieceConfig();
		pieceConfig.PieceTable = "Hammer";
		pieceConfig.CraftingStation = "forge";
		pieceConfig.Requirements = new RequirementConfig[2]
		{
			new RequirementConfig
			{
				Item = "Wood",
				Amount = 10,
				Recover = true
			},
			new RequirementConfig
			{
				Item = "Iron",
				Amount = 10,
				Recover = true
			}
		};
		PieceConfig pieceConfig4 = pieceConfig;
		pieceConfig = new PieceConfig();
		pieceConfig.PieceTable = "Hammer";
		pieceConfig.CraftingStation = "forge";
		pieceConfig.Requirements = new RequirementConfig[3]
		{
			new RequirementConfig
			{
				Item = "FineWood",
				Amount = 10,
				Recover = true
			},
			new RequirementConfig
			{
				Item = "Silver",
				Amount = 2,
				Recover = true
			},
			new RequirementConfig
			{
				Item = "Obsidian",
				Amount = 10,
				Recover = true
			}
		};
		PieceConfig pieceConfig5 = pieceConfig;
		pieceConfig = new PieceConfig();
		pieceConfig.PieceTable = "Hammer";
		pieceConfig.CraftingStation = "forge";
		pieceConfig.Requirements = new RequirementConfig[2]
		{
			new RequirementConfig
			{
				Item = "FineWood",
				Amount = 10,
				Recover = true
			},
			new RequirementConfig
			{
				Item = "BlackMetal",
				Amount = 5,
				Recover = true
			}
		};
		PieceConfig pieceConfig6 = pieceConfig;
		CustomPiece customPiece = new CustomPiece(AssetHelper.PieceReforgerExt1Prefab, pieceConfig2);
		CustomPiece customPiece2 = new CustomPiece(AssetHelper.PieceReforgerExt2Prefab, pieceConfig3);
		CustomPiece customPiece3 = new CustomPiece(AssetHelper.PieceReforgerExt3Prefab, pieceConfig4);
		CustomPiece customPiece4 = new CustomPiece(AssetHelper.PieceReforgerExt4Prefab, pieceConfig5);
		CustomPiece customPiece5 = new CustomPiece(AssetHelper.PieceReforgerExt5Prefab, pieceConfig6);
		PieceManager.Instance.AddPiece(customPiece);
		PieceManager.Instance.AddPiece(customPiece2);
		PieceManager.Instance.AddPiece(customPiece3);
		PieceManager.Instance.AddPiece(customPiece4);
		PieceManager.Instance.AddPiece(customPiece5);
	}
}
