#nullable enable

using UnityEngine;

// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo

namespace Neuston.AssetRules
{
	public class Violation : IViolation
	{
		public Violation(Object @object, string reasonForViolation, string suggestedFix)
		{
			Object = @object;
			ReasonForViolation = reasonForViolation;
			SuggestedFix = suggestedFix;
		}

		public Object Object { get; }
		public string ReasonForViolation { get; }
		public string SuggestedFix { get; }
	}
}
