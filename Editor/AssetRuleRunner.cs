#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo

namespace Neuston.AssetRules
{
	public class AssetRuleRunner
	{
		/// <summary>
		/// Run all the automatically discovered rules.
		/// </summary>
		public static AssetRuleRunnerReport Run()
		{
			return Run(AssetRuleTypeFinder.FindRules());
		}

		/// <summary>
		/// Run a specific set of asset rules.
		/// </summary>
		public static AssetRuleRunnerReport Run(IEnumerable<Type> rules)
		{
			var report = new AssetRuleRunnerReport();

			foreach (var ruleType in rules)
			{
				var rule = (IAssetRule)Activator.CreateInstance(ruleType);
				var ruleReport = new RuleReport(rule);

				var assetPaths = AssetDatabase.FindAssets($"t:{rule.AssetType.Name}").Select(AssetDatabase.GUIDToAssetPath).ToList();

				AssetRuleConfigurationSingleton.Instance.FilterAssetPaths(ruleType, assetPaths);

				for (var i = 0; i < assetPaths.Count; i++)
				{
					string? assetPath = assetPaths[i];
					EditorUtility.DisplayProgressBar($"Evaluating Asset Rule {ruleType.Name}", assetPath, i/(float)assetPaths.Count);
					var asset = AssetDatabase.LoadAssetAtPath(assetPath, rule.AssetType);
					var violations = rule.Evaluate(asset).ToList();
					ruleReport.Violations.AddRange(violations);
				}

				EditorUtility.ClearProgressBar();

				report.RuleReports.Add(ruleReport);
			}

			return report;
		}
	}
}
