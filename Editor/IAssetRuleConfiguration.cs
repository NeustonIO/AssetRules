#nullable enable

using System;
using System.Collections.Generic;
using UnityEditor;

// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo

namespace Neuston.AssetRules
{
	/// <summary>
	/// Create a concrete implementation of this interface in your project.
	/// It will automatically be discovered by AssetRules and used for configuration.
	/// </summary>
	public interface IAssetRuleConfiguration
	{
		/// <summary>
		/// These are the build targets that the rules should consider.
		/// </summary>
		public BuildTarget[] BuildTargetsOfInterest { get; }

		/// <summary>
		/// These are the platforms that the rules should consider.
		/// The options for the platform string are "Standalone", "Web", "iPhone", "Android",
		/// "WebGL", "Windows Store Apps", "PS4", "XboxOne", "Nintendo Switch" and "tvOS".
		/// </summary>
		public string[] PlatformsOfInterest { get; }

		/// <summary>
		/// This is where you can filter which assets a rule should consider.
		/// </summary>
		/// <param name="ruleType">The rule in question.</param>
		/// <param name="assetPaths">Modify this if you want.</param>
		void FilterAssetPaths(Type ruleType, List<string> assetPaths);
	}
}
