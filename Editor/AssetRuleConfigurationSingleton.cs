#nullable enable

using System;
using UnityEditor;

// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo

namespace Neuston.AssetRules
{
	class AssetRuleConfigurationSingleton
	{
		static IAssetRuleConfiguration? instance;

		public static IAssetRuleConfiguration Instance
		{
			get
			{
				if (instance != null)
				{
					return instance;
				}

				var assetRuleConfigurationTypes = TypeCache.GetTypesDerivedFrom<IAssetRuleConfiguration>();

				if (assetRuleConfigurationTypes.Count == 0)
				{
					throw new AssetRuleConfigurationException("No IAssetRuleConfiguration found. Create a concrete implementation of IAssetRuleConfiguration in your project.");
				}

				if (assetRuleConfigurationTypes.Count > 1)
				{
					throw new AssetRuleConfigurationException("More than one IAssetRuleConfiguration found. Create only one concrete implementation of IAssetRuleConfiguration in your project.");
				}

				var type = assetRuleConfigurationTypes[0];

				instance = (IAssetRuleConfiguration)Activator.CreateInstance(type);

				return instance;
			}
		}
	}
}
