#nullable enable

using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo

namespace Neuston.AssetRules
{
	public abstract class AssetRule<TObjectType> : IAssetRule where TObjectType : Object
	{
		public abstract string ReasonWhyRuleIsImportant { get; }
		public Type AssetType => typeof(TObjectType);
		protected abstract IEnumerable<IViolation> Evaluate(TObjectType target);
		public IEnumerable<IViolation> Evaluate(Object target)
		{
			return Evaluate((TObjectType)target);
		}

		/// <summary>
		/// This is the project-specific AssetRules configuration.
		/// </summary>
		protected IAssetRuleConfiguration Configuration => AssetRuleConfigurationSingleton.Instance;
	}
}
