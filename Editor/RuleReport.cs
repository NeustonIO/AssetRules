#nullable enable

using System.Collections.Generic;

// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo

namespace Neuston.AssetRules
{
	public class RuleReport
	{
		readonly IAssetRule rule;

		public RuleReport(IAssetRule rule)
		{
			this.rule = rule;
		}

		public List<IViolation> Violations { get; } = new List<IViolation>();
		public string RuleName => rule.GetType().Name;
		public string ReasonWhyRuleIsImportant => rule.ReasonWhyRuleIsImportant;
	}
}
