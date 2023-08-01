#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo

namespace Neuston.AssetRules
{
    public class AssetRuleTypeFinder
    {
        public static IEnumerable<Type> FindRules()
        {
            return TypeCache.GetTypesDerivedFrom<IAssetRule>().Where(t => t.IsClass && !t.IsAbstract);
        }
    }
}