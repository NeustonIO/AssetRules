#nullable enable

using System;

// ReSharper disable UnusedMember.Global
// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo

namespace Neuston.AssetRules
{
	public class AssetRuleConfigurationException : Exception
	{
		public AssetRuleConfigurationException()
		{
		}

		public AssetRuleConfigurationException(string message) : base(message)
		{
		}

		public AssetRuleConfigurationException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
