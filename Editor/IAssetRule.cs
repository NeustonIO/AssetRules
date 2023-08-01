#nullable enable

using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo

namespace Neuston.AssetRules
{
	public interface IAssetRule
	{
		string ReasonWhyRuleIsImportant { get; }
		IEnumerable<IViolation> Evaluate(Object target);
		Type AssetType { get; }
	}
}
