#nullable enable

using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;

// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo

namespace Neuston.AssetRules
{
	public class ViolatedRulesMessageGenerator
	{
		public static string Generate(AssetRuleRunnerReport report)
		{
			var sb = new StringBuilder();

			// Title of message.
			int violationCount = report.RuleReports.Sum(r => r.Violations.Count);
			sb.AppendLine($"Asset rules were violated {violationCount} times.");
			sb.AppendLine();

			// Body of message.
			foreach (var ruleReport in report.RuleReports)
			{
				if (ruleReport.Violations.Any())
				{
					sb.AppendLine($"The asset rule {ruleReport.RuleName} was violated {ruleReport.Violations.Count} times.");
					sb.AppendLine(ruleReport.ReasonWhyRuleIsImportant);
					sb.AppendLine();

					foreach (var violation in ruleReport.Violations)
					{
						var assetPath = AssetDatabase.GetAssetPath(violation.Object);
						var fileName = Path.GetFileName(assetPath);
						sb.AppendLine($"{fileName} --> {violation.ReasonForViolation} {violation.SuggestedFix}");
					}

					sb.AppendLine();
				}
			}

			return sb.ToString();
		}
	}
}
