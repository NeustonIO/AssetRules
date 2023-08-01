#nullable enable

using UnityEngine;

// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo

namespace Neuston.AssetRules
{
	public interface IViolation
	{
		Object Object { get; }
		string ReasonForViolation { get; }
		string SuggestedFix { get; }
	}
}
