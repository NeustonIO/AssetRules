#nullable enable

using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo

namespace Neuston.AssetRules
{
	public class AssetRuleRunnerWindow : EditorWindow
	{
		class ViolatedRuleSection
		{
			public ViolatedRuleSection(RuleReport ruleReport)
			{
				RuleReport = ruleReport;
			}

			public RuleReport RuleReport { get; set; }
			public bool Visible { get; set; }
		}

		List<ViolatedRuleSection> violatedRuleSections = new List<ViolatedRuleSection>();
		Vector2 scrollPosition;

		[MenuItem("Tools/Neuston/Asset Rule Runner")]
		public static void OpenWindow()
		{
			var window = GetWindow<AssetRuleRunnerWindow>();
			window.titleContent.text = "Asset Rule Runner";
		}

		void OnGUI()
		{
			if (GUILayout.Button("Run Asset Rules", GUILayout.Width(120)))
			{
				RunAssetRules();
			}

			if (violatedRuleSections.Count == 0)
			{
				return;
			}

			DrawViolatedRuleSections();
		}

		void DrawViolatedRuleSections()
		{
			scrollPosition = GUILayout.BeginScrollView(scrollPosition);
			var height = GUILayout.Height(18);

			foreach (var violatedRuleSection in violatedRuleSections)
			{
				var ruleReport = violatedRuleSection.RuleReport;
				if (ruleReport.Violations.Any())
				{
					DrawViolations(violatedRuleSection, ruleReport, height);
				}
			}

			GUILayout.EndScrollView();
		}

		static void DrawViolations(ViolatedRuleSection violatedRuleSection, RuleReport ruleReport, GUILayoutOption height)
		{
			violatedRuleSection.Visible = EditorGUILayout.Foldout(violatedRuleSection.Visible,
				$"{ruleReport.RuleName} was violated {ruleReport.Violations.Count} times.");

			if (violatedRuleSection.Visible)
			{
				GUILayout.Label(ruleReport.ReasonWhyRuleIsImportant);
				GUILayout.Label("Violations:");

				foreach (var violation in ruleReport.Violations)
				{
					DrawViolation(violation, height);
				}
			}
		}

		static void DrawViolation(IViolation violation, GUILayoutOption height)
		{
			var assetPath = AssetDatabase.GetAssetPath(violation.Object);

			GUILayout.BeginHorizontal();

			// Object
			var type = AssetDatabase.GetMainAssetTypeAtPath(assetPath);
			var guiContent = EditorGUIUtility.ObjectContent(null, type);
			string fileName = Path.GetFileName(assetPath);
			guiContent.text = fileName;
			var before = GUI.skin.button.alignment;
			GUI.skin.button.alignment = TextAnchor.MiddleLeft;
			if (GUILayout.Button(guiContent, GUILayout.Width(240), height))
			{
				EditorGUIUtility.PingObject(AssetDatabase.LoadAssetAtPath<Object>(assetPath));
			}

			GUI.skin.button.alignment = before;

			// Suggestion
			var suggestionContent = new GUIContent("Suggestion", violation.SuggestedFix);
			if (GUILayout.Button(suggestionContent, GUILayout.Width(80)))
			{
				EditorUtility.DisplayDialog("Suggestion", violation.SuggestedFix, "OK");
			}

			// Reason
			GUILayout.Label($"{violation.ReasonForViolation}");

			GUILayout.EndHorizontal();
		}

		void RunAssetRules()
		{
			var report = AssetRuleRunner.Run();

			if (report.HasViolations)
			{
				violatedRuleSections = report.RuleReports.Select(r => new ViolatedRuleSection(r)).ToList();
				//Debug.LogError(ViolatedRulesMessageGenerator.Generate(report));
			}
		}

		void OnProjectChange()
		{
			Clear();
		}

		void OnDestroy()
		{
			Clear();
		}

		void Clear()
		{
			violatedRuleSections.Clear();
		}
	}
}
