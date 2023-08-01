#nullable enable

using System.Collections.Generic;
using System.Linq;

// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo

namespace Neuston.AssetRules
{
	public class AssetRuleRunnerReport
	{
		public List<RuleReport> RuleReports { get; set; } = new List<RuleReport>();
		public bool HasViolations => RuleReports.Any(r => r.Violations.Any());
	}
}
