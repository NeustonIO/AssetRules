# AssetRules

## Description

AssetRules is a Unity package that helps you easily set up and enforce rules for your assets.

## Usage

### Create a rule

```C#
public class TexturesShouldBePowerOfTwo : AssetRule<Texture2D>
{
	public override string ReasonWhyRuleIsImportant => "Textures with sizes that are power of two can be compressed better. That makes the app smaller and faster. Examples: 64x64, 256x256, 1024x256 etc.";

	protected override IEnumerable<IViolation> Evaluate(Texture2D texture)
	{
		if (!IsPowerOfTwo(texture))
		{
			yield return new Violation(
				texture,
				reasonForViolation: $"Resolution {texture.width}x{texture.height} is not power of two.",
				suggestedFix: $"You should resize the texture to a power of two resolution.");
		}
	}

	private bool IsPowerOfTwo(Texture2D texture)
	{
		return Mathf.IsPowerOfTwo(texture.width) && Mathf.IsPowerOfTwo(texture.height);
	}
}
```

### Run the rules

- Open `Tools > Neuston > Asset Rules Runner`.
- Click `Run Asset Rules`.
- See list of violations.
- Fix the violations.

### Enforce the rules

It's possible to make a test that runs the rules and fails if there are any violations.

This is useful for enforcement of the rules in a CI pipeline.

```C#
using Neuston.AssetRules;
using NUnit.Framework;

public class AssetRuleTests
{
	[Test]
	public void AssetsShouldComplyWithAssetRules()
	{
		var report = AssetRuleRunner.Run();

		if (report.HasViolations)
		{
			Assert.Fail(ViolatedRulesMessageGenerator.Generate(report));
		}
	}
}
```